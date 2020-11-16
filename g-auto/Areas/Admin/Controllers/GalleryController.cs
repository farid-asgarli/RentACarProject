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
    public class GalleryController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/Gallery
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.GalleryPage = true;

            //List<GalleryImage> xdb = db.GalleryImage.Include("Admin").ToList();
            //return View(xdb);


            ViewBag.NameSortParam = sortOrder=="Name" ? "name_desc" : "Name";
            ViewBag.IsSortParam = sortOrder == "Is" ? "is_desc" : "Is";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "";
            var xdb = from x in db.GalleryImage.Include("Admin")
                      select x;
            if (!String.IsNullOrEmpty(searchString))
            {
                xdb = xdb.Where(s => s.Title.ToString().Contains(searchString)
                                       || (s.Content).ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    xdb = xdb.OrderByDescending(s => s.Title);
                    break;
                case "Name":
                    xdb = xdb.OrderBy(s => s.Title);
                    break;
                case "Date":
                    xdb = xdb.OrderBy(s => s.PostDate);
                    break;
                case "date_desc":
                    xdb = xdb.OrderByDescending(s => s.PostDate);
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
            ViewBag.GalleryPage = true;

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(GalleryImage x)
        {
            int AdminId = (int)Session["AdminId"];

            if (ModelState.IsValid) {

                GalleryImage xdb = new GalleryImage();

                xdb.AdminId = x.AdminId;
                xdb.Content = x.Content;
                xdb.PostDate = DateTime.Now;
                xdb.Title = x.Title;
                if (db.AdminSettings.FirstOrDefault(c => c.AdminId == AdminId).alwaysActive)
                {
                    xdb.isActive = true;
                }
                else
                {
                    xdb.isActive = false;
                }

                if (x.PictureFile == null)
                {
                    xdb.Picture = "Default2";
                }
                else
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + Regex.Replace(x.PictureFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    x.PictureFile.SaveAs(imagePath);
                    xdb.Picture = imageName;

                }

                db.GalleryImage.Add(xdb);

                db.SaveChanges();
                

                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelCreate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Title, "Gallery Image");

                TempData["Create"] = "Create";



                return RedirectToAction("Index", "Gallery");
            }
            TempData["Create-Error"] = "Create-Error";
            ViewBag.Admin = (int)Session["AdminId"];
            ViewBag.GalleryPage = true;

            return View(x);
        }

        public ActionResult Update(int? id)
        {
            GalleryImage x = db.GalleryImage.Find(id);
            ViewBag.GalleryPage = true;

            if (x == null)
            {
                return RedirectToAction("Oops", "Error", new { id = 404 });
            }

            return View(x);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(GalleryImage x)
        {
            if (ModelState.IsValid)
            {

                GalleryImage xdb = db.GalleryImage.Include("Admin").FirstOrDefault(w=>w.Id==x.Id);

                xdb.Content = x.Content;
                xdb.Title = x.Title;
                xdb.ModifiedDate = DateTime.Now;
                xdb.AdminModifiedId = (int)Session["AdminId"];

                if (x.PictureFile != null)
                {
                    string imageName = DateTime.Now.ToString("ssfff") + Regex.Replace(x.PictureFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.Picture);
                    System.IO.File.Delete(oldImagePath);

                    x.PictureFile.SaveAs(imagePath);
                    xdb.Picture = imageName;
                }

                db.Entry(xdb).State = EntityState.Modified;
                db.SaveChanges();
                int AdminId = (int)Session["AdminId"];
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelUpdate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Title, "Gallery Image");

                TempData["Update"] = "Update";




                return RedirectToAction("Index", "Gallery");
            }
            TempData["Update-Error"] = "Update-Error";
            ViewBag.GalleryPage = true;


            return View(x);
        }

        public JsonResult Delete(int id)
        {
            GalleryImage xdb = db.GalleryImage.Find(id);
         

            string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.Picture);
            System.IO.File.Delete(oldImagePath);

            db.GalleryImage.Remove(xdb);
            db.SaveChanges();
            int AdminId = (int)Session["AdminId"];
            string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelDelete(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Title, "Gallery Image");

            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateActive(int? id)
        {
            var result = false;

            if (id != null)
            {
                GalleryImage xdb = db.GalleryImage.FirstOrDefault(w => w.Id == id);


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

            var xdb = db.GalleryImage.Where(c => c.Id == id).Select(s => new
            {

                Id = s.Id,
                Name = s.Title,
                Content = s.Content,
                Admin = s.Admin.FullName,
                PostDate = s.PostDate,
                EditDate = s.ModifiedDate,
                EditAdmin = s.AdminModified.FullName,
                Image = s.Picture

            }).FirstOrDefault();





            return Json(new { xdb, content = HttpUtility.HtmlDecode(xdb.Content) }, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
    }
}