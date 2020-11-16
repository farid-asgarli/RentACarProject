using g_auto.DAL;
using g_auto.Models;
using g_auto.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace g_auto.Controllers
{
    public class AboutGController : Controller
    {
        DBContext db = new DBContext();

        // GET: AboutG
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
            ViewBag.AboutPage = true;
            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();
            ViewBag.Address = db.Layout.FirstOrDefault().Address;
            ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
            ViewBag.Email = db.Layout.FirstOrDefault().Email;
            ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
            ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;


            Home v = new Home();


            v.About = db.About.FirstOrDefault(c=>c.isActive);
            v.Experts = db.Expert.Where(c=>c.isActive).ToList();
            v.Layout = db.Layout.FirstOrDefault();
            v.Services = db.Service.Where(c => c.isActive).ToList();
            v.Models = db.Model.Include("Brand").Include("ModelImages").Where(c => c.isActive).ToList();
            v.Reservations = db.Reservations.ToList();
            v.Sales = db.Sales.ToList();
            v.Appointments = db.ReservationServices.ToList();
            return View(v);
        }
    }
}