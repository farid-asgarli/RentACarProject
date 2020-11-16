using g_auto.Areas.Admin.Filter;
using g_auto.DAL;
using g_auto.Hubs;
using g_auto.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace g_auto.Areas.Admin.Controllers
{
    [logout]
    public class BlogCategoriesController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/Categories
        public ActionResult Index(string sortOrder, string searchString)
        {
            //List<BlogCategory> x = db.BlogCategory.Include("BlogToCategory").Include("BlogToCategory.Blog").Include("Admin").ToList();
            //return View(x);


            ViewBag.BlogCategoriesPage = true;


            ViewBag.NameSortParam = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.BlogSortParam = sortOrder == "Blog" ? "blog_desc" : "Blog";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "";

            var xdb = from x in db.BlogCategory.Include("BlogToCategory").Include("BlogToCategory.Blog").Include("Admin")
                      select x;
            if (!String.IsNullOrEmpty(searchString))
            {
                xdb = xdb.Where(s => s.Name.ToString().Contains(searchString)
                                       || s.BlogToCategory.FirstOrDefault().Blog.Title.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Name":
                    xdb = xdb.OrderBy(s => s.Name);
                    break;

                case "name_desc":
                    xdb = xdb.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    xdb = xdb.OrderBy(s => s.PostDate);
                    break;
                case "date_desc":
                    xdb = xdb.OrderByDescending(s => s.PostDate);
                    break;
                case "blog_desc":
                    xdb = xdb.OrderByDescending(s => s.BlogToCategory.FirstOrDefault()!=null? s.BlogToCategory.FirstOrDefault().Blog.Title:"");
                    break;
                case "Blog":
                    xdb = xdb.OrderBy(s => s.BlogToCategory.FirstOrDefault() != null ? s.BlogToCategory.FirstOrDefault().Blog.Title : "");
                    break;
                default:
                    xdb = xdb.OrderByDescending(s => s.PostDate);
                    break;

            }
            return View(xdb.ToList());

        }

        public ActionResult Create()
        {
            ViewBag.BlogCategoriesPage = true;
            ViewBag.Admin = (int)Session["AdminId"];
            return View();
        }

        [HttpPost]
        public ActionResult Create(BlogCategory x)
        {
            ViewBag.BlogCategoriesPage = true;
            if (ModelState.IsValid)
            {
                BlogCategory xdb = new BlogCategory();

                xdb.AdminId = x.AdminId;
                xdb.PostDate = DateTime.Now;
                xdb.Name = x.Name;

                db.BlogCategory.Add(xdb);
                db.SaveChanges();

                int AdminId = (int)Session["AdminId"];
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelCreate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Name, "Blog Category");

                TempData["Create"] = "Create";

                return RedirectToAction("Index", "BlogCategories");

            }
            TempData["Create-Error"] = "Create-Error";
            ViewBag.BlogCategoriesPage = true;

            ViewBag.Admin = (int)Session["AdminId"];
            return View(x);
        }

        public ActionResult Update(int Id)
        {
            ViewBag.BlogCategoriesPage = true;

            BlogCategory x = db.BlogCategory.Find(Id);

            if (x == null)
            {
                return RedirectToAction("Oops", "Error", new { id = 404 });
            }

            return View(x);

        }


        [HttpPost]
        public ActionResult Update(BlogCategory x)
        {
            if (ModelState.IsValid)
            {
                db.Entry(x).State = EntityState.Modified;


                db.SaveChanges(); 
                
                int AdminId = (int)Session["AdminId"];
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelUpdate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), x.Name, "Blog Category");


                TempData["Update"] = "Update";
                return RedirectToAction("Index");
            }

            TempData["Update-Error"] = "Update-Error";
            ViewBag.BlogCategoriesPage = true;
            return View(x);
        }

        public JsonResult Delete(int id)
        {
            BlogCategory xdb = db.BlogCategory.Find(id);
     
            db.BlogCategory.Remove(xdb);
            db.SaveChanges();

            int AdminId = (int)Session["AdminId"];
            string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelDelete(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Name, "Blog Category");

            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModalDetails(int id)
        {
            var xdb = db.BlogCategory.Where(c => c.Id == id).Select(s => new
            {

                Id = s.Id,
                Name = s.Name,
                Admin = s.Admin.FullName,
                PostDate = s.PostDate,
                EditDate = s.ModifiedDate,
                EditAdmin = s.AdminModified.FullName,

            }).FirstOrDefault();

            return Json(xdb, JsonRequestBehavior.AllowGet);
        }
    }
}