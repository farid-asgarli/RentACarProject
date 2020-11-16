using g_auto.DAL;
using g_auto.Hubs;
using g_auto.Models;
using g_auto.VM;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace g_auto.Controllers
{
    public class ServiceGController : Controller
    {
        // GET: ServiceG
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
            ViewBag.Products = products;
            #endregion
            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList(); ViewBag.Address = db.Layout.FirstOrDefault().Address;
            ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
            ViewBag.Email = db.Layout.FirstOrDefault().Email;
            ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
            ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;
            ViewBag.ServicePage = true;
            List<Service> xdb = db.Service.Where(c => c.isActive).ToList();
            return View(xdb);
        }

        public ActionResult ServiceReservation(int id)
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
            ViewBag.Products = products;
            #endregion
            ViewBag.ServicePage = true;
            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();
            ViewBag.Address = db.Layout.FirstOrDefault().Address;
            ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
            ViewBag.Email = db.Layout.FirstOrDefault().Email;
            ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
            ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;
            ServiceVM v = new ServiceVM();

            v.Service = db.Service.Include("ServiceToInfo").Include("ServiceToInfo.ServiceInfo").Include("ServiceBenefits").Where(c => c.isActive).FirstOrDefault(c=>c.Id==id);
            v.Services = db.Service.Include("ServiceToInfo").Include("ServiceToInfo.ServiceInfo").Include("ServiceBenefits").Where(c=>c.isActive).ToList();

            if (Session["VCS-" + id] == null)
            {
                Session["VCS-" + id] = true;


                v.Service.ViewCount = v.Service.ViewCount + 1;

                db.Entry(v.Service).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return View(v);
        }

        public JsonResult Datepicker(int id)
        {

            var dates = new List<DateTime>();

            foreach (ReservationService item in db.ReservationServices.Where(c => c.ServiceId == id))
            {
                dates.Add(item.AppDate);
            }


            var model = dates.ToList().Select(c => new
            {
                Day = c.Day,
                Month = c.Month,
                Year = c.Year

            });




            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Reservation(ServiceReservationVM v)
        {
            int uId=0;

            string response = "false";

            if (Session["User"] != null)
            {
                uId = (int)Session["UserId"];
            }

            if ((Session["UserId"] == null && db.ReservationServices.FirstOrDefault() == null)||(Session["UserId"] == null && db.ReservationServices.FirstOrDefault(c=>c.User.Email==v.Email&&c.User.Phone==v.Phone && c.isFinished==false && c.isCancelled == false) ==null))
            {
                

               if(v.Email!=null && v.Phone != null)
                {
                    User u = new User();
                    u.FullName = v.FullName;
                    u.Email = v.Email;
                    u.IsRegistered = false;
                    u.isBlocked = false;
                    u.Phone = v.Phone;
                    u.PostDate = DateTime.Now.AddHours(-1);
                    u.Address = "";

                    db.User.Add(u);
                    db.SaveChanges();


                    ReservationService x = new ReservationService();

                    x.isFinished = false;
                    x.isPending = true;
                    x.isActive = false;
                    x.isCancelled = false;
                    x.Time = v.Time;
                    x.UserId = u.Id;
                    x.ServiceId = v.ServiceId;
                    x.PostDate = DateTime.Now;
                    x.AppDate = v.AppDate;

                    db.ReservationServices.Add(x);
                    db.SaveChanges();

                    GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.All.newApp(DateTime.Now.ToString("HH:mm"), x.Id);


                    response = "true";
                }
                else
                {
                    response = "sessionerror";
                }


            }
            else if ((Session["User"]!=null && db.ReservationServices.FirstOrDefault() == null) || (Session["User"] != null && db.ReservationServices.FirstOrDefault(c => c.ServiceId == v.ServiceId && c.UserId == uId&&c.isFinished==false&&c.isCancelled==false)==null))
            {
                ReservationService x = new ReservationService();

                x.isFinished = false;
                x.isPending = true;
                x.isCancelled = false;
                x.isActive = false;
                x.Time = v.Time;
                x.UserId = (int)Session["UserId"];
                x.ServiceId = v.ServiceId;
                x.PostDate = DateTime.Now;
                x.AppDate = v.AppDate;

                db.ReservationServices.Add(x);
                db.SaveChanges();
                response = "true";
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.All.newApp(DateTime.Now.ToString("HH:mm"), x.Id);

            }


            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}