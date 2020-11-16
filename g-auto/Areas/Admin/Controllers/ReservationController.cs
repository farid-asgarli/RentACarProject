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
    public class ReservationController : Controller
    {
        // GET: Admin/Reservation
        DBContext db = new DBContext();
        public ActionResult Index()
        {
            ViewBag.ReservationPage = true;

            List<Reservation> xdb = db.Reservations.Include("Model").Include("Model.Brand").Include("Model.ModelImages").Include("Model.Admin").Include("Model.Reviews").Include("ReservationToFeatures").Include("ReservationToFeatures.FeatureSet").ToList();

            return View(xdb);
        }

        public ActionResult Details(int Id)
        {
            ViewBag.ReservationPage = true;



            Reservation xdb = db.Reservations.Include("Model").Include("Model.Brand").Include("User").Include("Model.ModelImages").Include("Model.Admin").Include("Model.Reviews").Include("ReservationToFeatures").Include("ReservationToFeatures.FeatureSet").FirstOrDefault(c=>c.Id==Id);

            return View(xdb);
        }

        public JsonResult ChargeReservation(int? id)
        {
            Reservation xdb = db.Reservations.FirstOrDefault(c => c.Id == id);

            xdb.isActive = true;
            xdb.isCancelled = false;
            xdb.isFinished = false;
            xdb.isPending = false;


            db.Entry(xdb).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();



            int AdminId = (int)Session["AdminId"];
            string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;

            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).reservationConfirm(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), id);

            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult CancelReservation(int? id)
        {
            int AdminId = (int)Session["AdminId"];

            Reservation xdb = db.Reservations.FirstOrDefault(c => c.Id == id);

            xdb.isActive = false;
            xdb.isCancelled = true;
            xdb.isFinished = false;
            xdb.isPending = false;

            xdb.CancelledAdminId = AdminId;
            xdb.CancelledDate = DateTime.Now;

            db.Entry(xdb).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();

            string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;

            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).reservationCancel(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), id);

            return Json(JsonRequestBehavior.AllowGet);
        }
    }

}