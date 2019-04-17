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
            //Static Files
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("_nuxt/*");
            routes.IgnoreRoute("favicon.ico");

            //Views
            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute("admin", "admin/", defaults: new { controller = "Admin", action = "Admin" });
            routes.MapRoute("dashboard", "dashboard/", defaults: new { controller = "Dashboard", action = "Dashboard" });

            //Api
            routes.MapRoute(
                "ApiLogin",
                "login.php/",
                new { controller = "Login", action = "post" },
                new { httpMethod = new HttpMethodConstraint("POST") }
            );
            routes.MapRoute(
                "ApiLogout",
                "logout.php/",
                new { controller = "Logout", action = "Index" }
            );

            routes.MapRoute(
                "ApiAdminOptions",
                "admin/options.php",
                new { controller = "AdminOptions", action = "Index" }
            );

            routes.MapRoute(
                "ApiDashboardOptions",
                "dashboard/options.php",
                new { controller = "DashboardOptions", action = "Index" }
            );

        }
    }
}
