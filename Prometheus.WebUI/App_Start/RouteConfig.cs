using System.Web.Mvc;
using System.Web.Routing;

namespace Prometheus.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");




            /* routes for updating Service portfolio */
            routes.MapRoute(
                name: "ShowServiceSection",
                url: "Service/Show/{section}/{id}",
                defaults: new { controller = "Service", action = "Show", section = UrlParameter.Optional, id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "ShowServiceSectionItem",
                url: "Service/ShowServiceSectionItem/{section}/{id}",
                defaults: new { controller = "Service", action = "ShowServiceSectionItem", section = UrlParameter.Optional, id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "UpdateServiceSectionItem",
                url: "Service/UpdateServiceSectionItem/{section}/{id}",
                defaults: new { controller = "Service", action = "UpdateServiceSectionItem", section = UrlParameter.Optional, id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AddServiceSectionItem",
                url: "Service/AddServiceSectionItem/{section}/{id}",
                defaults: new { controller = "Service", action = "AddServiceSectionItem", section = UrlParameter.Optional, id = UrlParameter.Optional }
);






            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
