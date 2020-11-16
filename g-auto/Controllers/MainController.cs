using g_auto.DAL;
using g_auto.Hubs;
using g_auto.Models;
using g_auto.VM;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace g_auto.Controllers
{
    public class MainController : Controller
    {
        DBContext db = new DBContext();
        // GET: Main


        public ActionResult ForgotPassword()
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
            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();
            ViewBag.Address = db.Layout.FirstOrDefault().Address;
            ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
            ViewBag.Email = db.Layout.FirstOrDefault().Email;
            ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
            ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;
            return View();
        }
       
        public ActionResult VerifyPasswordLink(VerifyPasswordLinkVM v)
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
            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();
            ViewBag.Address = db.Layout.FirstOrDefault().Address;
            ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
            ViewBag.Email = db.Layout.FirstOrDefault().Email;
            ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
            ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;

            if (db.VerificationCodes.FirstOrDefault(c => c.UserId == v.visitorid && c.VerCode == v.verc) != null)
            {

                VerificationCode code = db.VerificationCodes.FirstOrDefault(c => c.UserId == v.visitorid && c.VerCode == v.verc);
             
                TimeSpan span = DateTime.Now.Subtract(code.PostDate);

                if (span.TotalMinutes < 10)
                {
                    User xdb = db.User.Find(v.visitorid);

                    Session["OnPassUpd-" + xdb.Id] = true;

                    return View(xdb);
                }
                else
                {
                    db.VerificationCodes.Remove(code);


                    db.SaveChanges();
                    Session["InvalidLink"] = true;

                    return RedirectToAction("ForgotPassword", "Main");
                }

            }
            else
            {
                Session["InvalidLink"] = true;

                return RedirectToAction("ForgotPassword", "Main");

            }



        }

        [HttpPost]
        public ActionResult ForgotPassword(PasswordUpdateVM v)
        {
            if (db.User.FirstOrDefault(c => c.Email == v.Email) != null)
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

                ViewBag.Address = db.Layout.FirstOrDefault().Address;
                ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
                ViewBag.Email = db.Layout.FirstOrDefault().Email;
                ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
                ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;
                ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();

                User user = db.User.FirstOrDefault(c => c.Email == v.Email);

                foreach(VerificationCode item in db.VerificationCodes.Where(c => c.UserId == user.Id))
                {
                    db.VerificationCodes.Remove(item);
                }

                db.SaveChanges();

                VerificationCode xdb = new VerificationCode();

                xdb.PostDate = DateTime.Now;
                xdb.UserId = user.Id;

                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[64];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);

                xdb.VerCode = finalString;

                db.VerificationCodes.Add(xdb);
                db.SaveChanges();


                string FilePath = Server.MapPath("~/Assets/template/EmailTemplate.cshtml") ;
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();

                MailText = MailText.Replace("[RequestedEmail]", v.Email.Trim());
                MailText = MailText.Replace("[TargetWebsite]", HttpContext.Request.Url.GetLeftPart(UriPartial.Authority));
                MailText = MailText.Replace("[RequestedPasswordURL]", HttpContext.Request.Url.GetLeftPart(UriPartial.Authority) + "/Main/VerifyPasswordLink?verc=" + xdb.VerCode + "&visitorid=" + xdb.UserId);
                

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("autocarrenttm2020@gmail.com", "autocarrenttm2020@gmail.com");
                mail.To.Add(new MailAddress(v.Email));
                mail.Subject = "Update Password";
                mail.Body = MailText;
                //mail.Body = "<html><body><p> Hi there,</p ><p> Password update link has been requested for " + v.Email + ".</p><br></br><p> If you did not request the password change, simply ignore this mail. </p><br></br><p> Use the provieded link <a href='" + HttpContext.Request.Url.GetLeftPart(UriPartial.Authority) + "/Main/VerifyPasswordLink?verc=" + xdb.VerCode + "&visitorid=" + xdb.UserId + "'>here</a> to change your password</p><br></br><p> If you did not request password change, simply dismiss this mail. </p><br></br><p> If you have any questions, please do not hesitate to contact us again.</p><p> Sincerely,<br><strong> Autlines </strong></br ></p></body></html>";

                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("autocarrenttm2020@gmail.com", "@utoc@rRENT2020");

                smtp.Send(mail);


                Session["VerEmailSent"] = true;

                return View();

            }
            else
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

                ViewBag.Address = db.Layout.FirstOrDefault().Address;
                ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
                ViewBag.Email = db.Layout.FirstOrDefault().Email;
                ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
                ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;
                ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();

                ModelState.AddModelError("Email", "Email not found.");
                return View(v);
            }

           
        }
        [HttpPost]
        public ActionResult NewPasswordUpdate(User x)
        {

            if (x != null)
            {
                User xdb = db.User.Find(x.Id);

                if(Session["OnPassUpd-" + xdb.Id] != null)
                {
                    Session["OnPassUpd-" + xdb.Id] = null;
                }

                foreach (VerificationCode item in db.VerificationCodes.Where(c => c.UserId == xdb.Id))
                {
                    db.VerificationCodes.Remove(item);
                }

                db.SaveChanges();
                xdb.Password = Crypto.HashPassword(x.Password);

                db.Entry(xdb).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Login", "Main");
            }
            return View();
        }

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

            ViewBag.MainPage = true;
            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();
            ViewBag.Address = db.Layout.FirstOrDefault().Address;
            ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
            ViewBag.Email = db.Layout.FirstOrDefault().Email;
            ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
            ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;

            Home v = new Home();

            v.Sliders = db.Sliders.Where(c => c.isActive).ToList();
            v.Layout = db.Layout.Include("AboutBenefits").FirstOrDefault();
            v.Services = db.Service.Where(c => c.isActive).ToList();
            v.Experts = db.Expert.Where(c => c.isActive).ToList();
            v.About = db.About.FirstOrDefault();
            v.Brands = db.Brand.Include("Models").Where(c => c.isActive).ToList();
            v.Models = db.Model.Include("Brand").Include("ModelImages").Where(c=>c.isActive).ToList();
            v.Blogs = db.Blog.Include("BlogToCategory").Include("BlogToCategory.BlogCategory").Include("BlogToTags").Include("Comments").Include("Comments.Replies").Include("BlogToTags.Tag").ToList();
            v.Reservations = db.Reservations.ToList();
            v.Sales = db.Sales.ToList();
            v.Testimonials = db.Testimonials.Include("User").Where(c => c.isActive).ToList();
            v.Appointments = db.ReservationServices.ToList();


       
            return View(v);
        }

        public ActionResult Login()
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
            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();
            ViewBag.Address = db.Layout.FirstOrDefault().Address;
            ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
            ViewBag.Email = db.Layout.FirstOrDefault().Email;
            ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
            ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;
            return View();
        }

        public ActionResult Register()
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
            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();
            ViewBag.Address = db.Layout.FirstOrDefault().Address;
            ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
            ViewBag.Email = db.Layout.FirstOrDefault().Email;
            ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
            ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {

            if (db.User.FirstOrDefault(c => c.Email == user.Email) == null)
            {
                if (db.User.FirstOrDefault(c => c.Phone == user.Phone) == null)
                {
                    if (ModelState.IsValid)
                    {
                        user.IsRegistered = true;
                        user.isBlocked = false;
                        user.ProfilePicture = "Default";
                        user.ProfilePictureFile = null;
                        user.FullName = user.FullName;
                        user.Address = null;
                        user.PostDate = DateTime.Now;
                        user.Password = Crypto.HashPassword(user.Password);
                        user.LastIPAddress = Request.UserHostAddress;
                        user.LastEntranceTime = DateTime.Now;
                        user.EntranceCount = 0;

                        db.User.Add(user);
                        db.SaveChanges();

                        GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.All.newUser(DateTime.Now.ToString("HH:mm"), user.FullName, user.Id);


                        Session["User"] = user;
                        Session["UserId"] = user.Id;

                        return RedirectToAction("Index");
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
                    ViewBag.Products = products;
                    #endregion
                    ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();
                    ViewBag.Address = db.Layout.FirstOrDefault().Address;
                    ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
                    ViewBag.Email = db.Layout.FirstOrDefault().Email;
                    ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
                    ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;
                    return View(user);

                }
                else
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

                    ViewBag.Address = db.Layout.FirstOrDefault().Address;
                    ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
                    ViewBag.Email = db.Layout.FirstOrDefault().Email;
                    ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
                    ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;
                    ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();

                    ModelState.AddModelError("Phone", "Phone number is already in use.");

                    return View(user);
                }
            }
            else
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

                ViewBag.Address = db.Layout.FirstOrDefault().Address;
                ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
                ViewBag.Email = db.Layout.FirstOrDefault().Email;
                ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
                ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;
                ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();

                ModelState.AddModelError("Email", "Email is already in use.");

                return View(user);
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Login(UserLoginVM login)
        {
            if (ModelState.IsValid)
            {
                User user = db.User.FirstOrDefault(a => a.Email == login.Email);

                if (user != null)
                {
                    if (Crypto.VerifyHashedPassword(user.Password, login.Password) == true)
                    {
                        Session["User"] = user;
                        Session["UserId"] = user.Id;

                        HttpCookie cookie = new HttpCookie("hbsgnlrverusr");
                       

                        user.EntranceCount++;
                        user.LastIPAddress= Request.UserHostAddress;
                        user.LastEntranceTime = DateTime.Now;
                        db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        
                        db.Entry(user).Property(x => x.Email).IsModified = false;
                        db.Entry(user).Property(x => x.Phone).IsModified = false;
                        db.Entry(user).Property(x => x.FullName).IsModified = false;
                        db.Entry(user).Property(x => x.Address).IsModified = false;
                        db.Entry(user).Property(x => x.Password).IsModified = false;

                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();
                        ViewBag.Address = db.Layout.FirstOrDefault().Address;
                        ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
                        ViewBag.Email = db.Layout.FirstOrDefault().Email;
                        ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
                        ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;

                        ModelState.AddModelError("Password", "Email or Password is invalid.");

                        return View(login);
                    }
                }
                else
                {
                    ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();
                    ViewBag.Address = db.Layout.FirstOrDefault().Address;
                    ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
                    ViewBag.Email = db.Layout.FirstOrDefault().Email;
                    ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
                    ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;

                    ModelState.AddModelError("Password", "Email or Password is invalid.");
                    return View(login);
                }
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
            ViewBag.Products = products;
            #endregion

            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();
            ViewBag.Address = db.Layout.FirstOrDefault().Address;
            ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
            ViewBag.Email = db.Layout.FirstOrDefault().Email;
            ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
            ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;
            return View(login);
        }

    }
}