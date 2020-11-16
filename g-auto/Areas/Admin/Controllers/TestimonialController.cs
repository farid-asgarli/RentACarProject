using g_auto.Areas.Admin.Filter;
using g_auto.DAL;
using g_auto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace g_auto.Areas.Admin.Controllers
{
    [logout]
    public class TestimonialController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/Testimonial
        public ActionResult Index()
        {
            ViewBag.TestimonialPage = true;

            List<Testimonial> xdb = db.Testimonials.Include("User").ToList();

            return View(xdb);
        }

        public JsonResult UpdateActive(int? id)
        {
            var result = false;

            if (id != null)
            {
                Testimonial xdb = db.Testimonials.FirstOrDefault(w => w.Id == id);


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
        public JsonResult Delete(int id)
        {
            Testimonial xdb = db.Testimonials.Find(id);


            db.Testimonials.Remove(xdb);
            db.SaveChanges();


            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModalDetails(int id)
        {

            var xdb = db.Testimonials.Where(c => c.Id == id).Select(s => new
            {

                Id = s.Id,
                Name = s.User.FullName,
                Content = s.Content,
                PostDate = s.PostedDate,
                Image = s.User.ProfilePicture

            }).FirstOrDefault();





            return Json(xdb, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
    }
}