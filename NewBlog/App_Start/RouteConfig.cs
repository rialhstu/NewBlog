using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NewBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Blognew", action = "Posts", id = UrlParameter.Optional }
            );

            routes.MapRoute(
    "Category",
    "Category/{category}",
    new { controller = "Blognew", action = "Category" }
);

            routes.MapRoute(
    "Tag",
    "Tag/{tag}",
    new { controller = "Blognew", action = "Tag" }
);

            routes.MapRoute(
    "Post",
    "Archive/{year}/{month}/{title}",
    new { controller = "Blognew", action = "Post" }
);

            routes.MapRoute(
    "Login",
    "Login",
    new { controller = "Add", action = "Login" }
);

            routes.MapRoute(
"Logout",
"Logout",
new { controller = "Add", action = "Logout" }
);

            routes.MapRoute(
    "Manage",
    "Manage",
    new { controller = "Add", action = "Manage" }
);

            routes.MapRoute(
                "AddAction",
                "Add/{action}",
                new { controller = "Admin", action = "Login" }
            );

        }
    }
}