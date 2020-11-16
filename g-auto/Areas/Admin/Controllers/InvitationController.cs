using g_auto.Areas.Admin.Filter;
using g_auto.DAL;
using g_auto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace g_auto.Areas.Admin.Controllers
{
    [logout]
    public class InvitationController : Controller
    {
        DBContext db = new DBContext();
       

        public JsonResult CodeGenerate()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[64];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);


            return Json(finalString, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PostCode(string code)
        {
            if (db.TempAccessCodes.FirstOrDefault(c => c.Code == code) == null)
            {
                TempAccessCodes xdb = new TempAccessCodes();

                xdb.Code = code;
                xdb.PostDate = DateTime.Now;
                db.TempAccessCodes.Add(xdb);
                db.SaveChanges();
            }


            return Json(JsonRequestBehavior.AllowGet);

        }

    }

}