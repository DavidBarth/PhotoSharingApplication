using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PhotoSharingApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            
            //add custom routes to application here

            //photo/{id} route
            routes.MapRoute(
                name: "PhotoRoute",
                url: "photo/{id}",
                defaults: new { controller = "Photo", action = "Display" },
                constraints: new { id = "[0-9]+" }
                );
            
            //photo/title route
            routes.MapRoute(
                name: "PhotoTitleRoute",
                url: "photo/title/{title}",
                defaults: new { controller = "Photo", action = "DisplayByTitle" }
                );

            //default route added to the RouteTable object
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}