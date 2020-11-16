using g_auto.Areas.Admin.Filter;
using g_auto.DAL;
using g_auto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace g_auto.Areas.Admin.Controllers
{
    [logout]
    public class LayoutController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/Layout
        public ActionResult Index()
        {
            ViewBag.LayoutPage = true;
            ViewBag.AboutBenefits = db.AboutBenefits.ToList();
            Layout xdb = db.Layout.FirstOrDefault();
            return View(xdb);
        }
        
        
        
        
        [ValidateInput(false)]

        public JsonResult UpdateLayout(Layout x)
        {
            var result = false;

            if (x != null)
            {
                Layout xdb = db.Layout.FirstOrDefault(w => w.Id == x.Id);



                xdb.expertToggle = x.expertToggle;
                xdb.blogToggle = x.blogToggle;
                xdb.modelToggle = x.modelToggle;
                xdb.serviceToggle = x.serviceToggle;
                xdb.testimonialToggle = x.testimonialToggle;
                xdb.sliderToggle = x.sliderToggle;
                xdb.contactToggle = x.contactToggle;
                xdb.aboutToggle = x.aboutToggle;
                xdb.promoToggle = x.promoToggle;


                xdb.expertCount = x.expertCount;
                xdb.blogCount = x.blogCount;
                xdb.modelCount = x.modelCount;
                xdb.serviceCount = x.serviceCount;
                xdb.testiominalCount = x.testiominalCount;
                xdb.sliderCount = x.sliderCount;

              

                db.Entry(xdb).State = EntityState.Modified;
                db.Entry(xdb).Property(c=> c.Phone).IsModified = false;
                db.Entry(xdb).Property(c=> c.Email).IsModified = false;
                db.Entry(xdb).Property(c=> c.Address).IsModified = false;

                db.SaveChanges();

                result = true;

            }




            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]

        public JsonResult UpdateLogo(Layout x)
        {
            var result = false;

            if (x != null)
            {
                Layout xdb = db.Layout.FirstOrDefault(w => w.Id == x.Id);



                if (x.LogoFile != null)
                {
                    string imageName = DateTime.Now.ToString("ssfff") + Regex.Replace(x.LogoFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.Logo);
                    System.IO.File.Delete(oldImagePath);

                    x.LogoFile.SaveAs(imagePath);
                    xdb.Logo = imageName;
                }
                if (x.LogoFooterFile != null)
                {
                    string imageName = DateTime.Now.ToString("ssfff") + Regex.Replace(x.LogoFooterFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.LogoFooter);
                    System.IO.File.Delete(oldImagePath);

                    x.LogoFooterFile.SaveAs(imagePath);
                    xdb.LogoFooter = imageName;
                }

                if (x.About_ImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ssfff") + Regex.Replace(x.About_ImageFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.About_Image);
                    System.IO.File.Delete(oldImagePath);

                    x.About_ImageFile.SaveAs(imagePath);
                    xdb.About_Image = imageName;
                }

                if (x.SignatureFile != null)
                {
                    string imageName = DateTime.Now.ToString("ssfff") + Regex.Replace(x.SignatureFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.Signature);
                    System.IO.File.Delete(oldImagePath);

                    x.SignatureFile.SaveAs(imagePath);
                    xdb.Signature = imageName;
                }

                if (x.Promo_ImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ssfff") + Regex.Replace(x.Promo_ImageFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.Promo_Image);
                    System.IO.File.Delete(oldImagePath);

                    x.Promo_ImageFile.SaveAs(imagePath);
                    xdb.Promo_Image = imageName;
                }




                db.Entry(xdb).State = EntityState.Modified;
                db.Entry(xdb).Property(c => c.Phone).IsModified = false;
                db.Entry(xdb).Property(c => c.Email).IsModified = false;
                db.Entry(xdb).Property(c => c.Address).IsModified = false;
                db.SaveChanges();

                result = true;

            }




            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [ValidateInput(false)]
        public JsonResult UpdateAboutSection(Layout x)
        {
            var result = false;

            if (x != null)
            {
                Layout xdb = db.Layout.FirstOrDefault(w => w.Id == x.Id);

                xdb.About_Content = x.About_Content;
                xdb.About_Title = x.About_Title;
                xdb.About_CEO = x.About_CEO;


                using (DBContext db = new DBContext())
                {
                    foreach (var item in db.AboutBenefits.Where(c => c.LayoutId == xdb.Id))
                    {
                        db.AboutBenefits.Remove(item);
                    }

                    db.SaveChanges();
                }

                if (x.Benefits != null)
                {
                    foreach (var item in x.Benefits)
                    {
                        AboutBenefits w = new AboutBenefits();

                        w.Content = item;
                        w.LayoutId = xdb.Id;

                        db.AboutBenefits.Add(w);
                        db.SaveChanges();


                    }
                }

                db.Entry(xdb).State = EntityState.Modified;
                db.Entry(xdb).Property(c => c.Phone).IsModified = false;
                db.Entry(xdb).Property(c => c.Email).IsModified = false;
                db.Entry(xdb).Property(c => c.Address).IsModified = false;
                db.SaveChanges();

                result = true;

            }




            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [ValidateInput(false)]
        public JsonResult UpdateContactSection(Layout x)
        {
            var result = false;

            if (x != null)
            {
                Layout xdb = db.Layout.FirstOrDefault(w => w.Id == x.Id);

                xdb.Contact_Content = x.Contact_Content;
                xdb.Contact_Title = x.Contact_Title;


                db.Entry(xdb).State = EntityState.Modified;
                db.Entry(xdb).Property(c => c.Phone).IsModified = false;
                db.Entry(xdb).Property(c => c.Email).IsModified = false;
                db.Entry(xdb).Property(c => c.Address).IsModified = false;
                db.SaveChanges();

                result = true;

            }




            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public JsonResult UpdatePromoSection(Layout x)
        {
            var result = false;

            if (x != null)
            {
                Layout xdb = db.Layout.FirstOrDefault(w => w.Id == x.Id);

                xdb.Promo_Content = x.Promo_Content;
                xdb.Promo_Link = x.Promo_Link;


                db.Entry(xdb).State = EntityState.Modified;
                db.Entry(xdb).Property(c => c.Phone).IsModified = false;
                db.Entry(xdb).Property(c => c.Email).IsModified = false;
                db.Entry(xdb).Property(c => c.Address).IsModified = false;
                db.SaveChanges();

                result = true;

            }




            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateCompanySection(Layout x)
        {
            var result = false;

            if (x != null)
            {
                Layout xdb = db.Layout.FirstOrDefault(w => w.Id == x.Id);

                xdb.Phone = x.Phone;
                xdb.Address = x.Address;
                xdb.Email = x.Email;


                db.Entry(xdb).State = EntityState.Modified;
                db.SaveChanges();

                result = true;

            }




            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}