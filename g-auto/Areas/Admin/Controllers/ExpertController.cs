using g_auto.Areas.Admin.Filter;
using g_auto.DAL;
using g_auto.Hubs;
using g_auto.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace g_auto.Areas.Admin.Controllers
{
    [logout]
    public class ExpertController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/Expert
        public ActionResult Index(string sortOrder,string searchString)
        {
            ViewBag.ExpertPage = true;

            //List<Expert> x = db.Expert.Include("Admin").ToList();
            //return View(x);
            ViewBag.NameSortParam = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.IsSortParam = sortOrder == "Is" ? "is_desc" : "Is";
            ViewBag.ExSortParam = sortOrder == "Ex" ? "ex_desc" : "Ex";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var xdb = from x in db.Expert.Include("Admin")
                      select x;
            if (!String.IsNullOrEmpty(searchString))
            {
                xdb = xdb.Where(s => s.FullName.ToString().Contains(searchString)
                                       || s.ExperienceYear.ToString().Contains(searchString)
                                       || s.FacebookProfile.ToString().Contains(searchString) || s.InstagramProfile.ToString().Contains(searchString) || s.TwitterProfile.ToString().Contains(searchString) || s.LinkedInProfile.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    xdb = xdb.OrderByDescending(s => s.FullName);
                    break;
                case "Name":
                    xdb = xdb.OrderBy(s => s.FullName);
                    break;
                case "Date":
                    xdb = xdb.OrderBy(s => s.PostDate);
                    break;
                case "date_desc":
                    xdb = xdb.OrderByDescending(s => s.PostDate);
                    break;
                case "ex_desc":
                    xdb = xdb.OrderByDescending(s => s.ExperienceYear);
                    break;
                case "Ex":
                    xdb = xdb.OrderBy(s => s.ExperienceYear);
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
            ViewBag.ExpertPage = true;

            ViewBag.Admin = (int)Session["AdminId"];
            return View();
        }

        [HttpPost]
        public ActionResult Create(Expert x)
        {
            int AdminId = (int)Session["AdminId"];
            if (ModelState.IsValid)
            {

                Expert xdb = new Expert();

                if (x.ProfilePictureFile == null)
                {
                    xdb.ProfilePicture = "Default2";
                }
                else
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + Regex.Replace(x.ProfilePictureFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    x.ProfilePictureFile.SaveAs(imagePath);
                    xdb.ProfilePicture = imageName;

                }

                xdb.ExperienceYear = x.ExperienceYear;
                xdb.FullName = x.FullName;
                xdb.FacebookProfile = x.FacebookProfile;
                xdb.InstagramProfile = x.InstagramProfile;
                xdb.LinkedInProfile = x.LinkedInProfile;
                xdb.TwitterProfile = x.TwitterProfile;
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


                db.Expert.Add(xdb);
                TempData["Create"] = "Create";
                db.SaveChanges();
         
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelCreate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.FullName, "Expert");

                return RedirectToAction("Index", "Expert");
            }
            ViewBag.ExpertPage = true;

            TempData["Create-Error"] = "Create-Error";
            ViewBag.Admin = (int)Session["AdminId"];
            return View(x);
        }

        public ActionResult Update(int id)
        {
            ViewBag.ExpertPage = true;

            Expert xdb = db.Expert.Find(id);

            if (xdb == null)
            {
                return RedirectToAction("Oops", "Error", new { id = 404 });
            }
            return View(xdb);
        }

        [HttpPost]
        public ActionResult Update(Expert x)
        {
            if (ModelState.IsValid)
            {
                Expert xdb = db.Expert.Find(x.Id);


                if (x.ProfilePictureFile != null)
                {
                    string imageName = DateTime.Now.ToString("ssfff") + Regex.Replace(x.ProfilePictureFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.ProfilePicture);
                    System.IO.File.Delete(oldImagePath);

                    x.ProfilePictureFile.SaveAs(imagePath);
                    xdb.ProfilePicture = imageName;
                }

                xdb.ExperienceYear = x.ExperienceYear;
                xdb.FullName = x.FullName;
                xdb.FacebookProfile = x.FacebookProfile;
                xdb.InstagramProfile = x.InstagramProfile;
                xdb.LinkedInProfile = x.LinkedInProfile;
                xdb.TwitterProfile = x.TwitterProfile;
                xdb.ModifiedDate = DateTime.Now;
                xdb.AdminModifiedId = (int)Session["AdminId"];

                db.Entry(xdb).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Update"] = "Update";
                int AdminId = (int)Session["AdminId"];
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelUpdate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.FullName, "Expert");


                return RedirectToAction("Index", "Expert");
            }

            TempData["Update-Error"] = "Update-Error";
            ViewBag.ExpertPage = true;

            return View(x);
        }

        public JsonResult Delete(int id)
        {
            Expert xdb = db.Expert.Find(id);
          
            string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.ProfilePicture);
            System.IO.File.Delete(oldImagePath);

            db.Expert.Remove(xdb);
            db.SaveChanges();
            int AdminId = (int)Session["AdminId"];
            string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelDelete(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.FullName, "Expert");

            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateActive(int? id)
        {
            var result = false;

            if (id != null)
            {
                Expert xdb = db.Expert.FirstOrDefault(w => w.Id == id);


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

        public JsonResult ModalDetails(int id)
        {

            var xdb = db.Expert.Where(c => c.Id == id).Select(s => new
            {

                Id = s.Id,
                Year =s.ExperienceYear,
                Name = s.FullName,
                Admin = s.Admin.FullName,
                PostDate = s.PostDate,
                EditDate = s.ModifiedDate,
                EditAdmin = s.AdminModified.FullName,
                Image = s.ProfilePicture,
                Fb =s.FacebookProfile,
                Tw=s.TwitterProfile,
                Ig=s.InstagramProfile,
                Lk=s.LinkedInProfile

            }).FirstOrDefault();

            return Json(xdb, JsonRequestBehavior.AllowGet);
        }
    }
}