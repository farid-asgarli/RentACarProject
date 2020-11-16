using g_auto.DAL;
using g_auto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace g_auto.Areas.Admin.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Admin/Error

        public ActionResult Oops(int Id)
        {
            

            Response.StatusCode = Id;

            return View();
        }
    }
}