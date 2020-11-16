using g_auto.Areas.Admin.Filter;
using g_auto.DAL;
using g_auto.Hubs;
using g_auto.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace g_auto.Areas.Admin.Controllers
{
    [logout]
    public class ProductCategoriesController : Controller
    {
        // GET: Admin/ProductCategories
        DBContext db = new DBContext();
        public ActionResult Index(string sortOrder,string searchString)
        {

            //List<ProductCategory> xdb = db.ProductCategories.Include("ProductToCategory").Include("ProductToCategory.Product").Include("Admin").ToList();
            //return View(xdb);

            ViewBag.ProductCategoriesPage = true;

            ViewBag.NameSortParam = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.ProductSortParam = sortOrder == "Product" ? "product_desc" : "Product";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "";

            var xdb = from x in db.ProductCategories.Include("ProductToCategory").Include("ProductToCategory.Product").Include("Admin")
                      select x;
            if (!String.IsNullOrEmpty(searchString))
            {
                xdb = xdb.Where(s => s.Name.ToString().Contains(searchString)
                                       || s.ProductToCategory.FirstOrDefault().Product.Name.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Name":
                    xdb = xdb.OrderBy(s => s.Name);
                    break;

                case "name_desc":
                    xdb = xdb.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    xdb = xdb.OrderBy(s => s.PostDate);
                    break;
                case "date_desc":
                    xdb = xdb.OrderByDescending(s => s.PostDate);
                    break;
                case "product_desc":
                    xdb = xdb.OrderByDescending(s => s.ProductToCategory.FirstOrDefault()!=null? s.ProductToCategory.FirstOrDefault().Product.Name:"");
                    break;
                case "Product":
                    xdb = xdb.OrderBy(s => s.ProductToCategory.FirstOrDefault() != null ? s.ProductToCategory.FirstOrDefault().Product.Name : "");
                    break;
                default:
                    xdb = xdb.OrderByDescending(s => s.PostDate);
                    break;

            }
            return View(xdb.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.ProductCategoriesPage = true;

            ViewBag.Admin = (int)Session["AdminId"];
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductCategory x)
        {

            if (ModelState.IsValid)
            {
                ProductCategory xdb = new ProductCategory();

                xdb.AdminId = x.AdminId;
                xdb.PostDate = DateTime.Now;
                xdb.Name = x.Name;

                db.ProductCategories.Add(xdb);
                db.SaveChanges();
                int AdminId = (int)Session["AdminId"];
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelCreate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Name, "Product Category");

                TempData["Create"] = "Create";


                return RedirectToAction("Index", "ProductCategories");

            }

            TempData["Create-Error"] = "Create-Error";
            ViewBag.ProductCategoriesPage = true;

            ViewBag.Admin = (int)Session["AdminId"];
            return View(x);
        }

        public ActionResult Update(int Id)
        {
            ViewBag.ProductCategoriesPage = true;

            ProductCategory x = db.ProductCategories.Find(Id);

            if (x == null)
            {
                return RedirectToAction("Oops", "Error", new { id = 404 });
            }

            return View(x);

        }


        [HttpPost]
        public ActionResult Update(ProductCategory x)
        {
            if (ModelState.IsValid)
            {

                ProductCategory xdb = db.ProductCategories.Find(x.Id);


                xdb.Name = x.Name;
                xdb.ModifiedDate = DateTime.Now;
                xdb.AdminModifiedId = (int)Session["AdminId"];

                db.Entry(xdb).State = EntityState.Modified;


                db.SaveChanges();
                int AdminId = (int)Session["AdminId"];
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelUpdate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Name, "Product Category");

                TempData["Update"] = "Update";
                return RedirectToAction("Index");


            }
            TempData["Update-Error"] = "Update-Error";
            ViewBag.ProductCategoriesPage = true;

            return View(x);
        }

        public JsonResult Delete(int id)
        {
            ProductCategory xdb = db.ProductCategories.Find(id);
          

            db.ProductCategories.Remove(xdb);
            db.SaveChanges();
            int AdminId = (int)Session["AdminId"];
            string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelDelete(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Name, "Product Category");

            return Json(JsonRequestBehavior.AllowGet);
        }
        public JsonResult ModalDetails(int id)
        {

            var products = new List<Product>();

            foreach (ProductToCategory item in db.ProductToCategory.Include("ProductCategory").Where(c => c.ProductCategoryId == id))
            {

                products.Add(item.Product);
            }

            var xdb = db.ProductCategories.Where(c => c.Id == id).Select(s => new
            {

                Id = s.Id,
                Name = s.Name,
                Admin = s.Admin.FullName,
                PostDate = s.PostDate,
                EditDate = s.ModifiedDate,
                EditAdmin = s.AdminModified.FullName,

            }).FirstOrDefault();

            return Json(new { xdb, products }, JsonRequestBehavior.AllowGet);
        }
    }
}