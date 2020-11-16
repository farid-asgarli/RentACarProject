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
    public class ServiceInfoController : Controller
    {
        // GET: Admin/ServiceInfo
        DBContext db = new DBContext();
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.ServiceInfoPage = true;
            //List<ServiceInfo> xdb = db.ServiceInfo.Include("ServiceToInfo").Include("ServiceToInfo.Service").Include("Admin").ToList();
            //return View(xdb);


            ViewBag.NameSortParam = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.ServiceSortParam = sortOrder == "Service" ? "service_desc" : "Service";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "";

            var xdb = from x in db.ServiceInfo.Include("ServiceToInfo").Include("ServiceToInfo.Service").Include("Admin")
                      select x;
            if (!String.IsNullOrEmpty(searchString))
            {
                xdb = xdb.Where(s => s.Title.ToString().Contains(searchString)
                                       || s.ServiceToInfo.FirstOrDefault().Service.Title.ToString().Contains(searchString));
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
                case "service_desc":
                    xdb = xdb.OrderByDescending(s => s.ServiceToInfo.FirstOrDefault() != null ? s.ServiceToInfo.FirstOrDefault().Service.Title : "");
                    break;
                case "Service":
                    xdb = xdb.OrderBy(s => s.ServiceToInfo.FirstOrDefault() != null ? s.ServiceToInfo.FirstOrDefault().Service.Title : "");
                    break;
                default:
                    xdb = xdb.OrderByDescending(s => s.PostDate);
                    break;

            }
            return View(xdb.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.ServiceInfoPage = true;
            ViewBag.Admin = (int)Session["AdminId"];
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ServiceInfo x)
        {

            if (ModelState.IsValid)
            {
                ServiceInfo xdb = new ServiceInfo();
                xdb.AdminId = x.AdminId;
                xdb.PostDate = DateTime.Now;
                xdb.Content = x.Content;
                xdb.Title = x.Title;

                db.ServiceInfo.Add(xdb);
                db.SaveChanges();
                int AdminId = (int)Session["AdminId"];
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelCreate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Title, "Service Info");

                TempData["Create"] = "Create";


                return RedirectToAction("Index", "ServiceInfo");

            }
            ViewBag.ServiceInfoPage = true;

            TempData["Create-Error"] = "Create-Error";

            ViewBag.Admin = (int)Session["AdminId"];
            return View(x);
        }

        public ActionResult Update(int Id)
        {

            ViewBag.ServiceInfoPage = true;
            ServiceInfo x = db.ServiceInfo.Find(Id);

            if (x == null)
            {
                return RedirectToAction("Oops", "Error", new { id = 404 });
            }

            return View(x);

        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(ServiceInfo x)
        {
            if (ModelState.IsValid)
            {
                ServiceInfo xdb = db.ServiceInfo.FirstOrDefault(c => c.Id == x.Id);

                xdb.Content = x.Content;
                xdb.Title = x.Title;
                xdb.ModifiedDate = DateTime.Now;
                xdb.AdminModifiedId = (int)Session["AdminId"];

                db.Entry(xdb).State = EntityState.Modified;


                db.SaveChanges();
                int AdminId = (int)Session["AdminId"];
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelUpdate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Title, "Service Info");

                TempData["Update"] = "Update";
                return RedirectToAction("Index");


            }
            TempData["Update-Error"] = "Update-Error";
            ViewBag.ServiceInfoPage = true;

            return View(x);
        }

        public JsonResult Delete(int id)
        {
            ServiceInfo xdb = db.ServiceInfo.Find(id);

            db.ServiceInfo.Remove(xdb);
            db.SaveChanges();

            int AdminId = (int)Session["AdminId"];
            string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelDelete(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Title, "Service Info");

            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModalDetails(int id)
        {
            var xdb = db.ServiceInfo.Where(c => c.Id == id).Select(s => new
            {

                Id = s.Id,
                Name = s.Title,
                Content = s.Content,
                Admin = s.Admin.FullName,
                PostDate = s.PostDate,
                EditDate = s.ModifiedDate,
                EditAdmin = s.AdminModified.FullName,

            }).FirstOrDefault();

            return Json(new { xdb, content = HttpUtility.HtmlDecode(xdb.Content)}, JsonRequestBehavior.AllowGet);
        }
    }
}
