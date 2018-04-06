using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UsersDiosna
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Api",
                url: "api/{controller}/{method}/{id}",
                defaults: new { controller = "Api", action = "Index", page = UrlParameter.Optional, count = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "MobileApi",
                url: "MobileApi/{action}/{mobileToken}",
                defaults: new { controller = "MobileApi", action = "Get", mobileToken = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Alarms",
                url: "Alarm/{action}/{page}/{count}/",
                defaults: new { controller = "Alarm", action = "Index", page = UrlParameter.Optional, count = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Report",
                url: "{controller}/{action}/{month}/{year}",
                defaults: new { controller = "Home", action = "Index", month = UrlParameter.Optional, year = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ReportDosing",
                url: "{controller}/{action}/{day}/{month}/{year}",
                defaults: new { controller = "Home", action = "Index", day=UrlParameter.Optional, month = UrlParameter.Optional, year = UrlParameter.Optional }
            );
        }
    }
}
