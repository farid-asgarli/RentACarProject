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
    public class NewsletterController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/Newsletter
        public ActionResult Index()
        {

            ViewBag.NewsletterPage = true;
            List<NewsLetter> xdb = db.NewsLetters.ToList();

           

            return View(xdb);
        }

        public JsonResult DeleteNews(int? id)
        {
            var response = "";

            NewsLetter xdb = db.NewsLetters.FirstOrDefault(c => c.Id == id);

            db.NewsLetters.Remove(xdb);


            db.SaveChanges();
            int AdminId = (int)Session["AdminId"];
            string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;

            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).newsCancel(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Email);

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}