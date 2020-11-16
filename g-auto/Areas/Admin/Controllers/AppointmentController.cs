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
    public class AppointmentController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/Appointment
        public ActionResult Index()
        {
            ViewBag.AppointmentPage = true;

            List<ReservationService> xdb = db.ReservationServices.Include("Service").Include("User").ToList();

            return View(xdb);
        }

        public JsonResult ConfirmAppointment(int? id)
        {

            string response = "";

            ReservationService xdb = db.ReservationServices.FirstOrDefault(c => c.Id == id);

            xdb.isActive = true;
            xdb.isCancelled = false;
            xdb.isFinished = false;
            xdb.isPending = false;



            db.Entry(xdb).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();
            int AdminId = (int)Session["AdminId"];
            string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;

            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).appConfirm(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Id);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FinishAppointment(int? id)
        {

            string response = "";

            ReservationService xdb = db.ReservationServices.FirstOrDefault(c => c.Id == id);

            xdb.isActive = false;
            xdb.isCancelled = false;
            xdb.isFinished = true;
            xdb.isPending = false;



            db.Entry(xdb).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();
            int AdminId = (int)Session["AdminId"];
            string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;

            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).appFinish(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Id);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CancelAppointment(int? id)
        {

            string response = "";

            ReservationService xdb = db.ReservationServices.FirstOrDefault(c => c.Id == id);

            xdb.isActive = false;
            xdb.isCancelled = true;
            xdb.isFinished = false;
            xdb.isPending = false;



            db.Entry(xdb).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();
            int AdminId = (int)Session["AdminId"];
            string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;

            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).appCancel(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), id);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}