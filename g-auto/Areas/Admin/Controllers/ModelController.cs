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
using System.Web.UI.WebControls;

namespace g_auto.Areas.Admin.Controllers
{
    [logout]
    public class ModelController : Controller
    {

        DBContext db = new DBContext();
        // GET: Admin/Model
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.ModelPage = true;

            //List<Model> xdb = db.Model.Include(c => c.Brand).Include("ModelImages").Include("Admin").ToList();
            //return View(xdb);

            ViewBag.NameSortParam = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.IsSortParam = sortOrder == "Is" ? "is_desc" : "Is";
            ViewBag.EngineSortParam = sortOrder == "Engine" ? "engine_desc" : "Engine";
            ViewBag.BrandSortParam = sortOrder == "Brand" ? "brand_desc" : "Brand";
            ViewBag.ELSortParam = sortOrder == "EL" ? "el_desc" : "EL";
            ViewBag.HPSortParam = sortOrder == "HP" ? "hp_desc" : "HP";
            ViewBag.MassSortParam = sortOrder == "Mass" ? "mass_desc" : "Mass";
            ViewBag.MileageSortParam = sortOrder == "MD" ? "md_desc" : "MD";
            ViewBag.PDSortParam = sortOrder == "PD" ? "pd_desc" : "PD";
            ViewBag.RatingSortParam = sortOrder == "Rating" ? "rating_desc" : "Rating";
            ViewBag.TMSortParam = sortOrder == "TM" ? "tm_desc" : "TM";
            ViewBag.YearSortParam = sortOrder == "Year" ? "year_desc" : "Year";
            ViewBag.ConditionSortParam = sortOrder == "Condition" ? "condition_desc" : "Condition";
            ViewBag.DTSortParam = sortOrder == "DT" ? "dt_desc" : "DT";
            ViewBag.FTSortParam = sortOrder == "FT" ? "ft_desc" : "FT";




            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var xdb = from x in db.Model.Include(c => c.Brand).Include("ModelImages").Include("Admin")
                      select x;
            if (!String.IsNullOrEmpty(searchString))
            {
                xdb = xdb.Where(s => s.Name.ToString().Contains(searchString)
                                       || s.Brand.Name.ToString().Contains(searchString)
                                       || s.Engine.ToString().Contains(searchString) || s.Color.ToString().Contains(searchString) || s.DriveTrain.ToString().Contains(searchString) || s.EngineLayout.ToString().Contains(searchString) || s.Engine.ToString().Contains(searchString)
                                        || s.HorsePower.ToString().Contains(searchString) || s.Mass.ToString().Contains(searchString) || s.FuelType.ToString().Contains(searchString) || s.Mileage.ToString().Contains(searchString) || s.PriceDaily.ToString().Contains(searchString)
                                         || s.Condition.ToString().Contains(searchString) || s.Transmission.ToString().Contains(searchString) || s.Year.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    xdb = xdb.OrderByDescending(s => s.Name);
                    break;
                case "Name":
                    xdb = xdb.OrderBy(s => s.Name);
                    break;
                case "dt_desc":
                    xdb = xdb.OrderByDescending(s => s.DriveTrain.ToString());
                    break;
                case "DT":
                    xdb = xdb.OrderBy(s => s.DriveTrain.ToString());
                    break;
                case "seat_desc":
                    xdb = xdb.OrderByDescending(s => s.Seats);
                    break;
                case "Seat":
                    xdb = xdb.OrderBy(s => s.Seats);
                    break;
                case "Date":
                    xdb = xdb.OrderBy(s => s.PostDate);
                    break;
                case "date_desc":
                    xdb = xdb.OrderByDescending(s => s.PostDate);
                    break;
                case "brand_desc":
                    xdb = xdb.OrderByDescending(s => s.Brand != null ? s.Brand.Name : "");
                    break;
                case "Brand":
                    xdb = xdb.OrderBy(s => s.Brand != null ? s.Brand.Name : "");
                    break;
                case "engine_desc":
                    xdb = xdb.OrderByDescending(s => s.Engine);
                    break;
                case "Engine":
                    xdb = xdb.OrderBy(s => s.Engine);
                    break;
                case "condition_desc":
                    xdb = xdb.OrderByDescending(s => s.Condition);
                    break;
                case "Condition":
                    xdb = xdb.OrderBy(s => s.Condition);
                    break;
                case "doors_desc":
                    xdb = xdb.OrderByDescending(s => s.Doors);
                    break;
                case "Doors":
                    xdb = xdb.OrderBy(s => s.Doors);
                    break;

                case "el_desc":
                    xdb = xdb.OrderByDescending(s => s.EngineLayout);
                    break;
                case "EL":
                    xdb = xdb.OrderBy(s => s.EngineLayout);
                    break;
                case "ft_desc":
                    xdb = xdb.OrderByDescending(s => s.FuelType);
                    break;
                case "FT":
                    xdb = xdb.OrderBy(s => s.FuelType);
                    break;
                case "hp_desc":
                    xdb = xdb.OrderByDescending(s => s.HorsePower);
                    break;
                case "HP":
                    xdb = xdb.OrderBy(s => s.HorsePower);
                    break;

                case "mass_desc":
                    xdb = xdb.OrderByDescending(s => s.Mass);
                    break;
                case "Mass":
                    xdb = xdb.OrderBy(s => s.Mass);
                    break;

                case "md_desc":
                    xdb = xdb.OrderByDescending(s => s.Mileage);
                    break;
                case "MD":
                    xdb = xdb.OrderBy(s => s.Mileage);
                    break;
                case "pd_desc":
                    xdb = xdb.OrderByDescending(s => s.PriceDaily);
                    break;
                case "PD":
                    xdb = xdb.OrderBy(s => s.PriceDaily);
                    break;
                case "color_desc":
                    xdb = xdb.OrderByDescending(s => s.Color);
                    break;
                case "Color":
                    xdb = xdb.OrderBy(s => s.Color);
                    break;
                case "tm_desc":
                    xdb = xdb.OrderByDescending(s => s.Transmission);
                    break;
                case "TM":
                    xdb = xdb.OrderBy(s => s.Transmission);
                    break;
                case "year_desc":
                    xdb = xdb.OrderByDescending(s => s.Year);
                    break;
                case "Year":
                    xdb = xdb.OrderBy(s => s.Year);
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
            ViewBag.ModelPage = true;

            ViewBag.Admin = (int)Session["AdminId"];
            ViewBag.Brand = db.Brand.ToList();

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Model x)
        {
            int AdminId = (int)Session["AdminId"];
            if (ModelState.IsValid)
            {
                Model xdb = new Model();

                xdb.AdminId = x.AdminId;
                xdb.PostDate = DateTime.Now;

                xdb.BrandId = x.BrandId;

                xdb.Color = x.Color;
                xdb.Condition = x.Condition;
                xdb.Doors = x.Doors;
                xdb.Engine = x.Engine;
                xdb.EngineLayout = x.EngineLayout;
                xdb.FuelType = x.FuelType;
                xdb.HorsePower = x.HorsePower;
                xdb.Mass = x.Mass;
                xdb.Mileage = x.Mileage;
                xdb.Name = x.Name;
                xdb.PriceDaily = x.PriceDaily;
                xdb.Seats = x.Seats;
                xdb.Transmission = x.Transmission;
                xdb.DriveTrain = x.DriveTrain;
                xdb.Year = x.Year;
                xdb.hasABS = x.hasABS;
                xdb.hasAlloyWheels = x.hasAlloyWheels;
                xdb.hasCC = x.hasCC;
                xdb.hasConditioner = x.hasConditioner;
                xdb.hasESP = x.hasESP;
                xdb.hasLeatherInterior = x.hasLeatherInterior;
                xdb.hasPSensors = x.hasPSensors;
                xdb.hasXenon = x.hasXenon;

                xdb.Description = x.Description;



                if (db.AdminSettings.FirstOrDefault(c => c.AdminId == AdminId).alwaysActive)
                {
                    xdb.isActive = true;
                }
                else
                {
                    xdb.isActive = false;
                }


                db.Model.Add(xdb);
                db.SaveChanges();


                if (x.ImageFile != null)
                {
                    foreach (HttpPostedFileBase image in x.ImageFile)
                    {
                        if (image == null)
                        {
                            xdb.ModelImages = null;
                        }
                        else
                        {
                            string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssffff") + Regex.Replace(image.FileName, "[^A-Za-z0-9.]", "");
                            string imagePath = Path.Combine(Server.MapPath("~/Uploads"), imageName);

                            image.SaveAs(imagePath);

                            ModelImages w = new ModelImages();
                            w.Name = imageName;
                            w.ModelId = xdb.Id;

                            db.ModelImages.Add(w);
                            db.SaveChanges();
                        }

                    }
                }
                TempData["Create"] = "Create";
     
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelCreate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Name, "Model");

                return RedirectToAction("Index", "Model");


            }
            ViewBag.ModelPage = true;


            ViewBag.Admin = (int)Session["AdminId"];
            ViewBag.Brand = db.Brand.ToList();
            TempData["Create-Error"] = "Create-Error";


            return View(x);
        }

        public ActionResult Update(int id)
        {

            Model xdb = db.Model.Include("Brand").Include("ModelImages").FirstOrDefault(c => c.Id == id);
            ViewBag.ModelPage = true;


            ViewBag.Brand = db.Brand.ToList();

            if (xdb == null)
            {
                return RedirectToAction("Oops", "Error");
            }

            return View(xdb);
        }


        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Update(Model x)
        {
            if (ModelState.IsValid)
            {
                Model xdb = db.Model.Include("Brand").Include("ModelImages").FirstOrDefault(c => c.Id == x.Id);


                xdb.BrandId = x.BrandId;

                xdb.Color = x.Color;
                xdb.Condition = x.Condition;
                xdb.Doors = x.Doors;
                xdb.Engine = x.Engine;
                xdb.EngineLayout = x.EngineLayout;
                xdb.FuelType = x.FuelType;
                xdb.HorsePower = x.HorsePower;
                xdb.Mass = x.Mass;
                xdb.Mileage = x.Mileage;
                xdb.Name = x.Name;
                xdb.PriceDaily = x.PriceDaily;
                xdb.Seats = x.Seats;
                xdb.Transmission = x.Transmission;
                xdb.Year = x.Year;
                xdb.DriveTrain = x.DriveTrain;
                xdb.hasABS = x.hasABS;
                xdb.hasAlloyWheels = x.hasAlloyWheels;
                xdb.hasCC = x.hasCC;
                xdb.hasConditioner = x.hasConditioner;
                xdb.hasESP = x.hasESP;
                xdb.hasLeatherInterior = x.hasLeatherInterior;
                xdb.hasPSensors = x.hasPSensors;
                xdb.hasXenon = x.hasXenon;
                xdb.Description = x.Description;

                xdb.ModifiedDate = DateTime.Now;
                xdb.AdminModifiedId = (int)Session["AdminId"];




                db.Entry(xdb).State = EntityState.Modified;


                db.SaveChanges();


                if (x.ImageFile[0] != null)
                {
                    using (DBContext db = new DBContext())
                    {
                        foreach (var item in db.ModelImages.Where(c => c.ModelId == xdb.Id))
                        {
                            string oldImagePath = Path.Combine(Server.MapPath("~/Uploads"), item.Name);
                            System.IO.File.Delete(oldImagePath);

                            db.ModelImages.Remove(item);
                        }
                        db.SaveChanges();
                    }


                    foreach (HttpPostedFileBase image in x.ImageFile)
                    {


                        string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssffff") + Regex.Replace(image.FileName, "[^A-Za-z0-9.]", "");
                        string imagePath = Path.Combine(Server.MapPath("~/Uploads"), imageName);

                        image.SaveAs(imagePath);

                        ModelImages w = new ModelImages();
                        w.Name = imageName;
                        w.ModelId = xdb.Id;

                        db.ModelImages.Add(w);
                        db.SaveChanges();


                    }

                    db.SaveChanges();

                }

                int AdminId = (int)Session["AdminId"];
                string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelUpdate(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Name, "Model");

                TempData["Update"] = "Update";
                return RedirectToAction("Index");
            }


            Model xdb2 = db.Model.Include("Brand").Include("ModelImages").FirstOrDefault(p => p.Id == x.Id);

            ViewBag.ModelImages = db.ModelImages.Where(c => c.ModelId == xdb2.Id).ToList();

            ViewBag.Brand = db.Brand.ToList();
            ViewBag.ModelPage = true;

            TempData["Update-Error"] = "Update-Error";
            return View(x);
        }

        public JsonResult Delete(int? id)
        {
            Model xdb = db.Model.Include("Brand").Include("ModelImages").FirstOrDefault(p => p.Id == id);




            foreach (var item in db.ModelImages.Where(c => c.ModelId == xdb.Id))
            {
                string oldImagePath = Path.Combine(Server.MapPath("~/Uploads"), item.Name);
                System.IO.File.Delete(oldImagePath);

                db.ModelImages.Remove(item);
            }


            db.Model.Remove(xdb);

            db.SaveChanges();

            int AdminId = (int)Session["AdminId"];
            string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;
            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelDelete(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Name, "Model");

            return Json(JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateActive(int? id)
        {
            var result = false;

            if (id != null)
            {
                Model xdb = db.Model.FirstOrDefault(w => w.Id == id);


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

            var xdb = db.Model.Where(c => c.Id == id).Select(s => new
            {

                Id = s.Id,
                BrandName = s.Brand.Name,
                Name = s.Name,
                Condition = s.Condition,
                Price = s.PriceDaily,
                Desc = s.Description,
                Admin = s.Admin.FullName,
                PostDate = s.PostDate,
                EditDate = s.ModifiedDate,
                EditAdmin = s.AdminModified.FullName,
                images = s.ModelImages.ToList(),
                s.Color,
                s.Doors,
                s.Engine,
                s.EngineLayout,
                s.FuelType,
                s.HorsePower,
                s.Mass,
                s.Mileage,
                s.Transmission,
                s.Year,
                TotalViews = s.ViewCount



            }).FirstOrDefault();

         



            return Json(new { xdb, desc = HttpUtility.HtmlDecode(xdb.Desc) }, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
    }
}