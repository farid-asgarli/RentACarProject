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
    public class TagsController : Controller
    {
        // GET: Admin/Tags
        DBContext db = new DBContext();
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.TagsPage = true;

            //List<Tags> xdb = db.Tags.Include("BlogToTags").Include("BlogToTags.Blog").Include("Admin").ToList();
            //return View(xdb);

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.BlogSortParam = sortOrder == "Blog" ? "blog_desc" : "Blog";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var xdb = from x in db.Tags.Include("BlogToTags").Include("BlogToTags.Blog").Include("Admin")
                           select x;
            if (!String.IsNullOrEmpty(searchString))
            {
                xdb = xdb.Where(s => s.Name.ToString().Contains(searchString)
                                       || (s.BlogToTags.FirstOrDefault()!=null && s.BlogToTags.FirstOrDefault().Blog.Title.ToString().Contains(searchString)));
            }
            switch (sortOrder)
            {
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
                    xdb = xdb.OrderByDescending(s => s.BlogToTags.FirstOrDefault() != null? s.BlogToTags.FirstOrDefault().Blog.Title:"");
                    break;
                case "Blog":
                    xdb = xdb.OrderBy(s => s.BlogToTags.FirstOrDefault() != null ? s.BlogToTags.FirstOrDefault().Blog.Title : "");
                    break;
                default:
                    xdb = xdb.OrderBy(s => s.Name);
                    break;
            }
            return View(xdb.ToList());



        }

        public ActionResult Create()
        {
            ViewBag.TagsPage = true;

            ViewBag.Admin = (int)Session["AdminId"];
            return View();
        }

         

        [HttpPost]
        public ActionResult Create(Tags x)
        {

            if (ModelState.IsValid)
            {
                Tags xdb = new Tags();

                xdb.Name = x.Name;
                xdb.AdminId = x.AdminId;
                xdb.PostDate = DateTime.Now;

                db.Tags.Add(xdb);
                db.SaveChanges();

                int AdminId = (int)Session["AdminId"];
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;

                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelCreate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Name, "Tag");


                TempData["Create"] = "Create";


                return RedirectToAction("Index", "Tags");

            }
            ViewBag.TagsPage = true;

            TempData["Create-Error"] = "Create-Error";

            ViewBag.Admin = (int)Session["AdminId"];
            return View(x);
        }

        
        public ActionResult Update(int Id)
        {
            ViewBag.TagsPage = true;

            Tags x = db.Tags.Find(Id);

            if (x == null)
            {
                return RedirectToAction("Oops", "Error", new { id = 404 });
            }

            return View(x);

        }


        [HttpPost]
        public ActionResult Update(Tags x)
        {
            if (ModelState.IsValid)
            {
                Tags xdb = db.Tags.Find(x.Id);


                xdb.Name = x.Name;
                xdb.ModifiedDate = DateTime.Now;
                xdb.AdminModifiedId = (int)Session["AdminId"];

                db.Entry(xdb).State = EntityState.Modified;


                db.SaveChanges();
                int AdminId = (int)Session["AdminId"];
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelUpdate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Name, "Tag");


                TempData["Update"] = "Update";
                return RedirectToAction("Index");


            }
            TempData["Update-Error"] = "Update-Error";
            ViewBag.TagsPage = true;

            return View(x);
        }

        public JsonResult Delete(int id)
        {
            Tags xdb = db.Tags.Find(id);
          

            db.Tags.Remove(xdb);
            db.SaveChanges();

            int AdminId = (int)Session["AdminId"];
            string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;

            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelDelete(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Name, "Tag");

            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModalDetails (int id)
        {
            var xdb = db.Tags.Where(c => c.Id == id).Select(s => new
            {

                Id = s.Id,
                Name = s.Name,
                Admin=s.Admin.FullName,
                PostDate=s.PostDate,
                EditDate=s.ModifiedDate,
                EditAdmin=s.AdminModified.FullName,

            }).FirstOrDefault();

            return Json(xdb, JsonRequestBehavior.AllowGet);
        }
    }
}