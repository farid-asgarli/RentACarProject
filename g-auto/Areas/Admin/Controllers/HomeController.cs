using g_auto.Areas.Admin.Filter;
using g_auto.DAL;
using g_auto.Hubs;
using g_auto.Models;
using g_auto.ViewModels;
using Microsoft.AspNet.SignalR;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace g_auto.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        DBContext db = new DBContext();
        [logout]
        public ActionResult Index()
        {
            ViewBag.DashboardPage = true;

            Dashboard v = new Dashboard();

            v.Admins = db.Admin.ToList();
            v.Appointments = db.ReservationServices.Include("User").Include("Service").ToList();
            v.Products = db.Products.ToList();
            v.Reservations = db.Reservations.Include("Model").Include("Model.ModelImages").ToList();
            v.Sales = db.Sales.Include("Product").Include("Product.ProductToCategory").Include("Product.ProductToCategory.ProductCategory").Include("Product.Admin").ToList();
            v.Models = db.Model.ToList();
            v.Blogs = db.Blog.ToList();
            v.Brands = db.Brand.ToList();
            v.Experts = db.Expert.ToList();
            v.Gallery = db.GalleryImage.ToList();
            v.Services = db.Service.ToList();
            v.Sliders = db.Sliders.ToList();
            v.ModifiedBlogs = db.Blog.Include("AdminModified").Where(c => c.ModifiedDate.Value != null).ToList();
            v.ModifiedBrands = db.Brand.Include("AdminModified").Where(c => c.ModifiedDate.Value != null).ToList();
            v.ModifiedModels = db.Model.Include("AdminModified").Where(c => c.ModifiedDate.Value != null).ToList();
            v.ModifiedExperts = db.Expert.Include("AdminModified").Where(c => c.ModifiedDate.Value != null).ToList();
            v.ModifiedProducts = db.Products.Include("AdminModified").Where(c => c.ModifiedDate.Value != null).ToList();
            v.ModifiedServices = db.Service.Include("AdminModified").Where(c => c.ModifiedDate.Value != null).ToList();
            v.ModifiedSlider = db.Sliders.Include("AdminModified").Where(c => c.ModifiedDate.Value != null).ToList();
            v.ModifiedGallery= db.GalleryImage.Include("AdminModified").Where(c => c.ModifiedDate.Value != null).ToList();


            return View(v);
        }


        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(TempAccessCodes x)
        {
            if (db.TempAccessCodes.FirstOrDefault(c => c.Code == x.Code) != null)
            {

                TempAccessCodes code = db.TempAccessCodes.FirstOrDefault(c => c.Code == x.Code);

                TimeSpan span = DateTime.Now.Subtract(code.PostDate);

                if (span.TotalMinutes < 10)
                {
                    Session["UserRegister"] = true;




                    db.TempAccessCodes.Remove(db.TempAccessCodes.FirstOrDefault(c => c.Code == x.Code));
                    db.SaveChanges();


                    return RedirectToAction("SettingsSetupPage", "Home");
                }
                else
                {

                    db.TempAccessCodes.Remove(db.TempAccessCodes.FirstOrDefault(c => c.Code == x.Code));
                    db.SaveChanges();

                    ModelState.AddModelError("Code", "Invalid Access Code.");
                    return View();
                }

            }
            else
            {
                ModelState.AddModelError("Code", "Invalid Access Code.");
                return View();
            }


      
        }

        public ActionResult LogOut()
        {
            Session["Admin"] = null;
            Session["AdminId"] = null;

            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult Login(ViewModels.LoginPage l)
        {


            if (ModelState.IsValid)
            {
                Models.Admin user = db.Admin.FirstOrDefault(u => u.Email == l.Email);


                if (user != null)
                {
                    if ((Crypto.VerifyHashedPassword(user.Password, l.Password)))
                    {
                        if (user.isBlocked == false)
                        {
                            Session["Admin"] = user;
                            Session["AdminId"] = user.Id;

                            HttpCookie cookie = new HttpCookie("hbsgnlrver");
                            cookie.Value = user.Id.ToString();
                            cookie.Expires = DateTime.Now.AddYears(1);
                            Response.SetCookie(cookie);



                            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.All.checkOnline(DateTime.Now.ToString("HH:mm"), user.FullName, user.ProfilePicture);

                            user.EntranceCount++;
                            user.LastIPAddress = Request.UserHostAddress;
                            user.LastEntranceTime = DateTime.Now;
                            db.Entry(user).State = System.Data.Entity.EntityState.Modified;

                            db.SaveChanges();

                            int UserId = (int)Session["AdminId"];

                            if (db.Layout.FirstOrDefault() == null)
                            {
                                return RedirectToAction("InitialSetupPage", "Home");

                            }
                            else if (db.AdminSettings.FirstOrDefault(c => c.AdminId == user.Id) == null)
                            {
                                AdminSettings ax = new AdminSettings();
                                ax.DisplayName = ((Models.Admin)Session["Admin"]).FullName;
                                ax.AdminId = (int)Session["AdminId"];
                                ax.alwaysActive = false;


                                db.AdminSettings.Add(ax);
                                db.SaveChanges();

                            }
                            else if (db.AdminSettings.FirstOrDefault(c => c.AdminId == user.Id) == null && db.Layout.FirstOrDefault() == null)
                            {
                                return RedirectToAction("InitialSetupPage", "Home");

                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");


                            }
                        }
                        else
                        {
                            ModelState.AddModelError("Password", "Your account is blocked.");
                            return View(l);
                        }



                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Credentials are invalid.");
                        return View(l);
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "Credentials are invalid.");
                    return View(l);
                }
            }
            return View(l);


        }
        [logout]
        public ActionResult InitialSetupPage()
        {
            int AdminId = (int)Session["AdminId"];

            Models.Admin xdb = db.Admin.Find(AdminId);


            return View(xdb);
        }

       

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult InitialSetup()
        {
            int AdminId = (int)Session["AdminId"];

            Layout x = new Layout();

            x.expertToggle = true;
            x.blogToggle = true;
            x.modelToggle = true;
            x.serviceToggle = true;
            x.testimonialToggle = true;
            x.sliderToggle = true;
            x.contactToggle = true;
            x.aboutToggle = true;


            x.expertCount = 0;
            x.blogCount = 0;
            x.modelCount = 0;
            x.serviceCount = 0;
            x.testiominalCount = 0;
            x.sliderCount = 0;

            x.About_Content = "";
            x.About_Title = "";
            x.About_CEO = "";

            x.Contact_Content = "";
            x.Contact_Title = "";

            x.Promo_Content = "";
            x.Promo_Link = "";

            x.About_Image = "Default2";
            x.Logo = "Default2";
            x.LogoFooter= "Default2"; 
            x.Signature = "Default2";
            x.Promo_Image = "Default2";


            x.LogoFile = null;
            x.LogoFooterFile = null;
            x.About_ImageFile = null;
            x.SignatureFile = null;
            x.Promo_ImageFile = null;

            x.Address = "";
            x.Email = "companymail@mail.com";
            x.Phone = "(000) 000-0000";

            db.Layout.Add(x);
            db.SaveChanges();

            if (db.AdminSettings.FirstOrDefault(c => c.AdminId == AdminId) == null)
            {
                AdminSettings ax = new AdminSettings();
                ax.DisplayName = ((Models.Admin)Session["Admin"]).FullName;
                ax.AdminId = (int)Session["AdminId"];
                ax.alwaysActive = false;
                ax.menuGrouping = false;


                db.AdminSettings.Add(ax);
                db.SaveChanges();


             

                Models.Admin xdb = db.Admin.Find(AdminId);

                if (xdb.ProfilePicture == null)
                {
                    xdb.ProfilePicture = "Default2";

                }

                db.Entry(xdb).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }



            var result = true;

            return Json(result, JsonRequestBehavior.AllowGet);
        }


       
        public ActionResult SettingsSetupPage()
        {

            return View();
        }

        public JsonResult SettingsSetup(Models.Admin x)
        {
           if (Session["UserRegister"]!= null)
            {
                Models.Admin xdb = new Models.Admin();

                xdb.Email = x.Email;
                xdb.Phone = x.Phone;
                xdb.Password = Crypto.HashPassword(x.Password);
                xdb.FullName = x.FullName;
                xdb.EntranceCount=1;
                xdb.LastIPAddress = Request.UserHostAddress;
                xdb.LastEntranceTime = DateTime.Now;
                xdb.isBlocked = false;

                if (x.ProfilePictureFile != null)
                {
                    string imageName = DateTime.Now.ToString("ssfff") + Regex.Replace(x.ProfilePictureFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    x.ProfilePictureFile.SaveAs(imagePath);
                    xdb.ProfilePicture = imageName;
                }
                else
                {
                    xdb.ProfilePicture = "default-admin-avatar.png";
                }

                db.Admin.Add(xdb);
                db.SaveChanges();

                AdminSettings ax = new AdminSettings();
                ax.DisplayName = xdb.FullName;
                ax.AdminId = xdb.Id;
                ax.alwaysActive = false;
                ax.menuGrouping = false;

                db.AdminSettings.Add(ax);
                db.SaveChanges();



                Session["UserRegister"] = null;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.All.checkReg(DateTime.Now.ToString("HH:mm"), xdb.FullName, xdb.ProfilePicture);

                return Json("true",JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("false",JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EmailMatchCheck(string email)
        {

            var response = "false";

            if (db.Admin.FirstOrDefault(c => c.Email == email) == null)
            {
                response = "true";
            }
            else
            {
                response = "false";
            }


            return Json(response,JsonRequestBehavior.AllowGet);
        }
    }
}