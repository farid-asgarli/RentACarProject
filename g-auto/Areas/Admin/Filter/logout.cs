using System.Web;
using System.Web.Mvc;

namespace g_auto.Areas.Admin.Filter
{
    public class logout : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["Admin"] == null)
            {
                filterContext.Result = new RedirectResult("~/Admin/Home/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}