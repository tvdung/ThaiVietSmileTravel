using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ThaiViet_Smile_Travel
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[]{ "ThaiViet_Smile_Travel.Controllers" }
            );

            routes.MapRoute(
               name: "Tour",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Tour", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "ThaiViet_Smile_Travel.Controllers" }
           );

            routes.MapRoute(
              name: "Card",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Card", action = "Index", id = UrlParameter.Optional },
              namespaces: new string[] { "ThaiViet_Smile_Travel.Controllers" }
          );

            routes.MapRoute(
              name: "OrderTour",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Card", action = "OrderTour", id = UrlParameter.Optional },
              namespaces: new string[] { "ThaiViet_Smile_Travel.Controllers" }
          );

            
        }
    }
}
