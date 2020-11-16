using g_auto.DAL;
using g_auto.Hubs;
using g_auto.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace g_auto.Controllers
{
    public class ContactGController : Controller
    {
        // GET: ContactG
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
            ViewBag.Address = db.Layout.FirstOrDefault().Address;
            ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
            ViewBag.Email = db.Layout.FirstOrDefault().Email;
            ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
            ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;

            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();

            ViewBag.Products = products;
            ViewBag.ContactPage = true;

            Layout xdb = db.Layout.FirstOrDefault();

            return View(xdb);
        }

        public JsonResult ContactSupport(Message x)
        {

            string response = "false";


            if (x != null)
            {
                Message xdb = new Message();

                xdb.Content = x.Content;
                xdb.Name = x.Name;
                xdb.Email = x.Email;
                xdb.Phone = x.Phone;
                xdb.Subject = x.Subject;
                xdb.PostDate = DateTime.Now;
                xdb.isRead = false;
         

                db.Message.Add(xdb);

                db.SaveChanges();


                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.All.newMessage(xdb.Name, DateTime.Now.ToString("HH:mm"), xdb.Subject);

                response = "true";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubscribeNews(NewsLetter x)
        {
            string response = "false";

            if (db.NewsLetters.FirstOrDefault()==null || db.NewsLetters.FirstOrDefault(c=>c.Email == x.Email)==null)
            {
                NewsLetter xdb = new NewsLetter();
                xdb.Email = x.Email;
                xdb.PostDate = DateTime.Now;

                db.NewsLetters.Add(xdb);
                db.SaveChanges();

                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.All.newSubscription(DateTime.Now.ToString("HH:mm"), xdb.Email);
                response = "true";
            }


            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}