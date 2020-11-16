using g_auto.DAL;
using g_auto.Hubs;
using g_auto.Models;
using g_auto.VM;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace g_auto.Controllers
{
    public class BlogGController : Controller
    {
        DBContext db = new DBContext();
        // GET: BlogG
        public ActionResult Index()
        {
            #region Cart list

            HttpCookie cookieCart = Request.Cookies["Cart"];
            List<string> CartList = new List<string>();
            if (cookieCart != null)
            {
                CartList = cookieCart.Value.Split(',').ToList();
                CartList.RemoveAt(CartList.Count - 1);

                ViewBag.CartList = CartList;
                ViewBag.CartListCount = CartList.Count;
            }
            else
            {
                ViewBag.CartListCount = 0;
            }

            List<Product> products = new List<Product>();

            foreach (var item in CartList)
            {
                foreach (var prd in db.Products.Include("ProductImages").Include("Admin").Include("ProductToCategory").Include("ProductToCategory.ProductCategory").ToList())
                {
                    if (Convert.ToInt32(item.Split('-')[0]) == prd.Id)
                    {
                        prd.Count = Convert.ToDecimal(item.Split('-')[1]);
                        products.Add(prd);
                    }
                }

            }
            #endregion

            ViewBag.Products = products;
            ViewBag.BlogPage = true;
            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();
            ViewBag.Address = db.Layout.FirstOrDefault().Address;
            ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
            ViewBag.Email = db.Layout.FirstOrDefault().Email;
            ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
            ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;



            BlogVM v = new BlogVM();

            v.Blogs = db.Blog.Include("BlogToTags").Include("BlogToTags.Tag").Include("BlogToCategory").Include("BlogToCategory.BlogCategory").Include("Admin").Include("Admin.AdminSettings").Where(c => c.isActive).OrderByDescending(c=>c.PostDate).Skip(0).Take(4).ToList();

            v.Categories = db.BlogCategory.Include("BlogToCategory").Include("BlogToCategory.Blog").ToList();

            v.Tags = db.Tags.Include("BlogToTags").Include("BlogToTags.Blog").ToList();

            return View(v);
        }

        public ActionResult Details(int id)
        {
            #region Cart list

            HttpCookie cookieCart = Request.Cookies["Cart"];
            List<string> CartList = new List<string>();
            if (cookieCart != null)
            {
                CartList = cookieCart.Value.Split(',').ToList();
                CartList.RemoveAt(CartList.Count - 1);

                ViewBag.CartList = CartList;
                ViewBag.CartListCount = CartList.Count;
            }
            else
            {
                ViewBag.CartListCount = 0;
            }

            List<Product> products = new List<Product>();

            foreach (var item in CartList)
            {
                foreach (var prd in db.Products.Include("ProductImages").Include("Admin").Include("ProductToCategory").Include("ProductToCategory.ProductCategory").ToList())
                {
                    if (Convert.ToInt32(item.Split('-')[0]) == prd.Id)
                    {
                        prd.Count = Convert.ToDecimal(item.Split('-')[1]);
                        products.Add(prd);
                    }
                }

            }
            #endregion

            ViewBag.Products = products;
            ViewBag.Address = db.Layout.FirstOrDefault().Address;
            ViewBag.Phone = db.Layout.FirstOrDefault().Phone;
            ViewBag.Email = db.Layout.FirstOrDefault().Email;
            ViewBag.FooterLogo = db.Layout.FirstOrDefault().LogoFooter;
            ViewBag.HeaderLogo = db.Layout.FirstOrDefault().Logo;
            ViewBag.BlogPage = true;
            ViewBag.Blogs = db.Blog.Where(c => c.isActive).OrderByDescending(c => c.PostDate).Take(3).ToList();


            BlogVM v = new BlogVM();

            v.Blog = db.Blog.Include("BlogToTags").Include("BlogToTags.Tag").Include("BlogToCategory").Include("BlogToCategory.BlogCategory").Include("Admin").Include("Admin.AdminSettings").FirstOrDefault(c=>c.Id==id &&c.isActive);
           
            v.Blogs = db.Blog.Include("BlogToTags").Include("BlogToTags.Tag").Include("BlogToCategory").Include("BlogToCategory.BlogCategory").Include("Admin").Include("Admin.AdminSettings").Include("Comments").Include("Comments.User").Include("Comments.Replies").Include("Comments.Replies.User").ToList();

            v.Categories = db.BlogCategory.Include("BlogToCategory").Include("BlogToCategory.Blog").ToList();

            v.Tags = db.Tags.Include("BlogToTags").Include("BlogToTags.Blog").ToList();



         
            if (Session["VCB-" + id] == null)
            {
                Session["VCB-" + id] = true;


                v.Blog.ViewCount=v.Blog.ViewCount+1;

                db.Entry(v.Blog).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return View(v);
        }

        public JsonResult PostComment(Comment x)
        {
            if (Session["User"] != null)
            {

                Comment xdb = new Comment();

                xdb.BlogId = x.BlogId;
                xdb.Content = x.Content;
                xdb.PostedDate = DateTime.Now;
                xdb.UserId = x.UserId;

                db.Comment.Add(xdb);

                db.SaveChanges();

                Blog blog = db.Blog.Find(xdb.BlogId);

                User usr = (User)Session["User"];
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.All.postComment(usr.FullName, DateTime.Now.ToString("HH:mm"), blog.Title, blog.Id);

                var response = db.Comment.Where(c => c.Id == xdb.Id).Select(s => new
                {
                    CommentContent = s.Content,
                    Name = s.User.FullName,
                    ProfPic = s.User.ProfilePicture,
                    dataBlogId = s.BlogId,
                    dataUserId = s.UserId,
                    dataCommentId = s.Id,
                    PostDay = s.PostedDate.Day,
                    PostMonth = s.PostedDate.Month,
                    PostYear = s.PostedDate.Year,
                    PostHour = s.PostedDate.Hour,
                    PostMinute = s.PostedDate.Minute,
                }).FirstOrDefault();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("nousersession", JsonRequestBehavior.AllowGet);

            }
        }



        public JsonResult PostReply(Reply x)
        {

            if (Session["User"] != null)
            {
                Reply xdb = new Reply();

                xdb.BlogId = x.BlogId;
                xdb.CommentId = x.CommentId;
                xdb.Content = x.Content;
                xdb.UserId = x.UserId;
                xdb.PostedDate = DateTime.Now;

                db.Replies.Add(xdb);

                db.SaveChanges();

                Blog blog = db.Blog.Find(xdb.BlogId);

                User usr = (User)Session["User"];
                GlobalHost.ConnectionManager.GetHubContext<NotifyHub>().Clients.All.postComment(usr.FullName, DateTime.Now.ToString("HH:mm"), blog.Title, blog.Id);

                var response = db.Replies.Where(c => c.Id == xdb.Id).Select(s => new {
                    CommentContent = s.Content,
                    Name = s.User.FullName,
                    ProfPic = s.User.ProfilePicture,
                    dataBlogId = s.BlogId,
                    dataUserId = s.UserId,
                    dataCommentId = s.CommentId,
                    PostDay = s.PostedDate.Day,
                    PostMonth = s.PostedDate.Month,
                    PostYear = s.PostedDate.Year,
                    PostHour = s.PostedDate.Hour,
                    PostMinute = s.PostedDate.Minute,
                }).FirstOrDefault();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("nousersession", JsonRequestBehavior.AllowGet);

            }
        }


        public JsonResult FetchProfPic()
        {
            if (Session["UserId"] != null)
            {

                int userId = (int)Session["UserId"];



                var response = db.User.Find(userId).ProfilePicture;
                

                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("nopic",JsonRequestBehavior.AllowGet);
            }
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
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
        public JsonResult IndexScroll(int part)
        {
            var response = db.Blog.Where(c=>c.isActive).OrderByDescending(c => c.PostDate).Skip(4*part).Take(4).Select(s=> new { 
            Image=s.BlogCoverImage,
            BlogId=s.Id,
            Title = s.Title,
            Description =s.Description,
            Admin = s.Admin.AdminSettings.FirstOrDefault().DisplayName,
            Category =s.BlogToCategory.FirstOrDefault().BlogCategory.Name,
            PostMonth =s.PostDate.Month,
            PostDay =s.PostDate.Day,
            Content=s.Content
            }).ToList();
            return Json(response, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }


        public JsonResult BringCat(int id)
        {
            var response = db.BlogToCategory.Where(c => c.BlogCategoryId == id && c.Blog.isActive).Select(s => new
            {
                CatName= s.BlogCategory.Name,
                Image = s.Blog.BlogCoverImage,
                BlogId = s.Blog.Id,
                Title = s.Blog.Title,
                Description = s.Blog.Description,
                Admin = s.Blog.Admin.AdminSettings.FirstOrDefault().DisplayName,
                Category = s.Blog.BlogToCategory.FirstOrDefault().BlogCategory.Name,
                PostMonth = s.Blog.PostDate.Month,
                PostDay = s.Blog.PostDate.Day,
                Content = s.Blog.Content


            }).ToList();



         return Json(response, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);

        }


        public JsonResult BringTag(int id)
        {
            var response = db.BlogToTags.Where(c => c.TagId == id &&c.Blog.isActive).Select(s => new
            {
                TagName = s.Tag.Name,
                Image = s.Blog.BlogCoverImage,
                BlogId = s.Blog.Id,
                Title = s.Blog.Title,
                Description = s.Blog.Description,
                Admin = s.Blog.Admin.AdminSettings.FirstOrDefault().DisplayName,
                Category = s.Blog.BlogToCategory.FirstOrDefault().BlogCategory.Name,
                PostMonth = s.Blog.PostDate.Month,
                PostDay = s.Blog.PostDate.Day,
                Content = s.Blog.Content


            }).ToList();



            return Json(response, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);

        }

        public JsonResult BlogSearch(string searchData)
        {
            var response = db.Blog.Where(c => c.isActive).Where(c => c.Title.Contains(searchData) || c.Description.Contains(searchData) || c.Content.Contains(searchData) || c.BlogToTags.FirstOrDefault().Tag.Name.Contains(searchData) || c.BlogToCategory.FirstOrDefault().BlogCategory.Name.Contains(searchData)).Select(s=> new{

            Image = s.BlogCoverImage,
            BlogId = s.Id,
            Title = s.Title,
            Description = s.Description,
            Admin = s.Admin.AdminSettings.FirstOrDefault().DisplayName,
            Category = s.BlogToCategory.FirstOrDefault().BlogCategory.Name,
            PostMonth = s.PostDate.Month,
            PostDay = s.PostDate.Day,
            Content = s.Content
            }).ToList();





            return Json(response, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);


        }
    }
}