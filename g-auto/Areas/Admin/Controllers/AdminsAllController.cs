using g_auto.Areas.Admin.Filter;
using g_auto.DAL;
using g_auto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace g_auto.Areas.Admin.Controllers
{
    [logout]
    public class AdminsAllController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/AdminsAll
        public ActionResult Index()
        {

            

            int AdminId = (int)Session["AdminId"];

            db.Admin.Find(AdminId);

            List<Models.Admin> xdb = db.Admin.Where(c=>c.Id!=AdminId).ToList();

            return View(xdb);
        }

        public JsonResult ResetPassword(int id)
        {
            int AdminId = (int)Session["AdminId"];

            if (db.Admin.FirstOrDefault(c => c.Id == AdminId).hasPrivelege)
            {
                Models.Admin xdb =  db.Admin.Find(id);

                xdb.Password = Crypto.HashPassword("123456");

                db.Entry(xdb).State = System.Data.Entity.EntityState.Modified;


                db.SaveChanges();

                return Json("true", JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult DeleteAdmin(int id)
        {
            int AdminId = (int)Session["AdminId"];

            if (db.Admin.FirstOrDefault(c => c.Id == AdminId).hasPrivelege)
            {
                Models.Admin xdb = db.Admin.Find(id);

                xdb.isBlocked=true;

                db.Entry(xdb).State = EntityState.Modified;

                db.SaveChanges();

                return Json("true", JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult UnblockAdmin(int id)
        {
            int AdminId = (int)Session["AdminId"];

            if (db.Admin.FirstOrDefault(c => c.Id == AdminId).hasPrivelege)
            {
                Models.Admin xdb = db.Admin.Find(id);

                xdb.isBlocked = false;

                db.Entry(xdb).State = EntityState.Modified;

                db.SaveChanges();

                return Json("true", JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult ProvidePrivelege(int? id)
        {
            int AdminId = (int)Session["AdminId"];

            if (db.Admin.FirstOrDefault(c => c.Id == AdminId).hasPrivelege)
            {

                Models.Admin xdb = db.Admin.FirstOrDefault(w => w.Id == id);


                if (xdb.hasPrivelege == false)
                {
                    xdb.hasPrivelege = true;
                }
                else
                {
                    xdb.hasPrivelege = false;

                }

                db.Entry(xdb).State = EntityState.Modified;
                db.SaveChanges();
                return Json("true", JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }



            
        }
    }
}