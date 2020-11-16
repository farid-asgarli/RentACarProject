using g_auto.Areas.Admin.Filter;
using g_auto.DAL;
using g_auto.Hubs;
using g_auto.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace g_auto.Areas.Admin.Controllers
{
    [logout]
    public class SaleController : Controller
    {
        DBContext db = new DBContext();   
        // GET: Admin/Sale
        public ActionResult Index()
        {
            List<Sale> xdb = db.Sales.Include("Product").Include("Product.ProductImages").Include("Product.Admin").Include("Product.ProductToCategory").Include("Product.ProductToCategory.ProductCategory").Include("User").Include("Shipments").ToList();

            ViewBag.SalePage = true;

            return View(xdb);
        }
        public ActionResult Details(int Id)
        {
            ViewBag.SalePage = true;

            Sale xdb = db.Sales.Include("Product").Include("Product.ProductImages").Include("Product.Admin").Include("Product.ProductToCategory").Include("Product.ReviewProducts").Include("Product.ProductToCategory.ProductCategory").Include("User").Include("Shipments").Include("Admin").FirstOrDefault(c => c.Id == Id);


            return View(xdb);

        }

        public JsonResult CancelOrder(int Id)
        {
            int AdminId = (int)Session["AdminId"];


            Sale xdb = db.Sales.FirstOrDefault(c => c.Id == Id);

            xdb.isCancelled = true;
            xdb.isPending = false;
            xdb.isActive = false;
            xdb.isFinished = false;
            xdb.CancelledAdminId = AdminId;
            xdb.CancelledDate = DateTime.Now;

            db.Entry(xdb).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            
            string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;

            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).orderCancel(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), Id);



            var response = xdb;



            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ConfirmOrder(int Id)
        {

            Sale xdb = db.Sales.FirstOrDefault(c => c.Id == Id);

            xdb.isCancelled = false;
            xdb.isActive = true;
            xdb.isFinished = false;
            xdb.isPending = false;

            db.Entry(xdb).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            var response = xdb.Id;
            int AdminId = (int)Session["AdminId"];
            string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;

            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).orderConfirm(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), Id);

            return Json(response, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CreateShipment(int id)
        {
            Sale xdb = db.Sales.Include("User").FirstOrDefault(c => c.Id == id);

            Shipment x = new Shipment();

            x.IsCanceled = false;
            x.CreatedDate = DateTime.Now;
            x.ESTDelivery = DateTime.Now.AddDays(2);
            x.Address = xdb.User.Address;
            x.IsDelivered = true;
            x.SaleId = xdb.Id;
            x.IsReady = false;  
            

            db.Shipments.Add(x);
            db.SaveChanges();
            int AdminId = (int)Session["AdminId"];
            string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;

            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).orderFinish(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), id);

            var response = db.Shipments.Where(c => c.Id == x.Id).Select(s => new
            {
                Id = s.Id,
                ESTDay = s.ESTDelivery.Value.Day,
                ESTMonth = s.ESTDelivery.Value.Month,
                ESTYear = s.ESTDelivery.Value.Year,
                ESTHour = s.ESTDelivery.Value.Hour,
                ESTMinute=s.ESTDelivery.Value.Minute,
                Day = s.CreatedDate.Value.Day,
                Month = s.CreatedDate.Value.Month,
                Year = s.CreatedDate.Value.Year,
                Hour = s.CreatedDate.Value.Hour,
                Minute = s.CreatedDate.Value.Minute
            }).ToList();

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}