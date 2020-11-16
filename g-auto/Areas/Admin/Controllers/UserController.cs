using g_auto.Areas.Admin.Filter;
using g_auto.DAL;
using g_auto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace g_auto.Areas.Admin.Controllers
{
    [logout]
    public class UserController : Controller
    {
        // GET: Admin/User
        DBContext db = new DBContext();
        public ActionResult Index()
        {
            ViewBag.UserPage = true;

            List<User> xdb = db.User.ToList(); 

            return View(xdb);
        }
        public ActionResult Details(int? id)
        {
            ViewBag.UserPage = true;

            User xdb = db.User.Include("Sales").Include("Sales.Product")
                .Include("Sales.Product.ProductImages").Include("Reservations").Include("Sales.Product.ProductToCategory").Include("Sales.Product.ProductToCategory.ProductCategory")
                .Include("Reservations.Model").Include("Reservations.Model.ModelImages").Include("Reservations.Model.Brand")
                .Include("Reviews").Include("Reviews.Model").Include("ReviewProducts")
                .Include("ReviewProducts.Product").Include("ReservationServices")
                .Include("ReservationServices.Service").Include("Comments").FirstOrDefault(c => c.Id == id);

            return View(xdb);
        }

        public JsonResult Delete(int id)
        {
           

            if (Session["AdminId"]!=null)
            {
                User xdb = db.User.Find(id);

                if (xdb.Sales != null)
                {
                    foreach (Sale item in db.Sales.Where(c => c.UserId == id))
                    {
                        db.Sales.Remove(item);

                    }
                }

                if (xdb.ReservationServices != null)
                {
                    foreach (ReservationService item in db.ReservationServices.Where(c => c.UserId == id))
                    {
                        db.ReservationServices.Remove(item);
                    }
                }

                if (xdb.Reservations != null)
                {
                    foreach (Reservation item in db.Reservations.Where(c => c.UserId == id))
                    {
                        db.Reservations.Remove(item);
                    }
                }

                if (xdb.Comments != null)
                {
                    foreach (Reply item in db.Replies.Where(c => c.UserId == id))
                    {
                        db.Replies.Remove(item);

                    }
                }

                if (xdb.Replies != null)
                {
                    foreach (Comment item in db.Comment.Where(c => c.UserId == id))
                    {
                        db.Comment.Remove(item);
                    }
                }
                if (xdb.Testimonials != null)
                {
                    foreach (Testimonial item in db.Testimonials.Where(c => c.UserId == id))
                    {
                        db.Testimonials.Remove(item);
                    }
                }

                if (xdb.ReviewProducts != null)
                {
                    foreach (ReviewProduct item in db.ReviewProduct.Where(c => c.UserId == id))
                    {
                        db.ReviewProduct.Remove(item);
                    }
                }

                if (xdb.Reviews != null)
                {
                    foreach (Review item in db.Reviews.Where(c => c.UserId == id))
                    {
                        db.Reviews.Remove(item);
                    }
                }

                db.SaveChanges();

                db.User.Remove(xdb);


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