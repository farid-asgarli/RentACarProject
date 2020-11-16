using g_auto.Areas.Admin.Filter;
using g_auto.DAL;
using g_auto.Hubs;
using g_auto.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace g_auto.Areas.Admin.Controllers
{
    [logout]
    public class FeatureSetController : Controller
    {
        // GET: Admin/FeatureSet
        DBContext db = new DBContext();
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.FeatureSetPage = true;

            ViewBag.NameSortParam = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.ServiceSortParam = sortOrder == "Service" ? "service_desc" : "Service";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "";
            ViewBag.PriceSortParam = sortOrder == "Price" ? "price_desc" : "Price";

            var xdb = from x in db.FeatureSet.Include("ReservationToFeatures").Include("ReservationToFeatures.Reservation").Include("ReservationToFeatures.Reservation.Model").Include("Admin")
                      select x;
            if (!String.IsNullOrEmpty(searchString))
            {
                xdb = xdb.Where(s => s.Title.ToString().Contains(searchString)
                                       || s.Content.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Name":
                    xdb = xdb.OrderBy(s => s.Title);
                    break;

                case "name_desc":
                    xdb = xdb.OrderByDescending(s => s.Title);
                    break;
                case "Price":
                    xdb = xdb.OrderBy(s => s.Price);
                    break;

                case "price_desc":
                    xdb = xdb.OrderByDescending(s => s.Price);
                    break;
                case "Date":
                    xdb = xdb.OrderBy(s => s.PostDate);
                    break;
                case "date_desc":
                    xdb = xdb.OrderByDescending(s => s.PostDate);
                    break;
                case "service_desc":
                    xdb = xdb.OrderByDescending(s => s.ReservationToFeatures.FirstOrDefault() != null ? s.ReservationToFeatures.FirstOrDefault().Reservation.Id : 0);
                    break;
                case "Service":
                    xdb = xdb.OrderBy(s => s.ReservationToFeatures.FirstOrDefault() != null ? s.ReservationToFeatures.FirstOrDefault().Reservation.Id : 0);
                    break;
                default:
                    xdb = xdb.OrderByDescending(s => s.PostDate);
                    break;

            }
            return View(xdb.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.FeatureSetPage = true;
            ViewBag.Admin = (int)Session["AdminId"];
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FeatureSet x)
        {

            if (ModelState.IsValid)
            {
                FeatureSet xdb = new FeatureSet();
                xdb.AdminId = x.AdminId;
                xdb.PostDate = DateTime.Now;
                xdb.Content = x.Content;
                xdb.Price = x.Price;
                xdb.Title = x.Title;
                

                db.FeatureSet.Add(xdb);
                db.SaveChanges();
                int AdminId = (int)Session["AdminId"];
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelCreate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Title, "Feature Set");
                TempData["Create"] = "Create";


                return RedirectToAction("Index", "FeatureSet");

            }
            ViewBag.ServiceInfoPage = true;

            TempData["Create-Error"] = "Create-Error";

            ViewBag.Admin = (int)Session["AdminId"];
            return View(x);
        }

        public ActionResult Update(int Id)
        {

            ViewBag.FeatureSetPage = true;
            FeatureSet x = db.FeatureSet.Find(Id);

            if (x == null)
            {
                return RedirectToAction("Oops", "Error", new { id = 404 });
            }

            return View(x);

        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(FeatureSet x)
        {
            if (ModelState.IsValid)
            {
                FeatureSet xdb = db.FeatureSet.FirstOrDefault(c => c.Id == x.Id);

                xdb.Content = x.Content;
                xdb.Title = x.Title;
                xdb.Price = x.Price;
                xdb.ModifiedDate = DateTime.Now;
                xdb.AdminModifiedId = (int)Session["AdminId"];

                db.Entry(xdb).State = EntityState.Modified;


                db.SaveChanges();
                int AdminId = (int)Session["AdminId"];
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelUpdate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Title, "Feature Set");


                TempData["Update"] = "Update";
                return RedirectToAction("Index");


            }
            TempData["Update-Error"] = "Update-Error";
            ViewBag.FeatureSetPage = true;

            return View(x);
        }

        public JsonResult Delete(int id)
        {
            FeatureSet xdb = db.FeatureSet.Find(id);
           
            db.FeatureSet.Remove(xdb);
            db.SaveChanges();
            int AdminId = (int)Session["AdminId"];
            string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelDelete(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Title, "Feature Set");
            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModalDetails(int id)
        {
            var xdb = db.FeatureSet.Where(c => c.Id == id).Select(s => new
            {

                Id = s.Id,
                Name = s.Title,
                Content = s.Content,
                Price = s.Price,
                Admin = s.Admin.FullName,
                PostDate = s.PostDate,
                EditDate = s.ModifiedDate,
                EditAdmin = s.AdminModified.FullName,

            }).FirstOrDefault();

            return Json(new { xdb, content = HttpUtility.HtmlDecode(xdb.Content) }, JsonRequestBehavior.AllowGet);
        }
    }
}