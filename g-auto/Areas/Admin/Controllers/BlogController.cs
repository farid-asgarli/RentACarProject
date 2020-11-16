using g_auto.Areas.Admin.Filter;
using g_auto.DAL;
using g_auto.Hubs;
using g_auto.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace g_auto.Areas.Admin.Controllers
{
    [logout]
    public class BlogController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/Blog
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.BlogPage = true;
            //List<Blog> x = db.Blog.Include("BlogToCategory").Include("BlogToCategory.BlogCategory").Include("BlogToTags").Include("BlogToTags.Tag").Include("Admin").ToList();

            //return View(x);


            ViewBag.NameSortParam = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.CatSortParam = sortOrder == "Cat" ? "cat_desc" : "Cat";
            ViewBag.TagSortParam = sortOrder == "Tag" ? "tag_desc" : "Tag";
            ViewBag.IsSortParam = sortOrder=="Is" ? "is_desc" : "Is";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var xdb = from x in db.Blog.Include("BlogToCategory").Include("BlogToCategory.BlogCategory").Include("BlogToTags").Include("BlogToTags.Tag").Include("Admin")
                      select x;
            if (!String.IsNullOrEmpty(searchString))
            {
                xdb = xdb.Where(s => s.Title.ToString().Contains(searchString)
                                       || s.BlogToCategory.FirstOrDefault().BlogCategory.Name.ToString().Contains(searchString)
                                       || s.BlogToTags.FirstOrDefault().Tag.Name.ToString().Contains(searchString) || (s.Content).ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Name":
                    xdb = xdb.OrderBy(s => s.Title);
                    break;
                case "name_desc":
                    xdb = xdb.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    xdb = xdb.OrderBy(s => s.PostDate);
                    break;
                case "date_desc":
                    xdb = xdb.OrderByDescending(s => s.PostDate);
                    break;
                case "cat_desc":
                    xdb = xdb.OrderByDescending(s => s.BlogToCategory.FirstOrDefault()!=null? s.BlogToCategory.FirstOrDefault().BlogCategory.Name:"");
                    break;
                case "Cat":
                    xdb = xdb.OrderBy(s => s.BlogToCategory.FirstOrDefault()!=null? s.BlogToCategory.FirstOrDefault().BlogCategory.Name:"");
                    break;
                case "tag_desc":
                    xdb = xdb.OrderByDescending(s => s.BlogToTags.FirstOrDefault()!=null? s.BlogToTags.FirstOrDefault().Tag.Name:"");
                    break;
                case "Tag":
                    xdb = xdb.OrderBy(s => s.BlogToTags.FirstOrDefault() != null ? s.BlogToTags.FirstOrDefault().Tag.Name : "");
                    break;
                case "is_desc":
                    xdb = xdb.OrderByDescending(s => s.isActive);
                    break;
                case "Is":
                    xdb = xdb.OrderBy(s => s.isActive);
                    break;
                default:
                    xdb = xdb.OrderByDescending(s => s.PostDate);
                    break;
            }
            return View(xdb.ToList());


        }
        public ActionResult Create()
        {
            ViewBag.Admin = (int)Session["AdminId"];
            ViewBag.Category = db.BlogCategory.ToList();
            ViewBag.Tag = db.Tags.ToList();
            ViewBag.BlogPage = true;

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Blog x)
        {
            int AdminId = (int)Session["AdminId"];

            ViewBag.BlogPage = true;

            if (ModelState.IsValid)
            {
                Blog xdb = new Blog();

                if (x.BlogCoverImageFile == null)
                {
                    xdb.BlogCoverImage = "Default2";
                }
                else
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + Regex.Replace(x.BlogCoverImageFile.FileName, "[^A-Za-z0-9.]", "") ;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    x.BlogCoverImageFile.SaveAs(imagePath);
                    xdb.BlogCoverImage = imageName;

                }


                xdb.Title = x.Title;
                xdb.AdminId = x.AdminId;
                xdb.Content = x.Content;
                xdb.Description = x.Description;
                xdb.PostDate = DateTime.Now;
                xdb.isActive = false;
                xdb.enableComments = x.enableComments;
                if (db.AdminSettings.FirstOrDefault(c => c.AdminId == AdminId).alwaysActive)
                {
                    xdb.isActive = true;
                }
                else
                {
                    xdb.isActive = false;
                }

                db.Blog.Add(xdb);
                db.SaveChanges();

                if ( x.BlogCategoryId != null)
                {
                    foreach (var item in x.BlogCategoryId)
                    {
                        BlogToCategory bcat = new BlogToCategory();
                        bcat.BlogId = xdb.Id;
                        bcat.BlogCategoryId = item;

                        db.BlogToCategory.Add(bcat);
                    }
                }

                db.SaveChanges();

                if ( x.TagId != null)
                {
                    foreach (var item in x.TagId)
                    {
                        BlogToTags btag = new BlogToTags();
                        btag.BlogId = xdb.Id;
                        btag.TagId = item;

                        db.BlogToTags.Add(btag);
                    }
                }
                db.SaveChanges();
                TempData["Create"] = "Create";
       
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelCreate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Title, "Blog");

                return RedirectToAction("Index", "Blog");
            }
            ViewBag.Category = db.BlogCategory.ToList();
            ViewBag.Tag = db.Tags.ToList();
            ViewBag.Admin = (int)Session["AdminId"];
            ViewBag.BlogPage = true;

            TempData["Create-Error"] = "Create-Error";

            return View(x);
        }


        public ActionResult Update(int? id)
        {
            ViewBag.Category = db.BlogCategory.ToList();
            ViewBag.Tag = db.Tags.ToList();

            ViewBag.BlogPage = true;

            Blog blog = db.Blog.Include("BlogToCategory").Include("BlogToTags").FirstOrDefault(b => b.Id == id);

            if (blog == null)
            {
                return RedirectToAction("Oops", "Error", new { id = 404 });
            }

            return View(blog);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Blog x)
        {
            if (ModelState.IsValid)
            {
                Blog xdb = db.Blog.Include("BlogToCategory").Include("BlogToTags").FirstOrDefault(b => b.Id == x.Id);

                xdb.Title = x.Title;
                xdb.Content = x.Content;
                xdb.Description = x.Description;
                xdb.ModifiedDate = DateTime.Now;
                xdb.AdminModifiedId = (int)Session["AdminId"];
                xdb.enableComments = x.enableComments;


                if (x.BlogCoverImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ssfff") + Regex.Replace(x.BlogCoverImageFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.BlogCoverImage);
                    System.IO.File.Delete(oldImagePath);

                    x.BlogCoverImageFile.SaveAs(imagePath);
                    xdb.BlogCoverImage = imageName;
                }


                db.Entry(xdb).State = EntityState.Modified;
                db.SaveChanges();

                using (DBContext db = new DBContext())
                {
                    foreach (var item in db.BlogToCategory.Where(c => c.BlogId == xdb.Id))
                    {
                        db.BlogToCategory.Remove(item);
                    }

                    db.SaveChanges();
                }

                if (x.BlogCategoryId != null)
                {
                    foreach (var item in x.BlogCategoryId)
                    {
                        BlogToCategory bcat = new BlogToCategory();
                        bcat.BlogId = xdb.Id;
                        bcat.BlogCategoryId = item;

                        db.BlogToCategory.Add(bcat);
                        db.SaveChanges();
                    }
                }

                using (DBContext db = new DBContext())
                {
                    foreach (var item in db.BlogToTags.Where(c => c.BlogId == xdb.Id))
                    {
                        db.BlogToTags.Remove(item);
                    }

                    db.SaveChanges();
                }

                if (x.TagId != null)
                {
                    foreach (var item in x.TagId)
                    {
                        BlogToTags bcat = new BlogToTags();
                        bcat.BlogId = xdb.Id;
                        bcat.TagId = item;

                        db.BlogToTags.Add(bcat);
                        db.SaveChanges();
                    }
                }
                int AdminId = (int)Session["AdminId"];
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelUpdate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Title, "Blog");

                TempData["Update"] = "Update";
                return RedirectToAction("Index");


            }
            ViewBag.BlogPage = true;

            ViewBag.Category = db.BlogCategory.ToList();
            ViewBag.Tag = db.Tags.ToList();



            TempData["Update-Error"] = "Update-Error";

            return View(x);
        }

        public JsonResult Delete(int id)
        {
            Blog xdb = db.Blog.Include("BlogToCategory")
                                         .Include("BlogToTags")
                                         .FirstOrDefault(p => p.Id == id);

            using (DBContext db = new DBContext())
            {
                if (db.BlogToCategory.Where(c => c.BlogId == xdb.Id) != null)
                {
                    foreach (var item in db.BlogToCategory.Where(c => c.BlogId == xdb.Id))
                    {
                        db.BlogToCategory.Remove(item);
                    }
                }

            }

            using (DBContext db = new DBContext())
            {
                if (db.BlogToTags.Where(c => c.BlogId == xdb.Id) != null)
                {
                    foreach (var item in db.BlogToTags.Where(c => c.BlogId == xdb.Id))
                    {
                        db.BlogToTags.Remove(item);
                    }
                }


            }

            string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.BlogCoverImage);
            System.IO.File.Delete(oldImagePath);

            db.SaveChanges();

            db.Blog.Remove(xdb);
            db.SaveChanges();

            int AdminId = (int)Session["AdminId"];
            string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelDelete(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Title, "Blog");
            return Json(JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateActive(int? id)
        {
            var result = false;

            if (id != null)
            {
                Blog xdb = db.Blog.FirstOrDefault(w => w.Id == id);


                if (xdb.isActive == false)
                {
                    xdb.isActive = true;
                }
                else
                {
                    xdb.isActive = false;

                }




                db.Entry(xdb).State = EntityState.Modified;
                db.SaveChanges();

                result = true;

            }



            return Json(result, JsonRequestBehavior.AllowGet);
        }


        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }
        public JsonResult ModalDetails(int id)
        {

            var xdb = db.Blog.Where(c => c.Id == id).Select(s => new
            {

                Id = s.Id,
                Name = s.Title,
                Content = s.Content,
                Admin = s.Admin.FullName,
                PostDate = s.PostDate,
                EditDate = s.ModifiedDate,
                EditAdmin = s.AdminModified.FullName,
                Image = s.BlogCoverImage,
                Desc = s.Description,
                Tags = s.BlogToCategory.ToList(),
                Categories = s.BlogToTags.ToList(),
                EnableComments = s.enableComments,
                TotalViews =s.ViewCount

            }).FirstOrDefault();





            return Json(new { xdb, content = HttpUtility.HtmlDecode(xdb.Content), desc=  HttpUtility.HtmlDecode(xdb.Desc) }, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
    }
}