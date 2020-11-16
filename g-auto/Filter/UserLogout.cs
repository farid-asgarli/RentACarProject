using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace g_auto.Filter
{
    public class UserLogout: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["User"] == null)
            {
                filterContext.Result = new RedirectResult("~/Main/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}