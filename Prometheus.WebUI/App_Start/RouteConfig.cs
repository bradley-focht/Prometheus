using System.Web.Mvc;
using System.Web.Routing;

namespace Prometheus.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            /* routes for showing service and filtering */
            routes.MapRoute(
                name: "Show",
                url: "Service/Index/{filterBy}/{filterArg}/{pageId}",
                defaults: new { controller = "Service", action = "Index", pageId = UrlParameter.Optional }
                );

            /* routes for updating Service portfolio */


            routes.MapRoute(
                name: "ShowServiceSection",
                url: "Service/Show/{section}/{id}",
                defaults: new { controller = "Service", action = "Show", section = UrlParameter.Optional, id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "ShowServiceSectionItem",
                url: "Service/ShowServiceSectionItem/{serviceId}/{section}/{id}",
                defaults: new { controller = "Service", action = "ShowServiceSectionItem"}
            );

            routes.MapRoute(
                name: "UpdateServiceSectionItem",
                url: "Service/UpdateServiceSectionItem/{serviceId}/{section}/{id}",
                defaults: new { controller = "Service", action = "UpdateServiceSectionItem"}
            );

            routes.MapRoute(
                name: "AddServiceSectionItem",
                url: "Service/AddServiceSectionItem/{section}/{id}",
                defaults: new { controller = "Service", action = "AddServiceSectionItem", parentId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ConfirmDeleteServiceSectionItem",
                url: "Service/ConfirmDeleteServiceSectionItem/{section}/{id}",
                defaults: new { controller = "Service", action = "ConfirmDeleteServiceSectionItem", section = UrlParameter.Optional, id = UrlParameter.Optional }
            );        

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
