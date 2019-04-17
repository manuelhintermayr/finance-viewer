using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FinanceViewer.Net
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("_nuxt/*");
            routes.IgnoreRoute("favicon.ico");

            routes.MapRoute("home", "", defaults: new { controller = "Home", action = "Index" });
            routes.MapRoute("admin", "admin/", defaults: new { controller = "Admin", action = "Admin" });
            routes.MapRoute("dashboard", "dashboard/", defaults: new { controller = "Dashboard", action = "Dashboard" });
            //routes.MapRoute("login", "login.php", defaults: new { controller = "Api/Login", action = "Post"});
        }
    }
}
