using g_auto.Areas.Admin.Filter;
using g_auto.DAL;
using g_auto.Hubs;
using g_auto.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.BuilderProperties;
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
    public class AboutController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/About
        public ActionResult Index()
        {
            ViewBag.AboutPage = true;
            List<About> xdb = db.About.Include("Admin").ToList();
            return View(xdb);
        }
        public ActionResult Create()
        {
            ViewBag.AboutPage = true;

            ViewBag.Admin = (int)Session["AdminId"];
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(About x)
        {
            int AdminId = (int)Session["AdminId"];

            ViewBag.AboutPage = true;

            if (ModelState.IsValid)
            {

                About xdb = new About();

                xdb.Address = x.Address;
                xdb.Content = x.Content;
                xdb.CoverImageFirstTitle = x.CoverImageFirstTitle;
                xdb.CoverImageSecondTitle = x.CoverImageSecondTitle;
                xdb.CoverImageThirdTitle = x.CoverImageThirdTitle;
                xdb.CoverImageFirstContent = x.CoverImageFirstContent;
                xdb.CoverImageSecondContent = x.CoverImageSecondContent;
                xdb.CoverImageThirdContent = x.CoverImageThirdContent;
                xdb.Email = x.Email;
                xdb.FacebookLink = x.FacebookLink;
                xdb.InstagramLink = x.InstagramLink;
                xdb.TwitterLink = x.TwitterLink;
                xdb.LinkedinLink = x.LinkedinLink;
                xdb.PinterestLink = x.PinterestLink;
                xdb.SkypeLink = x.SkypeLink;
                xdb.VimeoLink = x.VimeoLink;
                xdb.Phone = x.Phone;
                xdb.Title = x.Title;
                xdb.AdminId = x.AdminId;
                xdb.PostDate = DateTime.Now;
                if (db.AdminSettings.FirstOrDefault(c => c.AdminId == AdminId).alwaysActive)
                {
                    xdb.isActive = true;
                }
                else
                {
                    xdb.isActive = false;
                }

                if (x.CoverImageFirstFile == null)
                {
                    xdb.CoverImageFirst = "Default2";

                }
                else
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + Regex.Replace(x.CoverImageFirstFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    x.CoverImageFirstFile.SaveAs(imagePath);
                    xdb.CoverImageFirst = imageName;
                }

                if (x.CoverImageThirdFile == null)
                {
                    xdb.CoverImageThird = "Default2";

                }
                else
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + Regex.Replace(x.CoverImageThirdFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    x.CoverImageThirdFile.SaveAs(imagePath);
                    xdb.CoverImageThird = imageName;
                }
                if (x.CoverImageFourthFile == null)
                {
                    xdb.CoverImageFourth = "Default2";

                }
                else
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + Regex.Replace(x.CoverImageFourthFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    x.CoverImageFourthFile.SaveAs(imagePath);
                    xdb.CoverImageFourth = imageName;
                }

                if (x.CoverImageSecondFile == null)
                {
                    xdb.CoverImageSecond = "Default2";

                }
                else
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + Regex.Replace(x.CoverImageSecondFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    x.CoverImageSecondFile.SaveAs(imagePath);
                    xdb.CoverImageSecond = imageName;
                }

                db.About.Add(xdb);

                db.SaveChanges();
             
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelCreate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Title, "About");

                TempData["Create"] = "Create";

                return RedirectToAction("Index", "About");
            }
            ViewBag.Admin = (int)Session["AdminId"];
            TempData["Create-Error"] = "Create-Error";
            ViewBag.AboutPage = true;

            return View(x);
        }
        public ActionResult Update(int? id)
        {
            ViewBag.AboutPage = true;

            About xdb = db.About.Find(id);

            if (xdb == null)
            {
                return RedirectToAction("Oops", "Error", new { id = 404 });
            }

            return View(xdb);
        }

        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Update(About x)
        {

            if (ModelState.IsValid)
            {
                About xdb = db.About.Find(x.Id);

                xdb.Address = x.Address;
                xdb.Content = x.Content;
                xdb.CoverImageFirstTitle = x.CoverImageFirstTitle;
                xdb.CoverImageSecondTitle = x.CoverImageSecondTitle;
                xdb.CoverImageThirdTitle = x.CoverImageThirdTitle;
                xdb.CoverImageFirstContent = x.CoverImageFirstContent;
                xdb.CoverImageSecondContent = x.CoverImageSecondContent;
                xdb.CoverImageThirdContent = x.CoverImageThirdContent;
                xdb.Email = x.Email;
                xdb.FacebookLink = x.FacebookLink;
                xdb.InstagramLink = x.InstagramLink;
                xdb.TwitterLink = x.TwitterLink;
                xdb.LinkedinLink = x.LinkedinLink;
                xdb.PinterestLink = x.PinterestLink;
                xdb.SkypeLink = x.SkypeLink;
                xdb.VimeoLink = x.VimeoLink;
                xdb.Phone = x.Phone;
                xdb.Title = x.Title;
                xdb.ModifiedDate = DateTime.Now;
                xdb.AdminModifiedId = (int)Session["AdminId"];

                if (x.CoverImageFirstFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + Regex.Replace(x.CoverImageFirstFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.CoverImageFirst);
                    System.IO.File.Delete(oldImagePath);

                    x.CoverImageFirstFile.SaveAs(imagePath);
                    xdb.CoverImageFirst = imageName;

                }

                if (x.CoverImageSecondFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + Regex.Replace(x.CoverImageSecondFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.CoverImageSecond);
                    System.IO.File.Delete(oldImagePath);

                    x.CoverImageSecondFile.SaveAs(imagePath);
                    xdb.CoverImageSecond = imageName;

                }
                if (x.CoverImageThirdFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + Regex.Replace(x.CoverImageThirdFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.CoverImageThird);
                    System.IO.File.Delete(oldImagePath);

                    x.CoverImageThirdFile.SaveAs(imagePath);
                    xdb.CoverImageThird = imageName;

                }
                if (x.CoverImageFourthFile != null)
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + Regex.Replace(x.CoverImageFourthFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.CoverImageFourth);
                    System.IO.File.Delete(oldImagePath);

                    x.CoverImageFourthFile.SaveAs(imagePath);
                    xdb.CoverImageFourth = imageName;

                }



                db.Entry(xdb).State = EntityState.Modified;
                db.SaveChanges();
                int AdminId = (int)Session["AdminId"];
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;

                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelUpdate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Title, "About");
                TempData["Update"] = "Update";
                return RedirectToAction("Index", "About");
            }
            ViewBag.AboutPage = true;

            TempData["Update-Error"] = "Update-Error";
            return View(x);
        }

        public JsonResult Delete(int? id)
        {


            About xdb = db.About.Find(id);

           
            string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.CoverImageFirst);
            System.IO.File.Delete(oldImagePath);


            string oldImagePath2 = Path.Combine(Server.MapPath("~/Uploads/"), xdb.CoverImageSecond);
            System.IO.File.Delete(oldImagePath2);

            string oldImagePath3 = Path.Combine(Server.MapPath("~/Uploads/"), xdb.CoverImageThird);
            System.IO.File.Delete(oldImagePath3);

            string oldImagePath4 = Path.Combine(Server.MapPath("~/Uploads/"), xdb.CoverImageFourth);
            System.IO.File.Delete(oldImagePath4);

            db.About.Remove(xdb);

            db.SaveChanges();
            int AdminId = (int)Session["AdminId"];
            string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelDelete(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Title, "About");

            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateActive(int? id)
        {
            var result = false;

            if (id != null)
            {
                About xdb = db.About.FirstOrDefault(w => w.Id == id);


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

            var xdb = db.About.Where(c => c.Id == id).Select(s => new
            {

                Id = s.Id,
                Address = s.Address,
                Phone = s.Phone,
                Name = s.Title,
                Content = s.Content,
                Email = s.Email,
                Admin = s.Admin.FullName,
                PostDate = s.PostDate,
                EditDate = s.ModifiedDate,
                EditAdmin = s.AdminModified.FullName,
                ImageFirst = s.CoverImageFirst,
                ImageSecond = s.CoverImageSecond,
                Fb = s.FacebookLink,
                Tw = s.TwitterLink,
                Ig = s.InstagramLink,
                Lk = s.LinkedinLink,
                Pt = s.PinterestLink,
                Sk = s.SkypeLink,
                Vm = s.VimeoLink,

            }).FirstOrDefault();

            return Json(new { xdb, content = HttpUtility.HtmlDecode(xdb.Content) }, "application/json",Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
    }
}