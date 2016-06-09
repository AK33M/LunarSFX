using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LunarSFX
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                        "Manage",
                        "Manage",
                        new { controller = "Admin", action = "Manage" }
                    );

            routes.MapRoute(
                        "Category",
                        "Category/{category}",
                        new { controller = "Blog", action = "Category" }
                    );         
             
            routes.MapRoute(
                        "Tag",
                        "Tag/{tag}",
                        new { controller = "Blog", action = "Tag" }
                    );

            routes.MapRoute(
                        "Post",
                        "Archive/{year}/{month}/{title}",
                        new { controller = "Blog", action = "Post" }
                    );

            routes.MapRoute(
                         "Auth",
                         "Auth/{action}",
                         new { controller = "Auth", action = "LogIn" }
                     );

            routes.MapRoute(
                        "Action",
                        "{action}",
                        new { controller = "Blog", action = "Posts" }
                    );

            //routes.MapRoute(
            //          "Default",
            //          "{controller}/{action}/{id}",
            //          new { controller = "Blog", action = "Posts", id = UrlParameter.Optional }
            //       );
        }
    }
}
