using g_auto.Areas.Admin.Filter;
using g_auto.DAL;
using g_auto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace g_auto.Areas.Admin.Controllers
{
    [logout]

    public class SettingsController : Controller
    {
        DBContext db = new DBContext();

        // GET: Admin/Settings
        public ActionResult Index()
        {
            ViewBag.SettingsPage = true;

            int AdminId = (int)Session["AdminId"];

            AdminSettings xdb = db.AdminSettings.FirstOrDefault(c => c.AdminId == AdminId);

            return View(xdb);
        }

        public JsonResult UpdateSettings(AdminSettings x)
        {

            int AdminId = (int)Session["AdminId"];
            var result = false;

            if (x != null)
            {
                AdminSettings xdb = db.AdminSettings.FirstOrDefault(w => w.AdminId == AdminId);

                xdb.alwaysActive = x.alwaysActive;
                xdb.DisplayName = x.DisplayName;
                xdb.menuGrouping = x.menuGrouping;

                db.Entry(xdb).State = EntityState.Modified;
                db.SaveChanges();

                result = true;

            }




            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }

    
}