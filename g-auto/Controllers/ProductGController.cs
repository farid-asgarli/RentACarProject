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
    public class ProductGController : Controller
    {
        DBContext db = new DBContext();
        // GET: ProductG
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
            ViewBag.Products = products;

            ViewBag.ProductPage = true;

            ProductVM v = new ProductVM();
            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();

            v.Products = db.Products.Include("ProductImages").Include("Admin").Include("ProductToCategory").Include("ProductToCategory.ProductCategory").Include("Admin").Include("Sales").OrderByDescending(c => c.PostDate).ToList();
            v.Categories = db.ProductCategories.Include("ProductToCategory").Include("ProductToCategory.Product").Include("Admin").ToList();

            return View(v);
        }

        public ActionResult Details(int id)
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
            ViewBag.Products = products;

            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();

            ViewBag.ProductPage = true;

            ProductVM v = new ProductVM();

            v.Products = db.Products.Include("ProductImages").Include("Admin").Include("ProductToCategory").Include("ProductToCategory.ProductCategory").Include("Admin").ToList();
            v.Categories = db.ProductCategories.Include("ProductToCategory").Include("ProductToCategory.Product").Include("Admin").ToList();
            v.Product = db.Products.Include("ProductImages").Include("Admin").Include("ProductToCategory").Include("ProductToCategory.ProductCategory").Include("Admin").Include("ReviewProducts").Include("ReviewProducts.Replies").Include("ReviewProducts.Replies.Admin").Include("ReviewProducts.Replies.Admin.AdminSettings").Include("ReviewProducts.User").FirstOrDefault(c => c.Id == id);

            if (Session["VCP-" + id] == null)
            {
                Session["VCP-" + id] = true;


                v.Product.ViewCount = v.Product.ViewCount + 1;

                db.Entry(v.Product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return View(v);
        }

        public ActionResult ShoppingCart()
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
            # endregion
            ViewBag.Address = db.Layout.FirstOrDefault().Address;
            ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
            ViewBag.Email = db.Layout.FirstOrDefault().Email;
            ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
            ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;
            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();

            ViewBag.Products = products;

            return View(products);


        }

        public ActionResult CheckOut()
        {
            if (Session["User"] != null)
            {
                int userId = (int)Session["UserId"];

                ViewBag.User = db.User.Find(userId);


            }


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
            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();
            ViewBag.Address = db.Layout.FirstOrDefault().Address;
            ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
            ViewBag.Email = db.Layout.FirstOrDefault().Email;
            ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
            ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;
            ViewBag.Products = products;

            return View(products);
        }

        //[HttpPost]
        public JsonResult CheckOutAjax(CheckOutVM check)
        {
            //Call API
            bool APIResponse = true;
            var response = "true";

            foreach (var item2 in check.ProductId)
            {
                Product product = db.Products.Find(item2);
                int index = check.ProductId.ToList().IndexOf(item2);

                if (index < product.Amount && product.isActive)
                {
                }
                else
                {
                    APIResponse = false;
                    response = "false";
                }

            }

            if (APIResponse == true)
            {
                int UserId;
                if (Session["UserId"] == null)
                {
                    User user = new User();
                    user.FullName = check.FullName;
                    user.Address = check.Address;
                    user.Email = check.Email;
                    user.Phone = check.Phone;
                    user.PostDate = DateTime.Now.AddHours(-1);
                    user.IsRegistered = false;
                    user.isBlocked = false;
                    user.ProfilePicture = "Default";

                    db.User.Add(user);
                    db.SaveChanges();
                    UserId = user.Id;
                }
                else
                {
                    UserId = (int)Session["UserId"];
                }

                List<Sale> salesAjax = new List<Sale>() ;

                //Sell product
                foreach (var item in check.ProductId)
                {
                    Product product = db.Products.Find(item);
                    int index = check.ProductId.ToList().IndexOf(item);

                    Sale sale = new Sale();
                    sale.ProductId = item;
                    sale.Amount = check.ProductCount[index];
                    sale.UserId = UserId;
                    sale.PostDate = DateTime.Now;
                    sale.Price = product.Price;
                    sale.OrderNote = check.OrderNote;
                    sale.IsRefunded = false;
                    sale.isRefundRequested = false;
                    sale.isActive = false;
                    sale.isCancelled = false;
                    sale.isFinished = false;
                    sale.isPending = true;

                    db.Sales.Add(sale);
                    salesAjax.Add(sale);

                    db.SaveChanges();


                    GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.All.newOrder(DateTime.Now.ToString("HH:mm"), sale.Id);

                    product.Amount -= check.ProductCount[index];
                    db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                }

                

                db.SaveChanges();

                if (Request.Cookies["Cart"] != null)
                {
                    Response.Cookies["Cart"].Expires = DateTime.Now.AddDays(-1);
                }





                var succesproduct = salesAjax.Select(s => new
                {
                    SaleId = s.Id,
                    ProductId = s.ProductId,
                    Amount = s.Amount,
                    Name = s.Product.Name,
                    Price = s.Product.Price,
                    TotalPrice = s.Price,
                    Month = s.PostDate.Month,
                    Day = s.PostDate.Day,
                    Year = s.PostDate.Year,



                }).ToList();

               

                return Json(succesproduct, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(response, JsonRequestBehavior.AllowGet);
            }

          

        }

        public JsonResult AddToCart(int? id, decimal? count)
        {
            string response = "";
            if (id != null && count != null)
            {
                if (Request.Cookies["Cart"] != null)
                {
                    string oldList = Request.Cookies["Cart"].Value;
                    HttpCookie cookie = new HttpCookie("Cart");
                    cookie.Value = oldList;
                    List<string> cartList = oldList.Split(',').ToList();
                    cartList.RemoveAt(cartList.Count - 1);

                    string cartElement = cartList.FirstOrDefault(c => Convert.ToInt32(c.Split('-')[0]) == id);

                    if (cartElement == null)
                    {
                        cookie.Value += id + "-" + count + ",";
                        Request.Cookies["Cart"].Expires = DateTime.Now.AddYears(1);
                        Response.Cookies.Add(cookie);
                        response = "success-true";
                    }
                    else
                    {
                        cartList.Remove(cartElement);

                        if (cartList.Count() > 0)
                        {
                            cookie.Value = string.Join(",", cartList) + ",";
                            cookie.Value += id + "-" + count + ",";
                        }
                        else
                        {
                            cookie.Value = id + "-" + count + ",";
                        }

                        Request.Cookies["Cart"].Expires = DateTime.Now.AddYears(1);
                        Response.Cookies.Add(cookie);
                        response = "success-false";
                    }
                }
                else
                {
                    HttpCookie cookie = new HttpCookie("Cart");

                    cookie.Expires = DateTime.Now.AddYears(1);
                    cookie.Value += id + "-" + count + ",";
                    Response.Cookies.Add(cookie);
                    response = "success-true";
                }
            }
            else
            {
                response = "error";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveFromCart(int? id)
        {
            string response = "";
            if (id != null)
            {
                string oldList = Request.Cookies["Cart"].Value;
                HttpCookie cookie = new HttpCookie("Cart");
                List<string> cartList = oldList.Split(',').ToList();
                cartList.RemoveAt(cartList.Count - 1);

                string cartElement = cartList.FirstOrDefault(c => Convert.ToInt32(c.Split('-')[0]) == id);
                if (cartElement != null)
                {
                    cartList.Remove(cartElement);

                    if (cartList.Count() > 0)
                    {
                        cookie.Value = string.Join(",", cartList) + ",";
                    }
                    else
                    {
                        cookie.Value = "";
                    }

                    Request.Cookies["Cart"].Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Add(cookie);
                    response = "success-true";
                }

            }
            else
            {
                response = "error";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveFromCartAll()
        {
            string response = "";

            if (Request.Cookies["Cart"] != null)
            {
                Response.Cookies["Cart"].Expires = DateTime.Now.AddDays(-1);
            }


            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateProductStatus()
        {
            string response = "";

            if (Request.Cookies["ConditionCheck"] == null)
            {

                HttpCookie cookie = new HttpCookie("ConditionCheck");

                cookie.Expires = DateTime.Now.AddHours(1);
                cookie.Value = "ConditionCheck";
                Response.Cookies.Add(cookie);

                foreach (Product item in db.Products)
                {
                    if ((DateTime.Now - item.PostDate).TotalDays > 5)
                    {
                        item.isNewlyAdded = false;

                        db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        item.isNewlyAdded = true;
                        db.Entry(item).State = System.Data.Entity.EntityState.Modified;

                    }
                }


                db.SaveChanges();
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProductsLoad(int id)
        {
            var response = db.ProductToCategory.Where(c => c.Product.isActive && c.ProductCategoryId == id).Select(s => new
            {
                Amount =s.Product.Amount,
                ProductId = s.Product.Id,
                Price = s.Product.Price,
                Name = s.Product.Name,
                Date = s.Product.PostDate,
                Picture = s.Product.ProductImages.FirstOrDefault().Name,
                Condition = s.Product.Condition,
                Category = s.Product.ProductToCategory.FirstOrDefault().ProductCategory.Name,
                IsNew = s.Product.isNewlyAdded


            }).ToList();

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProductSearch(string searchData)
        {
            var response = db.Products.Where(c => c.isActive).Where(c => c.Name.Contains(searchData) || c.About.Contains(searchData) || c.Desc.Contains(searchData) || c.ProductToCategory.FirstOrDefault().ProductCategory.Name.Contains(searchData)).Select(s => new
            {
                Amount = s.Amount,
                ProductId = s.Id,
                Price = s.Price,
                Name = s.Name,
                Date = s.PostDate,
                Picture = s.ProductImages.FirstOrDefault().Name,
                Condition = s.Condition,
                Category = s.ProductToCategory.FirstOrDefault().ProductCategory.Name,
                IsNew = s.isNewlyAdded

            }).ToList();





            return Json(response, JsonRequestBehavior.AllowGet);


        }

        public JsonResult ProductData()
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

            var response = products.Where(c => c.isActive).Select(s => new
            {
                Name = s.Name,
                Price = s.Price,
                Count = s.Count,
                Id = s.Id,
                Image = s.ProductImages.FirstOrDefault().Name
            }).ToList();


            return Json(response, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CheckOutSuccess()
        {
            return View();
        }
    }
}