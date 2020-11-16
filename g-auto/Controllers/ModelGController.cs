using g_auto.DAL;
using g_auto.Hubs;
using g_auto.Models;
using g_auto.VM;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace g_auto.Controllers
{

    public class ModelGController : Controller
    {
        DBContext db = new DBContext();
        // GET: ModelG
        public ActionResult Grid(MainSearchVM vm)
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

            ViewBag.ModelPage = true;
            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();
            ViewBag.Address = db.Layout.FirstOrDefault().Address;
            ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
            ViewBag.Email = db.Layout.FirstOrDefault().Email;
            ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
            ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;

            var xdb = from x in db.Model.Where(c => c.isActive).Include("ModelImages").Include("Brand").OrderByDescending(c => c.PostDate)
                    select x;
      
            if (vm.Condition!=null)
            {
                xdb = xdb.Where(s => s.Transmission==vm.Transmission && s.EngineLayout==vm.EngineLayout && s.DriveTrain == vm.DriveTrain && s.Condition == vm.Condition && s.FuelType == vm.FuelType);
            }
           
            ViewBag.Brands = db.Brand.Where(c => c.isActive == true).Include("Models").Include("Models.ModelImages").ToList();
            ViewBag.ModelsCount = db.Model.Where(c => c.isActive).Count();

            return View(xdb.ToList());
        }

        public ActionResult List()
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

            ViewBag.ModelPage = true;

            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();
            ViewBag.Address = db.Layout.FirstOrDefault().Address;
            ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
            ViewBag.Email = db.Layout.FirstOrDefault().Email;
            ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
            ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;
            ModelVM v = new ModelVM();

            v.Models = db.Model.Where(c => c.isActive == true).Include("ModelImages").Include("Brand").ToList();
            v.Brands = db.Brand.Where(c => c.isActive == true).Include("Models").Include("Models.ModelImages").ToList();


            return View(v);
        }

        public ActionResult Details(int? id)
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
            ViewBag.ModelPage = true;

            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();
            ViewBag.Address = db.Layout.FirstOrDefault().Address;
            ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
            ViewBag.Email = db.Layout.FirstOrDefault().Email;
            ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
            ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;


            Model xdb = db.Model.Include("Admin").Include("Brand").Include("ModelImages").Include("Reviews").Include("Reviews.Replies").Include("Reviews.Replies.Admin").Include("Reviews.Replies.Admin.AdminSettings").Include("Reviews.User").FirstOrDefault(m => m.Id == id && m.isActive);

            if (Session["VCM-" + id] == null)
            {
                Session["VCM-" + id] = true;


                xdb.ViewCount = xdb.ViewCount + 1;

                db.Entry(xdb).State = EntityState.Modified;
                db.SaveChanges();
            }

            return View(xdb);

        }

        public ActionResult Reservation(int? id)
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

            ViewBag.ModelPage = true;

            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();
            ViewBag.Address = db.Layout.FirstOrDefault().Address;
            ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
            ViewBag.Email = db.Layout.FirstOrDefault().Email;
            ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
            ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;

            ReservationVM v = new ReservationVM();



            v.Model = db.Model.Include("Admin").Include("Brand").Include("ModelImages").Include("Reservations").FirstOrDefault(m => m.Id == id && m.isActive);
            v.FeatureSets = db.FeatureSet.ToList();


            return View(v);

        }

        
        [HttpPost]
        public ActionResult Reservation(ReservationVM v)
        {
            int uId = 0;

            string response = "false";

            if (Session["User"] != null)
            {
                uId = (int)Session["UserId"];
            }

            if ((Session["UserId"] == null && db.Reservations.FirstOrDefault() == null) || (Session["UserId"] == null && db.Reservations.FirstOrDefault(c => c.User.Email == v.Email && c.User.Phone == v.Phone && c.isFinished == false && c.isCancelled == false) == null))
            {


                if (v.Email != null && v.Phone != null)
                {
                    User u = new User();
                    u.FullName = v.FullName;
                    u.Email = v.Email;
                    u.Address = v.DeliveryAddress;
                    u.IsRegistered = false;
                    u.isBlocked = false;
                    u.Phone = v.Phone;
                    u.PostDate = DateTime.Now.AddHours(-1);

                    db.User.Add(u);
                    db.SaveChanges();



                    Reservation x = new Reservation();



                    //Price
                    decimal pr = v.ModelPrice * Convert.ToDecimal((v.EndDate).Subtract(v.StartDate).TotalDays);
                    for (int i = 0; i < v.isIncluded.Count(); i++)
                    {
                        if (v.isIncluded[i] == "yes")
                        {
                            pr += db.FeatureSet.Find(v.FeatureSetId[i]).Price;
                        }
                    }

                    x.Price = pr;
                    x.DeliveryAddress = v.DeliveryAddress;
                    x.EndDate = v.EndDate.Date;
                    x.isFinished = false;
                    x.isPending = true;
                    x.isActive = false;
                    x.isCancelled = false;
                    x.StartDate = v.StartDate.Date;
                    x.Time = v.Time;
                    x.UserId = u.Id;
                    x.ModelId = v.ModelId;
                    x.PostDate = DateTime.Now;



                    db.Reservations.Add(x);
                    db.SaveChanges();

                    for (int i = 0; i < v.isIncluded.Count(); i++)
                    {
                        if (v.isIncluded[i] == "yes")
                        {
                            ReservationToFeatures reservationToFeatures = new ReservationToFeatures();
                            reservationToFeatures.ReservationId = x.Id;
                            reservationToFeatures.FeatureSetId = v.FeatureSetId[i];

                            db.ReservationToFeatures.Add(reservationToFeatures);
                            db.SaveChanges();
                        }
                    }


                    db.SaveChanges();

                    GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.All.newRes(DateTime.Now.ToString("HH:mm"), x.Id);

                    Session["SuccessRes-" + x.Id] = true;



                    return Json(x.Id, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    response = "sessionerror";
                }


            }
            else if ((Session["User"] != null && db.Reservations.FirstOrDefault() == null) || (Session["User"] != null && db.Reservations.FirstOrDefault(c => c.ModelId == v.ModelId && c.UserId == uId && c.isFinished == false && c.isCancelled == false) == null))
            {
                Reservation x = new Reservation();



                //Price
                decimal pr = v.ModelPrice * Convert.ToDecimal((v.EndDate).Subtract(v.StartDate).TotalDays);
                for (int i = 0; i < v.isIncluded.Count(); i++)
                {
                    if (v.isIncluded[i] == "yes")
                    {
                        pr += db.FeatureSet.Find(v.FeatureSetId[i]).Price;
                    }
                }

                x.Price = pr;
                x.DeliveryAddress = v.DeliveryAddress;
                x.EndDate = v.EndDate.Date;
                x.isFinished = false;
                x.isPending = true;
                x.isActive = false;
                x.isCancelled = false;
                x.StartDate = v.StartDate.Date;
                x.Time = v.Time;
                x.UserId = uId;
                x.ModelId = v.ModelId;
                x.PostDate = DateTime.Now;



                db.Reservations.Add(x);
                db.SaveChanges();

                for (int i = 0; i < v.isIncluded.Count(); i++)
                {
                    if (v.isIncluded[i] == "yes")
                    {
                        ReservationToFeatures reservationToFeatures = new ReservationToFeatures();
                        reservationToFeatures.ReservationId = x.Id;
                        reservationToFeatures.FeatureSetId = v.FeatureSetId[i];

                        db.ReservationToFeatures.Add(reservationToFeatures);
                        db.SaveChanges();
                    }
                }


                db.SaveChanges();

                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.All.newRes(DateTime.Now.ToString("HH:mm"), x.Id);

                Session["SuccessRes-"+x.Id] = true;

           

                return Json(x.Id, JsonRequestBehavior.AllowGet);

            }


            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ResSuccess(int? id)
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

            ViewBag.ModelPage = true;

            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();
            ViewBag.Address = db.Layout.FirstOrDefault().Address;
            ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
            ViewBag.Email = db.Layout.FirstOrDefault().Email;
            ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
            ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;
            Reservation xdb = db.Reservations.Include("User").Include("Model").Include("ReservationToFeatures").Include("ReservationToFeatures.FeatureSet").FirstOrDefault(c => c.Id == id);

            return View(xdb);
        }

        public JsonResult Datepicker(int? id)
        {


            var dates = new List<DateTime>();

            foreach (Reservation item in db.Reservations.Where(c => c.ModelId == id))
            {
                for (var dt = item.StartDate; dt <= item.EndDate; dt = dt.AddDays(1))
                {
                    dates.Add(dt);

                }
            }


            var model = dates.ToList().Select(c => new
            {
                Day = c.Day,
                Month = c.Month,
                Year = c.Year

            });




            return Json(model, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ModelsList(int id)
        {
            if (id == 0)
            {
                var response = db.Model.Where(c => c.isActive == true).OrderByDescending(o => o.PostDate).Select(s => new
                {
                    Name = s.Name,
                    Picture = s.ModelImages.FirstOrDefault().Name,
                    Price = s.PriceDaily,
                    Year = s.Year,
                    Transmission = s.Transmission,
                    Engine = s.Engine,
                    FuelType=s.FuelType,
                    Id = s.Id

                }).ToList();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var response = db.Model.Where(c => c.isActive == true && c.BrandId == id).OrderByDescending(o => o.PostDate).Select(s => new
                {
                    Name = s.Name,
                    Picture = s.ModelImages.FirstOrDefault().Name,
                    Price = s.PriceDaily,
                    Year = s.Year,
                    Transmission = s.Transmission,
                    FuelType=s.FuelType,
                    Engine = s.Engine,
                    Id = s.Id

                }).ToList();

                return Json(response, JsonRequestBehavior.AllowGet);
            }

        }


        public JsonResult SortModelList(SortVM v)
        {


            if (v.Id != 0)
            {
                if (v.Data == "Name (A to Z)")
                {
                    var response = db.Model.Where(c => c.isActive == true && c.BrandId == v.Id).OrderBy(c => c.Name).Select(s => new
                    {
                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                        Engine = s.Engine,
                    FuelType=s.FuelType,
                        Id = s.Id

                    }).ToList();


                    return Json(response, JsonRequestBehavior.AllowGet);
                }
                else if (v.Data == "Name (Z to A)")
                {
                    var response = db.Model.Where(c => c.isActive == true && c.BrandId == v.Id).OrderByDescending(c => c.Name).Select(s => new
                    {

                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                    FuelType=s.FuelType,
                        Engine = s.Engine,
                        Id = s.Id

                    }).ToList();
                    return Json(response, JsonRequestBehavior.AllowGet);

                }
                else if (v.Data == "Price (Low to High)")
                {
                    var response = db.Model.Where(c => c.isActive == true && c.BrandId == v.Id).OrderBy(c => c.PriceDaily).Select(s => new
                    {

                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                    FuelType=s.FuelType,
                        Engine = s.Engine,
                        Id = s.Id

                    }).ToList();
                    return Json(response, JsonRequestBehavior.AllowGet);

                }
                else if (v.Data == "Price (High to Low)")
                {
                    var response = db.Model.Where(c => c.isActive == true && c.BrandId == v.Id).OrderByDescending(c => c.PriceDaily).Select(s => new
                    {

                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                    FuelType=s.FuelType,
                        Engine = s.Engine,
                        Id = s.Id

                    }).ToList();
                    return Json(response, JsonRequestBehavior.AllowGet);

                }

                else if (v.Data == "Year (Low to High)")
                {
                    var response = db.Model.Where(c => c.isActive == true && c.BrandId == v.Id).OrderBy(c => c.Year).Select(s => new
                    {

                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                    FuelType=s.FuelType,
                        Engine = s.Engine,
                        Id = s.Id

                    }).ToList();
                    return Json(response, JsonRequestBehavior.AllowGet);

                }
                else if (v.Data == "Year (High to Low)")
                {
                    var response = db.Model.Where(c => c.isActive == true && c.BrandId == v.Id).OrderByDescending(c => c.Year).Select(s => new
                    {

                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                    FuelType=s.FuelType,
                        Engine = s.Engine,
                        Id = s.Id

                    }).ToList();
                    return Json(response, JsonRequestBehavior.AllowGet);

                }
                else if (v.Data == "Default")
                {
                    var response = db.Model.Where(c => c.isActive == true && c.BrandId == v.Id).OrderByDescending(c => c.PostDate).Select(s => new
                    {

                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                    FuelType=s.FuelType,
                        Engine = s.Engine,
                        Id = s.Id

                    }).ToList();
                    return Json(response, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }
            }

            else
            {
                if (v.Data == "Name (A to Z)")
                {
                    var response = db.Model.Where(c => c.isActive == true ).OrderBy(c => c.Name).Select(s => new
                    {
                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                        Engine = s.Engine,
                    FuelType=s.FuelType,
                        Id = s.Id

                    }).ToList();


                    return Json(response, JsonRequestBehavior.AllowGet);
                }
                else if (v.Data == "Name (Z to A)")
                {
                    var response = db.Model.Where(c => c.isActive == true ).OrderByDescending(c => c.Name).Select(s => new
                    {

                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                        Engine = s.Engine,
                    FuelType=s.FuelType,
                        Id = s.Id

                    }).ToList();
                    return Json(response, JsonRequestBehavior.AllowGet);

                }
                else if (v.Data == "Price (Low to High)")
                {
                    var response = db.Model.Where(c => c.isActive == true ).OrderBy(c => c.PriceDaily).Select(s => new
                    {

                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                    FuelType=s.FuelType,
                        Transmission = s.Transmission,
                        Engine = s.Engine,
                        Id = s.Id

                    }).ToList();
                    return Json(response, JsonRequestBehavior.AllowGet);

                }
                else if (v.Data == "Price (High to Low)")
                {
                    var response = db.Model.Where(c => c.isActive == true ).OrderByDescending(c => c.PriceDaily).Select(s => new
                    {

                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                    FuelType=s.FuelType,
                        Engine = s.Engine,
                        Id = s.Id

                    }).ToList();
                    return Json(response, JsonRequestBehavior.AllowGet);

                }

                else if (v.Data == "Year (Low to High)")
                {
                    var response = db.Model.Where(c => c.isActive == true ).OrderBy(c => c.Year).Select(s => new
                    {

                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                    FuelType=s.FuelType,
                        Engine = s.Engine,
                        Id = s.Id

                    }).ToList();
                    return Json(response, JsonRequestBehavior.AllowGet);

                }
                else if (v.Data == "Year (High to Low)")
                {
                    var response = db.Model.Where(c => c.isActive == true ).OrderByDescending(c => c.Year).Select(s => new
                    {

                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                        Engine = s.Engine,
                    FuelType=s.FuelType,
                        Id = s.Id

                    }).ToList();
                    return Json(response, JsonRequestBehavior.AllowGet);

                }
                else if (v.Data == "Default")
                {
                    var response = db.Model.Where(c => c.isActive == true ).OrderByDescending(c => c.PostDate).Select(s => new
                    {

                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                    FuelType=s.FuelType,
                        Engine = s.Engine,
                        Id = s.Id

                    }).ToList();
                    return Json(response, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult ModelProperties(ModelPropertiesVM v)
        {

            if (v.BrandId != 0)
            {
                if (v.Data == "Name (A to Z)")
                {
                    var response = db.Model.Where(c => c.isActive == true && c.BrandId==v.BrandId && c.FuelType == v.FuelType && c.EngineLayout == v.EngineLayout && c.DriveTrain == v.DriveTrain && c.Transmission == v.Transmission && c.Condition == v.Condition).OrderBy(c => c.Name).Select(s => new
                    {
                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                    FuelType=s.FuelType,
                        Engine = s.Engine,
                        Id = s.Id

                    }).ToList();


                    return Json(response, JsonRequestBehavior.AllowGet);
                }
                else if (v.Data == "Name (Z to A)")
                {
                    var response = db.Model.Where(c => c.isActive == true && c.BrandId == v.BrandId && c.FuelType == v.FuelType && c.EngineLayout == v.EngineLayout && c.DriveTrain == v.DriveTrain && c.Transmission == v.Transmission && c.Condition == v.Condition).OrderByDescending(c => c.Name).Select(s => new
                    {

                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                    FuelType=s.FuelType,
                        Engine = s.Engine,
                        Id = s.Id

                    }).ToList();
                    return Json(response, JsonRequestBehavior.AllowGet);

                }
                else if (v.Data == "Price (Low to High)")
                {
                    var response = db.Model.Where(c => c.isActive == true && c.BrandId == v.BrandId && c.FuelType == v.FuelType && c.EngineLayout == v.EngineLayout && c.DriveTrain == v.DriveTrain && c.Transmission == v.Transmission && c.Condition == v.Condition).OrderBy(c => c.PriceDaily).Select(s => new
                    {

                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                    FuelType=s.FuelType,
                        Engine = s.Engine,
                        Id = s.Id

                    }).ToList();
                    return Json(response, JsonRequestBehavior.AllowGet);

                }
                else if (v.Data == "Price (High to Low)")
                {
                    var response = db.Model.Where(c => c.isActive == true && c.BrandId == v.BrandId && c.FuelType == v.FuelType && c.EngineLayout == v.EngineLayout && c.DriveTrain == v.DriveTrain && c.Transmission == v.Transmission && c.Condition == v.Condition).OrderByDescending(c => c.PriceDaily).Select(s => new
                    {

                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                        Engine = s.Engine,
                    FuelType=s.FuelType,
                        Id = s.Id

                    }).ToList();
                    return Json(response, JsonRequestBehavior.AllowGet);

                }

                else if (v.Data == "Year (Low to High)")
                {
                    var response = db.Model.Where(c => c.isActive == true && c.BrandId == v.BrandId && c.FuelType == v.FuelType && c.EngineLayout == v.EngineLayout && c.DriveTrain == v.DriveTrain && c.Transmission == v.Transmission && c.Condition == v.Condition).OrderBy(c => c.Year).Select(s => new
                    {

                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                        Engine = s.Engine,
                    FuelType=s.FuelType,
                        Id = s.Id

                    }).ToList();
                    return Json(response, JsonRequestBehavior.AllowGet);

                }
                else if (v.Data == "Year (High to Low)")
                {
                    var response = db.Model.Where(c => c.isActive == true && c.BrandId == v.BrandId && c.FuelType == v.FuelType && c.EngineLayout == v.EngineLayout && c.DriveTrain == v.DriveTrain && c.Transmission == v.Transmission && c.Condition == v.Condition).OrderByDescending(c => c.Year).Select(s => new
                    {

                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                    FuelType=s.FuelType,
                        Engine = s.Engine,
                        Id = s.Id

                    }).ToList();
                    return Json(response, JsonRequestBehavior.AllowGet);

                }
                else if (v.Data == "Default")
                {
                    var response = db.Model.Where(c => c.isActive == true && c.BrandId == v.BrandId && c.FuelType == v.FuelType && c.EngineLayout == v.EngineLayout && c.DriveTrain == v.DriveTrain && c.Transmission == v.Transmission && c.Condition == v.Condition).OrderByDescending(c => c.PostDate).Select(s => new
                    {

                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                    FuelType=s.FuelType,
                        Engine = s.Engine,
                        Id = s.Id

                    }).ToList();
                    return Json(response, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }
            }

            else
            {
                if (v.Data == "Name (A to Z)")
                {
                    var response = db.Model.Where(c => c.isActive == true && c.FuelType == v.FuelType && c.EngineLayout == v.EngineLayout && c.DriveTrain == v.DriveTrain && c.Transmission == v.Transmission && c.Condition == v.Condition).OrderBy(c => c.Name).Select(s => new
                    {
                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                    FuelType=s.FuelType,
                        Engine = s.Engine,
                        Id = s.Id

                    }).ToList();


                    return Json(response, JsonRequestBehavior.AllowGet);
                }
                else if (v.Data == "Name (Z to A)")
                {
                    var response = db.Model.Where(c => c.isActive == true && c.FuelType == v.FuelType && c.EngineLayout == v.EngineLayout && c.DriveTrain == v.DriveTrain && c.Transmission == v.Transmission && c.Condition == v.Condition).OrderByDescending(c => c.Name).Select(s => new
                    {

                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                    FuelType=s.FuelType,
                        Engine = s.Engine,
                        Id = s.Id

                    }).ToList();
                    return Json(response, JsonRequestBehavior.AllowGet);

                }
                else if (v.Data == "Price (Low to High)")
                {
                    var response = db.Model.Where(c => c.isActive == true && c.FuelType == v.FuelType && c.EngineLayout == v.EngineLayout && c.DriveTrain == v.DriveTrain && c.Transmission == v.Transmission && c.Condition == v.Condition).OrderBy(c => c.PriceDaily).Select(s => new
                    {

                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                    FuelType=s.FuelType,
                        Engine = s.Engine,
                        Id = s.Id

                    }).ToList();
                    return Json(response, JsonRequestBehavior.AllowGet);

                }
                else if (v.Data == "Price (High to Low)")
                {
                    var response = db.Model.Where(c => c.isActive == true && c.FuelType == v.FuelType && c.EngineLayout == v.EngineLayout && c.DriveTrain == v.DriveTrain && c.Transmission == v.Transmission && c.Condition == v.Condition).OrderByDescending(c => c.PriceDaily).Select(s => new
                    {

                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                    FuelType=s.FuelType,
                        Engine = s.Engine,
                        Id = s.Id

                    }).ToList();
                    return Json(response, JsonRequestBehavior.AllowGet);

                }

                else if (v.Data == "Year (Low to High)")
                {
                    var response = db.Model.Where(c => c.isActive == true && c.FuelType == v.FuelType && c.EngineLayout == v.EngineLayout && c.DriveTrain == v.DriveTrain && c.Transmission == v.Transmission && c.Condition == v.Condition).OrderBy(c => c.Year).Select(s => new
                    {

                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                    FuelType=s.FuelType,
                        Engine = s.Engine,
                        Id = s.Id

                    }).ToList();
                    return Json(response, JsonRequestBehavior.AllowGet);

                }
                else if (v.Data == "Year (High to Low)")
                {
                    var response = db.Model.Where(c => c.isActive == true && c.FuelType == v.FuelType && c.EngineLayout == v.EngineLayout && c.DriveTrain == v.DriveTrain && c.Transmission == v.Transmission && c.Condition == v.Condition).OrderByDescending(c => c.Year).Select(s => new
                    {

                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                    FuelType=s.FuelType,
                        Engine = s.Engine,
                        Id = s.Id

                    }).ToList();
                    return Json(response, JsonRequestBehavior.AllowGet);

                }
                else if (v.Data == "Default")
                {
                    var response = db.Model.Where(c => c.isActive == true && c.FuelType == v.FuelType && c.EngineLayout == v.EngineLayout && c.DriveTrain == v.DriveTrain && c.Transmission == v.Transmission && c.Condition == v.Condition).OrderByDescending(c => c.PostDate).Select(s => new
                    {

                        Name = s.Name,
                        Picture = s.ModelImages.FirstOrDefault().Name,
                        Price = s.PriceDaily,
                        Year = s.Year,
                        Transmission = s.Transmission,
                    FuelType=s.FuelType,
                        Engine = s.Engine,
                        Id = s.Id

                    }).ToList();
                    return Json(response, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult ModelSearch(string searchText)
        {
            var response = db.Model.Where(c => c.isActive == true && (c.Name.Contains(searchText)||c.Brand.Name.Contains(searchText))).OrderByDescending(o => o.PostDate).Select(s => new
                {
                    Name = s.Name,
                    Picture = s.ModelImages.FirstOrDefault().Name,
                    Price = s.PriceDaily,
                    Year = s.Year,
                    Transmission = s.Transmission,
                    Engine = s.Engine,
                    FuelType=s.FuelType,
                    Id = s.Id

                }).ToList();


            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}