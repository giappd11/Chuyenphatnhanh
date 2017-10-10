using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Chuyenphatnhanh
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "LocalizedDefault",
               url: "{lang}/{controller}/{action}",
               defaults: new { controller = "Home", action = "Index" },
               constraints: new { lang = "vi-vn|en-us" }
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index",lang= "vi-vn", id = UrlParameter.Optional }  
            );
        }
    }
}
