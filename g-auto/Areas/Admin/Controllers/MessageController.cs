using g_auto.Areas.Admin.Filter;
using g_auto.DAL;
using g_auto.Hubs;
using g_auto.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace g_auto.Areas.Admin.Controllers
{
    [logout]
    public class MessageController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/Message
        public ActionResult Index()
        {
            ViewBag.MessageCount = db.Message.Count();
            ViewBag.MessagePage = true;
            List<Message> xdb = db.Message.OrderByDescending(c => c.PostDate).Skip(0).Take(50).ToList();

            return View(xdb);
        }

        public ActionResult Details(int? id)
        {

            ViewBag.MessagePage = true;
            Message xdb = db.Message.FirstOrDefault(c => c.Id == id);

            return View(xdb);
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
        public JsonResult MessageCountChecker()
        {
            var response = db.Message.Count();

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InboxScroll(int part)
        {
            var response = db.Message.OrderByDescending(c => c.PostDate).Skip(50 * part).Take(50).Select(s => new
            {

                Subject = s.Subject,
                PostMonth = s.PostDate.Month,
                PostDay = s.PostDate.Day,
                PostHour = s.PostDate.Hour,
                PostMin = s.PostDate.Minute,
                Name = s.Name,
                IsRead = s.isRead,
                Id = s.Id
            }).ToList();
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModalContent(int id)
        {
            var xdb = db.Message.Include("MessageReplies").Include("MessageReplies.Admin").Where(c => c.Id == id).Select(s => new
            {
                Id = s.Id,
                Content = s.Content,
                Subject = s.Subject,
                Name = s.Name,
                Email = s.Email,
                Phone = s.Phone,
                Day = s.PostDate.Day,
                Month = s.PostDate.Month,
                Hour = s.PostDate.Hour,
                Min = s.PostDate.Minute,
                IsReplied = s.isReplied,

            }) ;


            return Json(xdb, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ModalContentReply(int id)
        {

            var response = db.MessageReplies.Where(c => c.MessageId == id).Select(s => new {

                ReplyContent = s.Content,
                ReplyAdmin = s.Admin.FullName,
                ReplyDay = s.PostDate.Day,
                ReplyMonth = s.PostDate.Month,
                ReplyHour = s.PostDate.Hour,
                ReplyMin = s.PostDate.Minute,

            }).ToList();


            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MarkAsRead(int id)
        {

            Message xdb = db.Message.FirstOrDefault(c => c.Id == id);

            xdb.isRead = true;

            db.Entry(xdb).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            var response = "";


            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MessageSearch(string text)
        {


            var response = db.Message.Where(c => c.Email.Contains(text) || c.Content.Contains(text) || c.Subject.Contains(text) || c.Name.Contains(text)).OrderByDescending(c => c.PostDate).Select(s => new
            {

                Subject = s.Subject,
                PostMonth = s.PostDate.Month,
                PostDay = s.PostDate.Day,
                PostHour = s.PostDate.Hour,
                PostMin = s.PostDate.Minute,
                Name = s.Name,
                IsRead = s.isRead,
                Id = s.Id


            }).ToList();


            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReplyToCustomer(MessageReply x)
        {
            int AdminId = (int)Session["AdminId"];

            string AdminName = db.AdminSettings.FirstOrDefault(c => c.AdminId == AdminId).DisplayName;

            var response = "";

            if (x != null)
            {
                MessageReply xdb = new MessageReply();


                xdb.Content = x.Content;
                xdb.PostDate = DateTime.Now;
                xdb.AdminId = AdminId;
                xdb.MessageId = x.MessageId;
                

                db.MessageReplies.Add(xdb);

                db.SaveChanges();

                string FilePath = Server.MapPath("~/Assets/template/EmailReplyTemplate.cshtml");
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();


                Message m = db.Message.FirstOrDefault(c => c.Id == xdb.MessageId);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("autocarrenttm2020@gmail.com", "autocarrenttm2020@gmail.com");
                mail.To.Add(new MailAddress(m.Email));
                mail.Subject = "RE:" + m.Subject + "";

                MailText = MailText.Replace("[UserName]", m.Name.Trim());
                MailText = MailText.Replace("[AdminName]", AdminName.Trim());
                MailText = MailText.Replace("[ReplyContent]", xdb.Content.Trim());


                mail.Body = MailText;
                //mail.Body = "<html><body><p> Dear " + m.Name + ",</p ><p> Thank you for your message.</p><br></br><p> " + xdb.Content + " </p><br></br><p> If you have any questions, please do not hesitate to contact us again.</p><p> Sincerely,<br><strong> " + AdminName + " </strong> from Autlines</br ></p></body></html>";

                mail.IsBodyHtml = true;
                
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("autocarrenttm2020@gmail.com", "@utoc@rRENT2020");

                smtp.Send(mail);

               

                m.isReplied = true;


                db.Entry(m).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();


               
                response = "true";

            }



            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            Message xdb = db.Message.Find(id);


            db.Message.Remove(xdb);
            db.SaveChanges();

            int AdminId = (int)Session["AdminId"];
            string ConnectionId = db.Admin.FirstOrDefault(c => c.Id == AdminId).ConnectionId;

            GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.AllExcept(ConnectionId).modelDelete(((Models.Admin)Session["Admin"]).FullName, DateTime.Now.ToString("HH:mm"), xdb.Id, "Tag");

            return Json(JsonRequestBehavior.AllowGet);
        }

    }
}
