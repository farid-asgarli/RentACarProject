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
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace g_auto.Areas.Admin.Controllers
{
    [logout]
    public class SliderController : Controller
    {

        DBContext db = new DBContext();
        // GET: Admin/Slider
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.SliderPage = true;
            //List<Sliders> xdb = db.Sliders.Include("Admin").ToList();
            //return View(xdb);


            ViewBag.NameSortParam = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.SubSortParam = sortOrder == "Sub" ? "sub_desc" : "Sub";
            ViewBag.LinkSortParam = sortOrder == "Link" ? "link_desc" : "Link";
            ViewBag.IsSortParam = sortOrder == "Is" ? "is_desc" : "Is";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "";

            var xdb = from x in db.Sliders.Include("Admin")
                      select x;
            if (!String.IsNullOrEmpty(searchString))
            {
                xdb = xdb.Where(s => s.Title.ToString().Contains(searchString)
                                       || s.Link.ToString().Contains(searchString)
                                       || s.Subtitle.ToString().Contains(searchString));
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
                case "sub_desc":
                    xdb = xdb.OrderByDescending(s => s.Subtitle);
                    break;
                case "Sub":
                    xdb = xdb.OrderBy(s => s.Subtitle);
                    break;
                case "link_desc":
                    xdb = xdb.OrderByDescending(s => s.Link.ToString());
                    break;
                case "Link":
                    xdb = xdb.OrderBy(s => s.Link.ToString());
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
            ViewBag.SliderPage = true;

            ViewBag.Admin = (int)Session["AdminId"];
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Sliders x)
        {
            int AdminId = (int)Session["AdminId"];
            if (ModelState.IsValid)
            {

                Sliders xdb = new Sliders();

                xdb.AdminId = x.AdminId;
                xdb.Title = x.Title;
                xdb.PostDate = DateTime.Now;
                xdb.Subtitle = x.Subtitle;
                xdb.Link = x.Link;

                if (db.AdminSettings.FirstOrDefault(c => c.AdminId == AdminId).alwaysActive)
                {
                    xdb.isActive = true;
                }
                else
                {
                    xdb.isActive = false;
                }

                if (x.ImageFile == null)
                {
                    xdb.Image = "Default2";
                }
                else
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + Regex.Replace(x.ImageFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    x.ImageFile.SaveAs(imagePath);
                    xdb.Image = imageName;

                }

                db.Sliders.Add(xdb);

                db.SaveChanges();
                TempData["Create"] = "Create";
         
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelCreate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Title, "Slider");

                return RedirectToAction("Index", "Slider");
            }
            TempData["Create-Error"] = "Create-Error";
            ViewBag.Admin = (int)Session["AdminId"];
            ViewBag.SliderPage = true;

            return View(x);
        }

        public ActionResult Update(int? id)
        {
            Sliders x = db.Sliders.Find(id);
            ViewBag.SliderPage = true;

            if (x == null)
            {
                return RedirectToAction("Oops", "Error", new { id = 404 });
            }

            return View(x);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Sliders x)
        {
            if (ModelState.IsValid)
            {

                Sliders xdb = db.Sliders.Include("Admin").FirstOrDefault(w => w.Id == x.Id);

                xdb.Subtitle = x.Subtitle;
                xdb.Title = x.Title;
                xdb.Link = x.Link;
                xdb.ModifiedDate = DateTime.Now;
                xdb.AdminModifiedId = (int)Session["AdminId"];

                if (x.ImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ssfff") + Regex.Replace(x.ImageFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.Image);
                    System.IO.File.Delete(oldImagePath);

                    x.ImageFile.SaveAs(imagePath);
                    xdb.Image = imageName;
                }

                db.Entry(xdb).State = EntityState.Modified;
                db.SaveChanges();
                int AdminId = (int)Session["AdminId"];
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelUpdate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Title, "Slider");


                TempData["Update"] = "Update";




                return RedirectToAction("Index", "Slider");
            }
            TempData["Update-Error"] = "Update-Error";
            ViewBag.SliderPage = true;


            return View(x);
        }

        public JsonResult Delete(int id)
        {
            Sliders xdb = db.Sliders.Find(id);
         

            string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.Image);
            System.IO.File.Delete(oldImagePath);

            db.Sliders.Remove(xdb);
            db.SaveChanges();
            int AdminId = (int)Session["AdminId"];
            string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;

            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelDelete(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Title, "Slider");
            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateActive(int? id)
        {
            var result = false;

            if (id != null)
            {
                Sliders xdb = db.Sliders.FirstOrDefault(w => w.Id == id);


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

        public JsonResult ModalDetails(int id)
        {
            var xdb = db.Sliders.Where(c => c.Id == id).Select(s => new
            {

                Id = s.Id,
                Name = s.Title,
                Image=s.Image,
                Link =s.Link,
                Subtitle = s.Subtitle,
                Admin = s.Admin.FullName,
                PostDate = s.PostDate,
                EditDate = s.ModifiedDate,
                EditAdmin = s.AdminModified.FullName,

            }).FirstOrDefault();

            return Json(xdb, JsonRequestBehavior.AllowGet);
        }

    }
}