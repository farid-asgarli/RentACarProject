using g_auto.DAL;
using g_auto.Filter;
using g_auto.Hubs;
using g_auto.Models;
using g_auto.VM;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace g_auto.Controllers
{
    [UserLogout]
    public class UserGController : Controller
    {
        // GET: UserG
        DBContext db = new DBContext();
        public ActionResult Index()
        {
            #region Cart list

            HttpCookie cookieCart = Request.Cookies["Cart"];
            List<string> CartList = new List<string>();
            if (cookieCart != null)
            {
                CartList = cookieCart.Value.Split(',').ToList();
                CartList.RemoveAt(CartList.Count - 1);

                ViewBag.CartList = CartList;
                ViewBag.CartListCount = CartList.Count;
            }
            else
            {
                ViewBag.CartListCount = 0;
            }

            List<Product> products = new List<Product>();

            foreach (var item in CartList)
            {
                foreach (var prd in db.Products.Include("ProductImages").Include("Admin").Include("ProductToCategory").Include("ProductToCategory.ProductCategory").ToList())
                {
                    if (Convert.ToInt32(item.Split('-')[0]) == prd.Id)
                    {
                        prd.Count = Convert.ToDecimal(item.Split('-')[1]);
                        products.Add(prd);
                    }
                }

            }
            #endregion

            ViewBag.Products = products;

            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList(); ViewBag.Address = db.Layout.FirstOrDefault().Address;
            ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
            ViewBag.Email = db.Layout.FirstOrDefault().Email;
            ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
            ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;
            int userId = (int)Session["UserId"];

            User xdb = db.User.Include("Reservations").Include("Reservations.Model").Include("Reservations.Model.Brand").Include("Reservations.Model.ModelImages").Include("Sales").Include("Sales.Shipments").Include("Sales.Product").Include("Sales.Product.ProductImages").Include("Sales.Product.ProductToCategory").Include("Sales.Product.ProductToCategory.ProductCategory").Include("ReservationServices").Include("ReservationServices.User").Include("ReservationServices.Service").FirstOrDefault(c=>c.Id==userId);

            return View(xdb);
        }

        public ActionResult OrderDetails(int Id)
        {
            #region Cart list

            HttpCookie cookieCart = Request.Cookies["Cart"];
            List<string> CartList = new List<string>();
            if (cookieCart != null)
            {
                CartList = cookieCart.Value.Split(',').ToList();
                CartList.RemoveAt(CartList.Count - 1);

                ViewBag.CartList = CartList;
                ViewBag.CartListCount = CartList.Count;
            }
            else
            {
                ViewBag.CartListCount = 0;
            }

            List<Product> products = new List<Product>();

            foreach (var item in CartList)
            {
                foreach (var prd in db.Products.Include("ProductImages").Include("Admin").Include("ProductToCategory").Include("ProductToCategory.ProductCategory").ToList())
                {
                    if (Convert.ToInt32(item.Split('-')[0]) == prd.Id)
                    {
                        prd.Count = Convert.ToDecimal(item.Split('-')[1]);
                        products.Add(prd);
                    }
                }

            }
            #endregion

            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList(); ViewBag.Address = db.Layout.FirstOrDefault().Address;
            ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
            ViewBag.Email = db.Layout.FirstOrDefault().Email;
            ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
            ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;
            ViewBag.Products = products;
            Sale xdb = db.Sales.Include("Product").Include("Product.ProductImages").Include("Product.ReviewProducts").Include("Product.ProductToCategory").Include("Product.ProductToCategory.ProductCategory").Include("Shipments").Include("User").Include("User.ReviewProducts").FirstOrDefault(c => c.Id == Id);


            return View(xdb);
        }

        public ActionResult BookingDetails(int Id)
        {
            #region Cart list

            HttpCookie cookieCart = Request.Cookies["Cart"];
            List<string> CartList = new List<string>();
            if (cookieCart != null)
            {
                CartList = cookieCart.Value.Split(',').ToList();
                CartList.RemoveAt(CartList.Count - 1);

                ViewBag.CartList = CartList;
                ViewBag.CartListCount = CartList.Count;
            }
            else
            {
                ViewBag.CartListCount = 0;
            }

            List<Product> products = new List<Product>();

            foreach (var item in CartList)
            {
                foreach (var prd in db.Products.Include("ProductImages").Include("Admin").Include("ProductToCategory").Include("ProductToCategory.ProductCategory").ToList())
                {
                    if (Convert.ToInt32(item.Split('-')[0]) == prd.Id)
                    {
                        prd.Count = Convert.ToDecimal(item.Split('-')[1]);
                        products.Add(prd);
                    }
                }

            }
            #endregion

            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList(); ViewBag.Address = db.Layout.FirstOrDefault().Address;
            ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
            ViewBag.Email = db.Layout.FirstOrDefault().Email;
            ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
            ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;
            ViewBag.Products = products;
            Reservation xdb = db.Reservations.Include("Model").Include("Model.Brand").Include("Model.ModelImages").Include("Model.Reviews").Include("ReservationToFeatures").Include("ReservationToFeatures.FeatureSet").Include("User").Include("User.Reviews").FirstOrDefault(c => c.Id == Id);


            return View(xdb);
        }

        public JsonResult UpdateUserPassword(PasswordVM passvm )
        {
            if (Session["User"] != null)
            {
                int userId = (int)Session["UserId"];

                User xdb = db.User.Find(userId);

                if (Crypto.VerifyHashedPassword(xdb.Password, passvm.oldpass) == true)
                {
                    xdb.Password = Crypto.HashPassword(passvm.newpass);

                    db.Entry(xdb).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();


                    return Json("true", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("false", JsonRequestBehavior.AllowGet);
                }

            }


            return Json("sessionerror",JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateUser(User x)
        {
            var response = "false";

            if (Session["User"]!=null)
            {

               User xdb = db.User.FirstOrDefault(c => c.Id == x.Id);

                xdb.FullName = x.FullName;
                xdb.Address = x.Address;
                xdb.Email = x.Email;
                xdb.Phone = x.Phone;

                if (x.ProfilePictureFile != null)
                {
                    string imageName = DateTime.Now.ToString("ssfff") + Regex.Replace(x.ProfilePictureFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.ProfilePicture);
                    System.IO.File.Delete(oldImagePath);

                    x.ProfilePictureFile.SaveAs(imagePath);
                    xdb.ProfilePicture = imageName;
                }


                db.Entry(xdb).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                response = "true";


            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProfImage(int id)
        {

            User xdb = db.User.FirstOrDefault(c => c.Id == id);

            var response = xdb.ProfilePicture;


            return Json(response, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult SaveFile(int id)
        {
           User xdb = db.User.FirstOrDefault(c => c.Id == id);



            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)

                {

                    string imageName = DateTime.Now.ToString("ssfff") + Regex.Replace(file.FileName, "[^A-Za-z0-9.]", "");

                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    file.SaveAs(Server.MapPath($"/Uploads/{imageName}"));

                    xdb.ProfilePicture = imageName;


                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.ProfilePicture);
                    System.IO.File.Delete(oldImagePath);

                    db.Entry(xdb).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                }
            }
            return Json(true);
        }


        public JsonResult PostProductReview(ReviewProduct x)
        {
            string response = "false";

            if (db.ReviewProduct.FirstOrDefault() == null || db.ReviewProduct.FirstOrDefault(c => c.ProductId == x.ProductId && c.UserId == x.UserId) == null)
            {
                ReviewProduct xdb = new ReviewProduct();

                xdb.Content = x.Content;
                xdb.Rating = x.Rating;
                xdb.ProductId = x.ProductId;
                xdb.PostedDate = DateTime.Now;
                xdb.UserId = x.UserId;
                xdb.SaleId = x.SaleId;

                db.ReviewProduct.Add(xdb);
                db.SaveChanges();

                User usr = (User)Session["User"];

                //GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.All.postReview(usr.FullName, DateTime.Now.ToString("HH:mm"), x.Product.Name, "Product");

                response = "true";
            }


            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PostModelReview(Review x)
        {
            string response = "false";


            if (db.Reviews.FirstOrDefault()==null||db.Reviews.FirstOrDefault(c=>c.ModelId==x.ModelId && c.UserId==x.UserId)==null)
            {
                Review xdb = new Review();

                xdb.Content = x.Content;
                xdb.Rating = x.Rating;
                xdb.ModelId = x.ModelId;
                xdb.PostedDate = DateTime.Now;
                xdb.UserId = x.UserId;
                xdb.ReservationId = x.ReservationId;

                db.Reviews.Add(xdb);
                db.SaveChanges();
                User usr = (User)Session["User"];

                //GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.All.postReview(usr.FullName, DateTime.Now.ToString("HH:mm"), x.Model.Name ,"Model");

                response = "true";
            }


            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CancelApp(int id)
        {
            string response = "false";

            ReservationService xdb = db.ReservationServices.Find(id);

            if (xdb.isPending)
            {
                db.ReservationServices.Remove(xdb);

                db.SaveChanges();

                response = "true";
            }


            return Json(response, JsonRequestBehavior.AllowGet);
        }



        public JsonResult PostTestimonial(Testimonial x)
        {
            if (Session["User"] != null)
            {

                int UserId = (int)Session["UserId"];

                User user = db.User.FirstOrDefault(c => c.Id == UserId);

                if(db.Testimonials.FirstOrDefault(c=>c.UserId==UserId)==null ||(db.Testimonials.FirstOrDefault(c=>c.UserId==UserId) != null && db.Testimonials.Where(c=>c.UserId==UserId).Count() < 3 ))
                {
                    Testimonial xdb = new Testimonial();

                    xdb.Content = x.Content;
                    xdb.UserId = UserId;
                    xdb.PostedDate = DateTime.Now;

                    db.Testimonials.Add(xdb);
                    db.SaveChanges();

                    User usr = (User)Session["User"];

                    GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.All.postTestimonial(usr.FullName, DateTime.Now.ToString("HH:mm"));

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