using g_auto.DAL;
using g_auto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace g_auto.Controllers
{
    
    public class CareersController : Controller
    {
        DBContext db = new DBContext();
        // GET: Careers
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
            ViewBag.CareersPage = true;
            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();
            ViewBag.Address = db.Layout.FirstOrDefault().Address;
            ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
            ViewBag.Email = db.Layout.FirstOrDefault().Email;
            ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
            ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;
            List<Vacancy> xdb = db.Vacancies.Where(c => c.isActive).ToList();

            return View(xdb);
        }

        public ActionResult Details(int id)
        {

            #region Cart list

            HttpCookie cookieCart = Request.Cookies ["Cart"];
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
            ViewBag.CareersPage = true;
            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();
            ViewBag.Address = db.Layout.FirstOrDefault().Address;
            ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
            ViewBag.Email = db.Layout.FirstOrDefault().Email;
            ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
            ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;
            Vacancy xdb = db.Vacancies.Include("Admin").Include("Admin.AdminSettings").FirstOrDefault(c => c.isActive&&c.Id==id);

            return View(xdb);
        }

        public ActionResult Application(int id)
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
            ViewBag.CareersPage = true;
            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();
            ViewBag.Address = db.Layout.FirstOrDefault().Address;
            ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
            ViewBag.Email = db.Layout.FirstOrDefault().Email;
            ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
            ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;
            Vacancy xdb = db.Vacancies.Include("Admin").Include("Admin.AdminSettings").FirstOrDefault(c => c.isActive && c.Id == id);

            return View(xdb);
        }
    }
}