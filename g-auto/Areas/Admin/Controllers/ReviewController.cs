using g_auto.Areas.Admin.Filter;
using g_auto.DAL;
using g_auto.Models;
using g_auto.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace g_auto.Areas.Admin.Controllers
{
    [logout]
    public class ReviewController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/Review
        public ActionResult Index()
        {
            ViewBag.ReviewPage = true;

            ReviewVM v = new ReviewVM();

           v.Reviews = db.Reviews.Include("User").Include("Model").Include("Model.Brand").Include("Reservation").ToList();
           v.ReviewProducts = db.ReviewProduct.Include("User").Include("Product").Include("Product.ProductToCategory").Include("Product.ProductToCategory.ProductCategory").Include("Sale").ToList();

            return View(v);
        }

        public ActionResult ReviewDetailsModel(int id)
        {
            ViewBag.ReviewPage = true;

            int AdminId =  (int)Session["AdminId"];

            
            ViewBag.DisplayName = db.AdminSettings.FirstOrDefault(c => c.AdminId == AdminId).DisplayName;

           


            Review xdb = db.Reviews.Include("User").Include("Model").Include("Model.Brand").Include("Model.ModelImages").Include("Replies").Include("Replies.Admin").Include("Replies.Admin.AdminSettings").Include("Reservation").FirstOrDefault(c=>c.Id==id);
            return View(xdb);
        }


        public ActionResult ReviewDetailProduct(int id)
        {
            ViewBag.ReviewPage = true;

            int AdminId = (int)Session["AdminId"];


            ViewBag.DisplayName = db.AdminSettings.FirstOrDefault(c => c.AdminId == AdminId).DisplayName;




            ReviewProduct xdb = db.ReviewProduct.Include("User").Include("Product").Include("Product.ProductToCategory").Include("Product.ProductToCategory.ProductCategory").Include("Product.ProductImages").Include("Replies").Include("Replies.Admin").Include("Replies.Admin.AdminSettings").Include("Sale").FirstOrDefault(c => c.Id == id);
            return View(xdb);
        }
        public JsonResult ReplyToCustomerModel(ModelReviewReply x)
        {
            int AdminId = (int)Session["AdminId"];

            string name = db.Admin.FirstOrDefault(c => c.Id == AdminId).FullName;

            ModelReviewReply xdb = new ModelReviewReply();


            xdb.Content = x.Content;
            xdb.PostedDate = DateTime.Now;
            xdb.AdminId = AdminId;
            xdb.ReviewId = x.ReviewId;

            db.ModelReviewReplies.Add(xdb);

            db.SaveChanges();


            return Json(new {xdb, name}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReplyToCustomerProduct(ProductReviewReply x)
        {
            int AdminId = (int)Session["AdminId"];

            string name = db.Admin.FirstOrDefault(c => c.Id == AdminId).FullName;

            ProductReviewReply xdb = new ProductReviewReply();


            xdb.Content = x.Content;
            xdb.PostedDate = DateTime.Now;
            xdb.AdminId = AdminId;
            xdb.ReviewProductId = x.ReviewProductId;

            db.ProductReviewReplies.Add(xdb);

            db.SaveChanges();


            return Json(new { xdb, name }, JsonRequestBehavior.AllowGet);
        }
    }
}