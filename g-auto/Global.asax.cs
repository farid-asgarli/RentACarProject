using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace g_auto
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Application["Totaluser"] = 0;
        }

        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    string hostName = Request.Headers["host"].Split(':')[0];
        //    if (hostName.Contains("Main"))
        //    {
        //        Exception exception = Server.GetLastError();
        //        Response.Clear();
        //        HttpException httpException = exception as HttpException;
        //        Response.TrySkipIisCustomErrors = true;

        //        switch (httpException.GetHttpCode())
        //        {
        //            case 404:
        //                Response.StatusCode = 404;
        //                Server.Transfer("~/ErrorPage/ErrorPage.cshtml");
        //                break;
        //            case 500:
        //            default:
        //                Response.StatusCode = 500;
        //                Server.Transfer("~/ErrorPage/ErrorPage.cshtml");
        //                break;
        //        }
        //        Server.ClearError();
        //    }
        //}

        //protected void Session_Start()
        //{
        //    Application.Lock();
        //    Application["Totaluser"] = (int)Application["Totaluser"] + 1;
        //    Application.UnLock();
        //}
    }
}
