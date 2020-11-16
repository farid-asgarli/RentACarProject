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
using System.Web.Mvc;

namespace g_auto.Areas.Admin.Controllers
{
    [logout]
    public class BrandController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/Brand
        public ActionResult Index(string sortOrder,string searchString)
        {
            ViewBag.BrandPage = true;


            //List<Brand> x = db.Brand.Include(c => c.Models).Include("Admin").ToList();
            //return View(x);


            ViewBag.NameSortParam = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.CountrySortParam = sortOrder=="Country" ? "country_desc" : "Country";
            ViewBag.ModelSortParam = sortOrder=="Model" ? "model_desc" : "Model";
            ViewBag.IsSortParam = sortOrder=="Is" ? "is_desc" : "Is";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var xdb = from x in db.Brand.Include(c => c.Models).Include("Admin")
                      select x;
            if (!String.IsNullOrEmpty(searchString))
            {
                xdb = xdb.Where(s => s.Name.ToString().Contains(searchString)
                                       || s.Models.FirstOrDefault().Name.ToString().Contains(searchString)
                                       || s.OriginCountry.ToString().Contains(searchString));
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
                case "model_desc":
                    xdb = xdb.OrderByDescending(s => s.Models.FirstOrDefault()!=null ? s.Models.FirstOrDefault().Name:"");
                    break;
                case "Model":
                    xdb = xdb.OrderBy(s => s.Models.FirstOrDefault() != null ? s.Models.FirstOrDefault().Name : "");
                    break;
                case "country_desc":
                    xdb = xdb.OrderByDescending(s => s.OriginCountry);
                    break;
                case "Country":
                    xdb = xdb.OrderBy(s => s.OriginCountry);
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
            ViewBag.BrandPage = true;

            return View();
        }

        [HttpPost]
        public ActionResult Create(Brand x)
        {
            int AdminId = (int)Session["AdminId"];
            if (ModelState.IsValid)
            {

                Brand xdb = new Brand();


                xdb.Name = x.Name;
                xdb.OriginCountry = x.OriginCountry;
                xdb.AdminId = x.AdminId;
                xdb.PostDate = DateTime.Now;
                if (db.AdminSettings.FirstOrDefault(c => c.AdminId == AdminId).alwaysActive)
                {
                    xdb.isActive = true;
                }
                else
                {
                    xdb.isActive = false;
                }

                if (x.LogoFile == null)
                {
                    xdb.Logo = "Default2";
                }
                else
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + Regex.Replace(x.LogoFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    x.LogoFile.SaveAs(imagePath);
                    xdb.Logo = imageName;

                }

                db.Brand.Add(xdb);

                db.SaveChanges();
             
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelCreate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Name, "Brand");
                TempData["Create"] = "Create";
                return RedirectToAction("Index", "Brand");
            }
            TempData["Create-Error"] = "Create-Error";
            ViewBag.Admin = (int)Session["AdminId"];
            ViewBag.BrandPage = true;

            return View(x);
        }

        public ActionResult Update(int? id)
        {
            ViewBag.BrandPage = true;

            Brand x = db.Brand.Find(id);

            if (x == null)
            {
                return RedirectToAction("Oops", "Error", new { id = 404 });
            }

            return View(x);
        }

        [HttpPost]
        public ActionResult Update(Brand x)
        {
            if (ModelState.IsValid)
            {
                Brand xdb = db.Brand.Find(x.Id);

                xdb.Name = x.Name;
                xdb.OriginCountry = x.OriginCountry; 
                xdb.ModifiedDate = DateTime.Now;
                xdb.AdminModifiedId = (int)Session["AdminId"];

                if (x.LogoFile != null)
                {
                    string imageName = DateTime.Now.ToString("ssfff") + Regex.Replace(x.LogoFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.Logo);
                    System.IO.File.Delete(oldImagePath);

                    x.LogoFile.SaveAs(imagePath);
                    xdb.Logo = imageName;
                }


                db.Entry(xdb).State = EntityState.Modified;
                db.SaveChanges();

                int AdminId = (int)Session["AdminId"];
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelUpdate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Name, "Brand");


                TempData["Update"] = "Update";

                return RedirectToAction("Index", "Brand");

            }

            TempData["Update-Error"] = "Update-Error";
            ViewBag.BrandPage = true;

            return View(x);
        }

        public JsonResult Delete(int id)
        {
            Brand xdb = db.Brand.Find(id);
         

            string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.Logo);
            System.IO.File.Delete(oldImagePath);

            db.Brand.Remove(xdb);
            db.SaveChanges();
            int AdminId = (int)Session["AdminId"];
            string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelDelete(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Name, "Brand");

            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateActive(int? id)
        {
            var result = false;

            if (id != null)
            {
                Brand xdb = db.Brand.FirstOrDefault(w => w.Id == id);


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

            var xdb = db.Brand.Where(c => c.Id == id).Select(s => new
            {

                Id = s.Id,
                Admin = s.Admin.FullName,
                PostDate = s.PostDate,
                EditDate = s.ModifiedDate,
                EditAdmin = s.AdminModified.FullName,
                Image = s.Logo,
                Name = s.Name,
                Country = s.OriginCountry,
                Models = s.Models.ToList()
            }).FirstOrDefault();

            return Json(xdb, JsonRequestBehavior.AllowGet);
        }
    }
}