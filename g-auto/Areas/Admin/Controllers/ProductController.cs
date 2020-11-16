using g_auto.Areas.Admin.Filter;
using g_auto.DAL;
using g_auto.Hubs;
using g_auto.Models;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace g_auto.Areas.Admin.Controllers
{
    [logout]
    public class ProductController : Controller
    {
        // GET: Admin/Product
        DBContext db = new DBContext();
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.ProductPage = true;

            ViewBag.Admin = (int)Session["AdminId"];


            //List<Product> products = db.Products.Include("ProductImages").Include("Admin").Include("ProductToCategory").Include("ProductToCategory.ProductCategory").Include("Admin").ToList();
            //return View(products);


            ViewBag.NameSortParam = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.CatSortParam = sortOrder == "Cat" ? "cat_desc" : "Cat";
            ViewBag.AmountSortParam = sortOrder == "Amount" ? "amount_desc" : "Amount";
            ViewBag.PriceSortParam = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "";
            ViewBag.IsSortParam = sortOrder == "Is" ? "is_desc" : "Is";

            var xdb = from x in db.Products.Include("ProductImages").Include("Admin").Include("ProductToCategory").Include("ProductToCategory.ProductCategory").Include("Admin")
                      select x;
            if (!String.IsNullOrEmpty(searchString))
            {
                xdb = xdb.Where(s => s.Name.ToString().Contains(searchString)
                                       || s.ProductToCategory.FirstOrDefault().Product.Name.ToString().Contains(searchString)
                                      || s.Amount.ToString().Contains(searchString) 
                                      || s.Price.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Name":
                    xdb = xdb.OrderBy(s => s.Name.ToString());
                    break;

                case "name_desc":
                    xdb = xdb.OrderByDescending(s => s.Name.ToString());
                    break;

                case "Amount":
                    xdb = xdb.OrderBy(s => s.Amount);
                    break;

                case "name_amount":
                    xdb = xdb.OrderByDescending(s => s.Amount);
                    break;
                case "Price":
                    xdb = xdb.OrderBy(s => s.Count);
                    break;

                case "price_desc":
                    xdb = xdb.OrderByDescending(s => s.Price.ToString());
                    break;

                case "Date":
                    xdb = xdb.OrderBy(s => s.PostDate);
                    break;
                case "date_desc":
                    xdb = xdb.OrderByDescending(s => s.PostDate);
                    break;
                case "cat_desc":
                    xdb = xdb.OrderByDescending(s => s.ProductToCategory.FirstOrDefault() != null ? s.ProductToCategory.FirstOrDefault().ProductCategory.Name.ToString() : "");
                    break;
                case "Cat":
                    xdb = xdb.OrderBy(s => s.ProductToCategory.FirstOrDefault() != null ? s.ProductToCategory.FirstOrDefault().ProductCategory.Name.ToString() : "");
                    break;
                
                case "is_desc":
                    xdb = xdb.OrderByDescending(s => s.isActive);
                    break;
                case "Is":
                    xdb = xdb.OrderBy(s => s.isActive);
                    break;
                default:
                    xdb = xdb.OrderByDescending(s => s.PostDate);
                    break;

            }
            return View(xdb.ToList());


        }

        public ActionResult Create()
        {
            ViewBag.Admin = (int)Session["AdminId"];
            ViewBag.Categories = db.ProductCategories.ToList();
            ViewBag.ProductPage = true;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Product x)
        {
            int AdminId = (int)Session["AdminId"];
            if (ModelState.IsValid)
            {
                Product xdb = new Product();
                xdb.About = x.About;
                xdb.AdminId = (int)Session["AdminId"];
                xdb.Amount = x.Amount;
                xdb.Desc = x.Desc;
                xdb.Name = x.Name;
                xdb.Price = x.Price;
                xdb.AdminId = x.AdminId;
                xdb.Condition = x.Condition;
                xdb.PostDate = DateTime.Now;
                if (db.AdminSettings.FirstOrDefault(c => c.AdminId == AdminId).alwaysActive)
                {
                    xdb.isActive = true;
                }
                else
                {
                    xdb.isActive = false;
                }
                xdb.isNewlyAdded = true;

                db.Products.Add(xdb);
                db.SaveChanges();

                if (x.CategoryId != null)
                {
                    foreach (var item in x.CategoryId)
                    {
                        ProductToCategory w = new ProductToCategory();
                        w.ProductId = xdb.Id;
                        w.ProductCategoryId = item;

                        db.ProductToCategory.Add(w);
                        db.SaveChanges();
                    }
                }



                if (x.ImageFile != null) {
                    foreach (HttpPostedFileBase image in x.ImageFile)
                    {
                        if (image == null)
                        {
                            xdb.ProductImages = null;
                        }
                        else
                        {
                            string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssffff") + (Regex.Replace(image.FileName, "[^A-Za-z0-9.]", ""));
                            string imagePath = Path.Combine(Server.MapPath("~/Uploads"), imageName);

                            image.SaveAs(imagePath);

                            ProductImages productImage = new ProductImages();
                            productImage.Name = imageName;
                            productImage.ProductId = xdb.Id;

                            db.ProductImages.Add(productImage);
                            db.SaveChanges();
                        }

                    }
                }

            
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelCreate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Name, "Product");

                TempData["Create"] = "Create";


                return RedirectToAction("Index");
            }



            TempData["Create-Error"] = "Create-Error";

            ViewBag.Categories = db.ProductCategories.ToList();
            ViewBag.Admin = (int)Session["AdminId"];
            ViewBag.ProductPage = true;
            return View(x);
        }

        public ActionResult Update(int? id)
        {
            ViewBag.ProductPage = true;
            ViewBag.Categories = db.ProductCategories.ToList();
            Product xdb = db.Products.Include("ProductImages").Include("ProductToCategory").FirstOrDefault(p => p.Id == id);

            if (xdb == null)
            {

                return RedirectToAction("Oops", "Error", new { id = 404 });
            }

            return View(xdb);

        }

        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Update(Product x)
        {
            if (ModelState.IsValid)
            {
                Product xdb = db.Products.Include("ProductImages").Include("ProductToCategory").FirstOrDefault(p => p.Id == x.Id);

                xdb.About = x.About;
                xdb.Amount = x.Amount;
                xdb.Desc = x.Desc;
                xdb.Name = x.Name;
                xdb.Condition = x.Condition;
                xdb.Price = x.Price; 
                xdb.ModifiedDate = DateTime.Now;
                xdb.AdminModifiedId = (int)Session["AdminId"];

                db.Entry(xdb).State = EntityState.Modified;
                db.SaveChanges();

                using (DBContext db = new DBContext())
                {
                    foreach (var item in db.ProductToCategory.Where(c => c.ProductId == xdb.Id))
                    {
                        db.ProductToCategory.Remove(item);
                    }

                    db.SaveChanges();
                }

                if (x.CategoryId != null)
                {
                    foreach (var item in x.CategoryId)
                    {
                        ProductToCategory w = new ProductToCategory();
                        w.ProductId = xdb.Id;
                        w.ProductCategoryId = item;

                        db.ProductToCategory.Add(w);
                        db.SaveChanges();
                    }
                }


                if (x.ImageFile[0] != null)
                {
                    using (DBContext db = new DBContext())
                    {
                        foreach (var item in db.ProductImages.Where(c => c.ProductId == xdb.Id))
                        {
                            string oldImagePath = Path.Combine(Server.MapPath("~/Uploads"), item.Name);
                            System.IO.File.Delete(oldImagePath);

                            db.ProductImages.Remove(item);
                        }
                        db.SaveChanges();
                    }


                    foreach (HttpPostedFileBase image in x.ImageFile)
                    {


                        string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssffff") + (Regex.Replace(image.FileName, "[^A-Za-z0-9.]", "")); 
                        string imagePath = Path.Combine(Server.MapPath("~/Uploads"), imageName);

                        image.SaveAs(imagePath);

                        ProductImages productImage = new ProductImages();
                        productImage.Name = imageName;
                        productImage.ProductId = xdb.Id;

                        db.ProductImages.Add(productImage);
                        db.SaveChanges();


                    }

                    db.SaveChanges();

                }



                db.SaveChanges();

                int AdminId = (int)Session["AdminId"];
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelUpdate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Name, "Product");


                TempData["Update"] = "Update";
                return RedirectToAction("Index", "Product");

            }

            ViewBag.ProductPage = true;
            Product xdb2 = db.Products.Include("ProductImages").Include("ProductToCategory").FirstOrDefault(p => p.Id == x.Id);

            ViewBag.Categories = db.ProductCategories.ToList();
            ViewBag.ProductImages = db.ProductImages.Where(c => c.ProductId == xdb2.Id).ToList();
            TempData["Update-Error"] = "Update-Error";

            return View(x);

        }

        public JsonResult Delete(int? id)
        {
            Product xdb = db.Products.Include("ProductImages").Include("ProductToCategory").FirstOrDefault(p => p.Id == id);

            foreach (var item in db.ProductImages.Where(c => c.ProductId == xdb.Id))
            {
                string oldImagePath = Path.Combine(Server.MapPath("~/Uploads"), item.Name);
                System.IO.File.Delete(oldImagePath);

                db.ProductImages.Remove(item);
            }



            foreach (var item in db.ProductToCategory.Where(c => c.ProductId == xdb.Id))
            {
                db.ProductToCategory.Remove(item);
            }


            db.Products.Remove(xdb);

            db.SaveChanges();
            int AdminId = (int)Session["AdminId"];
            string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelDelete(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Name, "Product");

            return Json(JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateActive(int? id)
        {
            var result = false;

            if (id != null)
            {
                Product xdb = db.Products.FirstOrDefault(w => w.Id == id);


                if (xdb.isActive == false)
                {
                    xdb.isActive = true;
                }
                else
                {
                    xdb.isActive = false;

                }




                db.Entry(xdb).State = EntityState.Modified;
                db.SaveChanges();

                result = true;

            }



            return Json(result, JsonRequestBehavior.AllowGet);
        }
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public JsonResult ModalDetails(int id)
        {

            var xdb = db.Products.Where(c => c.Id == id).Select(s => new
            {

                Id = s.Id,
                Name = s.Name,
                Content = s.About,
                Condition = s.Condition,
                Price = s.Price,
                Amount = s.Amount,
                Desc = s.Desc,
                Admin = s.Admin.FullName,
                PostDate = s.PostDate,
                EditDate = s.ModifiedDate,
                TotalViews =s.ViewCount,
                EditAdmin = s.AdminModified.FullName,
                ProductImages = s.ProductImages.ToList(),
                ProductToCategory = s.ProductToCategory.ToList()

            }).FirstOrDefault();

            var images = new List<ProductImages>();

            foreach (ProductImages item in xdb.ProductImages)
            {

                images.Add(item);
            }

           

            return Json(new { xdb, images, content = HttpUtility.HtmlDecode(xdb.Content), desc = HttpUtility.HtmlDecode(xdb.Desc) }, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
    }
}