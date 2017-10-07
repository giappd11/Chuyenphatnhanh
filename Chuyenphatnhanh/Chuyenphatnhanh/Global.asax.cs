using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Optimization;
using System.Web.Routing;
using System.Text;
using System.Threading;
using Chuyenphatnhanh.Controllers;
using NLog;

namespace Chuyenphatnhanh
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new MultiLanguageViewEngine());
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex != null)
            {
                Logger _logger = LogManager.GetCurrentClassLogger();
                StringBuilder err = new StringBuilder();
                err.Append("Error caught in Application_Error event\n");
                err.Append("Error in: " + (Context.Session == null ? string.Empty : Request.Url.ToString()));
                err.Append("\nError Message:" + ex.Message);
                if (null != ex.InnerException)
                {
                    err.Append("\nInner Error Message:" + ex.InnerException.Message);
                    err.Append(ex.InnerException.StackTrace);
                }
                err.Append("\nStack Trace:" + ex.StackTrace);
                Server.ClearError();

                if (null != Context.Session)
                {

                    err.Append($"Session: Identity name:[{Thread.CurrentPrincipal.Identity.Name}] IsAuthenticated:{Thread.CurrentPrincipal.Identity.IsAuthenticated}");
                }
                
                _logger.Error(err.ToString());

                if (null != Context.Session)
                {
                    var routeData = new RouteData();
                    routeData.Values.Add("controller", "Error");
                    routeData.Values.Add("action", "Error");
                    routeData.Values.Add("exception", ex);

                    if (ex.GetType() == typeof(HttpException))
                    {
                        routeData.Values.Add("statusCode", ((HttpException)ex).GetHttpCode());
                    }
                    else
                    {
                        routeData.Values.Add("statusCode", 500);
                    }
                    Response.TrySkipIisCustomErrors = true;
                    IController controller = new ErrorController();
                    controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
                    Response.End();
                }
            }
        }
    }
}
