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
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace g_auto.Areas.Admin.Controllers
{
    [logout]
    public class VacancyController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/Vacancy
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.VacancyPage = true;



            ViewBag.NameSortParam = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.IsSortParam = sortOrder == "Is" ? "is_desc" : "Is";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "";
            ViewBag.DeadlineSortParam = sortOrder == "Deadline" ? "deadline_desc" : "Deadline";
            var xdb = from x in db.Vacancies.Include("Admin")
                      select x;
            if (!String.IsNullOrEmpty(searchString))
            {
                xdb = xdb.Where(s => s.Title.ToString().Contains(searchString)
                                       || (s.Content).ToString().Contains(searchString) || (s.Description).ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    xdb = xdb.OrderByDescending(s => s.Title);
                    break;
                case "Name":
                    xdb = xdb.OrderBy(s => s.Title);
                    break;
                case "deadline_desc":
                    xdb = xdb.OrderByDescending(s => s.Deadline);
                    break;
                case "Deadline":
                    xdb = xdb.OrderBy(s => s.Deadline);
                    break;
                case "Date":
                    xdb = xdb.OrderBy(s => s.PostDate);
                    break;
                case "date_desc":
                    xdb = xdb.OrderByDescending(s => s.PostDate);
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
            ViewBag.VacancyPage = true;

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Vacancy x)
        {
            int AdminId = (int)Session["AdminId"];

            if (ModelState.IsValid)
            {

                Vacancy xdb = new Vacancy();

                xdb.AdminId = x.AdminId;
                xdb.Content = x.Content;
                xdb.PostDate = DateTime.Now;
                xdb.Title = x.Title;
                xdb.Deadline = x.Deadline;
                xdb.Description = x.Description;
                xdb.ViewCount = 0;
                xdb.Salary = x.Salary;

                if (db.AdminSettings.FirstOrDefault(c => c.AdminId == AdminId).alwaysActive)
                {
                    xdb.isActive = true;
                }
                else
                {
                    xdb.isActive = false;
                }

                if (x.CoverImageFile == null)
                {
                    xdb.CoverImage = "Default2";
                }
                else
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + Regex.Replace(x.CoverImageFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    x.CoverImageFile.SaveAs(imagePath);
                    xdb.CoverImage = imageName;

                }

                db.Vacancies.Add(xdb);



                db.SaveChanges();
                TempData["Create"] = "Create";

           
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelCreate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Title, "Vacancy");

                return RedirectToAction("Index", "Vacancy");
            }
            TempData["Create-Error"] = "Create-Error";
            ViewBag.Admin = (int)Session["AdminId"];
            ViewBag.VacancyPage = true;

            return View(x);
        }

        public ActionResult Update(int? id)
        {
            Vacancy x = db.Vacancies.Find(id);
            ViewBag.VacancyPage = true;

            if (x == null)
            {
                return RedirectToAction("Oops", "Error", new { id = 404 });
            }

            return View(x);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Vacancy x)
        {
            if (ModelState.IsValid)
            {

                Vacancy xdb = db.Vacancies.Include("Admin").FirstOrDefault(w => w.Id == x.Id);

                xdb.Content = x.Content;
                xdb.Title = x.Title;
                xdb.Description = x.Description;
                xdb.Salary = x.Salary;
                if (x.Deadline != null)
                {
                    xdb.Deadline = x.Deadline;
                }
                xdb.ModifiedDate = DateTime.Now;
                xdb.AdminModifiedId = (int)Session["AdminId"];

                if (x.CoverImageFile != null)
                {
                    string imageName = DateTime.Now.ToString("ssfff") + Regex.Replace(x.CoverImageFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.CoverImage);
                    System.IO.File.Delete(oldImagePath);

                    x.CoverImageFile.SaveAs(imagePath);
                    xdb.CoverImage = imageName;
                }

                db.Entry(xdb).State = EntityState.Modified;
                db.SaveChanges();

                int AdminId = (int)Session["AdminId"];
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelUpdate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Title, "Vacancy");


                TempData["Update"] = "Update";




                return RedirectToAction("Index", "Vacancy");
            }
            TempData["Update-Error"] = "Update-Error";
            ViewBag.VacancyPage = true;


            return View(x);
        }

        public JsonResult Delete(int id)
        {
            Vacancy xdb = db.Vacancies.Find(id);


            string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.CoverImage);
            System.IO.File.Delete(oldImagePath);

            db.Vacancies.Remove(xdb);
            db.SaveChanges();
            int AdminId = (int)Session["AdminId"];
            string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelDelete(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Title, "Vacancy");

            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateActive(int? id)
        {
            var result = false;

            if (id != null)
            {
                Vacancy xdb = db.Vacancies.FirstOrDefault(w => w.Id == id);


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

            var xdb = db.Vacancies.Where(c => c.Id == id).Select(s => new
            {

                Id = s.Id,
                Name = s.Title,
                Content = s.Content,
                Description = s.Description,
                Deadline = s.Deadline,
                Admin = s.Admin.FullName,
                PostDate = s.PostDate,
                EditDate = s.ModifiedDate,
                EditAdmin = s.AdminModified.FullName,
                Image = s.CoverImage

            }).FirstOrDefault();





            return Json(new { xdb, content = HttpUtility.HtmlDecode(xdb.Content) }, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
    }
}