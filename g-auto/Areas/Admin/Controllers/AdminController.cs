using g_auto.Areas.Admin.Filter;
using g_auto.DAL;
using g_auto.Models;
using g_auto.VM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace g_auto.Areas.Admin.Controllers
{
    [logout]
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        DBContext db = new DBContext();
        public ActionResult Index(int? id)
        {
            Models.Admin xdb = db.Admin.Find(id);

            ViewBag.CancelledSales = db.Sales.Include("Product").Include("Product.ProductImages").Where(c => c.CancelledAdminId == id && c.isCancelled);
            ViewBag.CancelledReservations = db.Reservations.Include("Model").Include("Model.ModelImages").Where(c => c.CancelledAdminId == id && c.isCancelled);
            ViewBag.Reservations = db.Reservations.Include("Model").Include("Model.ModelImages").Where(c => c.Model.AdminId==id&&c.isCancelled==false);
            ViewBag.Sales = db.Sales.Include("Product").Include("Product.ProductImages").Where(c => c.Product.AdminId==id&&c.isCancelled==false);

            return View(xdb);
        }

        public ActionResult Help()
        {


            return View();
        }

        public JsonResult UpdateAdminProfile(Models.Admin x)
        {
          

            if (x != null)
            {
                Models.Admin xdb = db.Admin.FirstOrDefault(w => w.Id == x.Id);

                xdb.Phone = x.Phone;
                xdb.Email = x.Email;
                xdb.FullName = x.FullName;

                if (x.ProfilePictureFile != null)
                {
                    string imageName = DateTime.Now.ToString("ssfff") + x.ProfilePictureFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.ProfilePicture);
                    System.IO.File.Delete(oldImagePath);

                    x.ProfilePictureFile.SaveAs(imagePath);
                    xdb.ProfilePicture = imageName;
                }


                db.Entry(xdb).State = EntityState.Modified;
                db.SaveChanges();


                return Json(xdb, JsonRequestBehavior.AllowGet);

            }

            return Json("false", JsonRequestBehavior.AllowGet);


        }



        public JsonResult SetupAdminProfile(Models.Admin x)
        {
            var result = false;

            if (x != null)
            {
                Models.Admin xdb = db.Admin.FirstOrDefault(w => w.Id == x.Id);


                xdb.FullName = x.FullName;
                xdb.Phone = x.Phone;
                xdb.Email = x.Email;


                if (x.ProfilePictureFile != null && x.ProfilePicture != "Default2")
                {
                    string imageName = DateTime.Now.ToString("ssfff") + x.ProfilePictureFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.ProfilePicture);
                    System.IO.File.Delete(oldImagePath);

                    x.ProfilePictureFile.SaveAs(imagePath);
                    xdb.ProfilePicture = imageName;
                }

                db.Entry(xdb).State = EntityState.Modified;
                db.SaveChanges();

                result = true;

            }



            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetupAdminProfilePicture(Models.Admin x)
        {
            var result = false;

            if (x != null)
            {
                Models.Admin xdb = db.Admin.FirstOrDefault(w => w.Id == x.Id);



                if (x.ProfilePictureFile != null && x.ProfilePicture!= "Default2")
                {
                    string imageName = DateTime.Now.ToString("ssfff") + x.ProfilePictureFile.FileName;
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.ProfilePicture);
                    System.IO.File.Delete(oldImagePath);

                    x.ProfilePictureFile.SaveAs(imagePath);
                    xdb.ProfilePicture = imageName;
                }


                db.Entry(xdb).State = EntityState.Modified;
                db.SaveChanges();

                result = true;

            }



            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateAdminPassword(PasswordVM passvm)
        {
            if (Session["Admin"] != null)
            {
                int userId = (int)Session["AdminId"];

                Models.Admin xdb = db.Admin.Find(userId);

                if (Crypto.VerifyHashedPassword(xdb.Password, passvm.oldpass) == true)
                {
                    xdb.Password = Crypto.HashPassword(passvm.newpass);

                    db.Entry(xdb).State = EntityState.Modified;
                    db.SaveChanges();


                    return Json("true", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("false", JsonRequestBehavior.AllowGet);
                }

            }


            return Json("sessionerror", JsonRequestBehavior.AllowGet);
        }
    }
}