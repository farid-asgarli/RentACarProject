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
    public class ServiceController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/Service
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.ServicePage = true;
            //List<Service> xdb = db.Service.Include("ServiceToInfo").Include("ServiceToInfo.ServiceInfo").Include(w => w.ServiceBenefits).Include("Admin").ToList();
            //return View(xdb);

            ViewBag.NameSortParam = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.BenefitSortParam = sortOrder == "Benefit" ? "benefit_desc" : "Benefit";
            ViewBag.InfoSortParam = sortOrder == "Info" ? "info_desc" : "Info";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "";
            ViewBag.IsSortParam = sortOrder == "Is" ? "is_desc" : "Is";

            var xdb = from x in db.Service.Include("ServiceToInfo").Include("ServiceToInfo.ServiceInfo").Include("ServiceBenefits").Include("Admin").ToList()
                      select x;
            if (!String.IsNullOrEmpty(searchString))
            {
                xdb = xdb.Where(s => s.Content.ToString().Contains(searchString)
                                       //|| s.ServiceBenefits.FirstOrDefault().Content.ToString().Contains(searchString)
                                       || s.Title.ToString().Contains(searchString)
                                      /* || (s.ServiceToInfo.FirstOrDefault()!=null && s.ServiceToInfo.FirstOrDefault().ServiceInfo.Title.ToString().Contains(searchString))*/);
            }
            switch (sortOrder)
            {
                case "Name":
                    xdb = xdb.OrderBy(s => s.Title.ToString());
                    break;

                case "name_desc":
                    xdb = xdb.OrderByDescending(s => s.Title.ToString());
                    break;
                case "Date":
                    xdb = xdb.OrderBy(s => s.PostDate);
                    break;
                case "date_desc":
                    xdb = xdb.OrderByDescending(s => s.PostDate);
                    break;
                case "info_desc":
                    xdb = xdb.OrderByDescending(s => s.ServiceToInfo.FirstOrDefault()!=null? s.ServiceToInfo.FirstOrDefault().ServiceInfo.Title.ToString():"");
                    break;
                case "Info":
                    xdb = xdb.OrderBy(s => s.ServiceToInfo.FirstOrDefault() != null ? s.ServiceToInfo.FirstOrDefault().ServiceInfo.Title.ToString() : "");
                    break;
                case "benefit_desc":
                    xdb = xdb.OrderByDescending(s => s.ServiceBenefits.FirstOrDefault().Content.ToString());
                    break;
                case "Benefit":
                    xdb = xdb.OrderBy(s => s.ServiceBenefits.FirstOrDefault().Content.ToString());
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
            ViewBag.ServicePage = true;
            ViewBag.Admin = (int)Session["AdminId"];
            ViewBag.Infos = db.ServiceInfo.ToList();

            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Service x)
        {
            int AdminId = (int)Session["AdminId"];
            if (ModelState.IsValid)
            {
                Service xdb = new Service();

                xdb.Content = x.Content;
                xdb.Title = x.Title;
                xdb.AdminId = x.AdminId;
                xdb.PostDate = DateTime.Now;
                xdb.Description = x.Description;

                if (db.AdminSettings.FirstOrDefault(c => c.AdminId == AdminId).alwaysActive)
                {
                    xdb.isActive = true;
                }
                else
                {
                    xdb.isActive = false;
                }


                if (x.ServiceImageFirstFile == null)
                {
                    xdb.ServiceImageFirst = "Default2";

                }
                else
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + Regex.Replace(x.ServiceImageFirstFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    x.ServiceImageFirstFile.SaveAs(imagePath);
                    xdb.ServiceImageFirst = imageName;
                }

                if (x.ServiceImageSecondFile == null)
                {
                    xdb.ServiceImageSecond = "Default2";

                }
                else
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + Regex.Replace(x.ServiceImageSecondFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    x.ServiceImageSecondFile.SaveAs(imagePath);
                    xdb.ServiceImageSecond = imageName;
                }

                if (x.ServiceIconFile == null)
                {
                    xdb.ServiceIcon = "Default2";

                }
                else
                {
                    string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + Regex.Replace(x.ServiceIconFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    x.ServiceIconFile.SaveAs(imagePath);
                    xdb.ServiceIcon = imageName;
                }


              
                db.Service.Add(xdb);

                db.SaveChanges();




                if (x.Benefits != null)
                {

                    foreach (var item in x.Benefits)
                    {
                        ServiceBenefit w = new ServiceBenefit();

                        w.Content = item;
                        w.ServiceId = xdb.Id;

                        db.ServiceBenefits.Add(w);

                    }
                }

                db.SaveChanges();

                if (db.ServiceInfo.FirstOrDefault() != null && x.ServiceInfoId != null)
                {
                    foreach (var item in x.ServiceInfoId)
                    {
                        ServiceToInfo w = new ServiceToInfo();
                        w.ServiceId = xdb.Id;
                        w.ServiceInfoId = item;

                        db.ServiceToInfo.Add(w);
                    }
                }

                db.SaveChanges();


                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelCreate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Title, "Service");
                TempData["Create"] = "Create";


                return RedirectToAction("Index", "Service");



            }
            ViewBag.ServicePage = true;
            ViewBag.Admin = (int)Session["AdminId"];
            TempData["Create-Error"] = "Create-Error";
            ViewBag.Infos = db.ServiceInfo.ToList();

            return View(x);
        }

        public ActionResult Update(int? id)
        {
            ViewBag.Infos = db.ServiceInfo.ToList();
            ViewBag.Benefits = db.ServiceBenefits.ToList();

            ViewBag.ServicePage = true;
            Service xdb = db.Service.Include("ServiceToInfo").Include(w => w.ServiceBenefits).FirstOrDefault(w => w.Id == id);

            if (xdb == null)
            {

                return RedirectToAction("Oops", "Error", new { id = 404 });
            }

            return View(xdb);


        }

        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Update(Service x)
        {
            if (ModelState.IsValid)
            {
                Service xdb = db.Service.Include("ServiceToInfo").Include(w => w.ServiceBenefits).FirstOrDefault(w => w.Id == x.Id);

                xdb.Content = x.Content;
                xdb.Title = x.Title;
                xdb.Description = x.Description; 
                xdb.ModifiedDate = DateTime.Now;
                xdb.AdminModifiedId = (int)Session["AdminId"];

                if (x.ServiceImageFirstFile != null)
                {
                    string imageName = DateTime.Now.ToString("ssfff") + Regex.Replace(x.ServiceImageFirstFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.ServiceImageFirst);
                    System.IO.File.Delete(oldImagePath);

                    x.ServiceImageFirstFile.SaveAs(imagePath);
                    xdb.ServiceImageFirst = imageName;
                }

                if (x.ServiceImageSecondFile != null)
                {
                    string imageName = DateTime.Now.ToString("ssfff") + Regex.Replace(x.ServiceImageSecondFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.ServiceImageSecond);
                    System.IO.File.Delete(oldImagePath);

                    x.ServiceImageSecondFile.SaveAs(imagePath);
                    xdb.ServiceImageSecond = imageName;
                }
                if (x.ServiceIconFile != null)
                {
                    string imageName = DateTime.Now.ToString("ssfff") + Regex.Replace(x.ServiceIconFile.FileName, "[^A-Za-z0-9.]", "");
                    string imagePath = Path.Combine(Server.MapPath("~/Uploads/"), imageName);

                    string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.ServiceIcon);
                    System.IO.File.Delete(oldImagePath);

                    x.ServiceIconFile.SaveAs(imagePath);
                    xdb.ServiceIcon = imageName;
                }


                db.Entry(xdb).State = EntityState.Modified;
                db.SaveChanges();

                using (DBContext db = new DBContext())
                {
                    foreach (var item in db.ServiceToInfo.Where(c => c.ServiceId == xdb.Id))
                    {
                        db.ServiceToInfo.Remove(item);
                    }

                    db.SaveChanges();
                }

                if (x.ServiceInfoId != null)
                {
                    foreach (var item in x.ServiceInfoId)
                    {
                        ServiceToInfo w = new ServiceToInfo();
                        w.ServiceId = xdb.Id;
                        w.ServiceInfoId = item;

                        db.ServiceToInfo.Add(w);
                        db.SaveChanges();
                    }
                }


                using (DBContext db = new DBContext())
                {
                    foreach (var item in db.ServiceBenefits.Where(c => c.ServiceId == xdb.Id))
                    {
                        db.ServiceBenefits.Remove(item);
                    }

                    db.SaveChanges();
                }

                if (x.Benefits != null)
                {
                    foreach (var item in x.Benefits)
                    {
                        ServiceBenefit w = new ServiceBenefit();

                        w.Content = item;
                        w.ServiceId = xdb.Id;

                        db.ServiceBenefits.Add(w);
                        db.SaveChanges();


                    }
                }
                int AdminId = (int)Session["AdminId"];
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelUpdate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Title, "Service");

                TempData["Update"] = "Update";
                return RedirectToAction("Index", "Service");
            }


            TempData["Update-Error"] = "Update-Error";

            Service xdb2 = db.Service.Include("ServiceToInfo").Include(w => w.ServiceBenefits).FirstOrDefault(w => w.Id == x.Id);


            ViewBag.Benefits = db.ServiceBenefits.Where(c => c.ServiceId == xdb2.Id).ToList();

            ViewBag.ServicePage = true;
            ViewBag.Infos = db.ServiceInfo.ToList();
            return View(x);
        }

        public JsonResult Delete(int? id)
        {

            Service xdb = db.Service.Include("ServiceToInfo").Include(w => w.ServiceBenefits).FirstOrDefault(w => w.Id == id);

          

            string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/"), xdb.ServiceImageFirst);
            System.IO.File.Delete(oldImagePath);

            string oldImagePath2 = Path.Combine(Server.MapPath("~/Uploads/"), xdb.ServiceImageSecond);
            System.IO.File.Delete(oldImagePath2);

            string oldImagePath3 = Path.Combine(Server.MapPath("~/Uploads/"), xdb.ServiceIcon);
            System.IO.File.Delete(oldImagePath3);

            foreach (var item in db.ServiceBenefits.Where(c => c.ServiceId == xdb.Id))
            {
                db.ServiceBenefits.Remove(item);
            }


            db.SaveChanges();

            db.Service.Remove(xdb);

            db.SaveChanges();
            int AdminId = (int)Session["AdminId"];
            string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelDelete(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Title, "Service");

            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateActive(int? id)
        {
            var result = false;

            if (id != null)
            {
                Service xdb = db.Service.FirstOrDefault(w => w.Id == id);


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

            var benefits = new List<ServiceBenefit>();

            foreach(ServiceBenefit item in db.ServiceBenefits.Where(c => c.ServiceId == id))
            {

                benefits.Add(item);
            }

            var xdb = db.Service.Where(c => c.Id == id).Select(s => new
            {

                Id = s.Id,
                Name = s.Title,
                Content = s.Content,
                Desc=s.Description,
                ImageFirst=s.ServiceImageFirst,
                ImageSecond=s.ServiceImageSecond,
                Admin = s.Admin.FullName,
                TotalViews = s.ViewCount,
                PostDate = s.PostDate,
                EditDate = s.ModifiedDate,
                EditAdmin = s.AdminModified.FullName,

            }).FirstOrDefault();

            return Json(new { xdb, benefits, content = HttpUtility.HtmlDecode(xdb.Content), desc = HttpUtility.HtmlDecode(xdb.Desc) }, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
    }
}