using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Wisky
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "DelBrandById",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Brand", action = "Delete", id = UrlParameter.Optional },
                namespaces: new string[] { "DSS.Controllers" }
            );


            routes.MapRoute(
                name: "LoadBrandById",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Brand", action = "Form", id = UrlParameter.Optional },
                namespaces: new string[] { "DSS.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new string[] { "DSS.Controllers" }
            );
        }
    }
}
