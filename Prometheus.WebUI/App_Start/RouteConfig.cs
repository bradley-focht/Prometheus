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

            #region Service Portfolio
            routes.MapRoute(
                name: "ShowServiceSection",
                url: "Service/Show/{section}/{id}/{pageId}",
                defaults: new { controller = "Service", action = "Show", section = UrlParameter.Optional, id = UrlParameter.Optional, pageId = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "ShowServiceSectionItem",
                url: "Service/ShowServiceSectionItem/{serviceId}/{section}/{id}",
                defaults: new { controller = "Service", action = "ShowServiceSectionItem" }
            );

            routes.MapRoute(
                name: "UpdateServiceSectionItem",
                url: "Service/UpdateServiceSectionItem/{serviceId}/{section}/{id}",
                defaults: new { controller = "Service", action = "UpdateServiceSectionItem" }
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
                    name: "AddServiceOption",
                    url:"Service/AddServiceOption/{id}/{categoryId}",
                    defaults: new {controller="Service", action="AddServiceOption", categoryId = UrlParameter.Optional }
            );

            #endregion

            #region Service Catalog

            routes.MapRoute(
                name: "ServiceOptions",
                url: "ServiceCatalog/ServiceOptions/{type}/{serviceId}",
                defaults: new { controller = "ServiceCatalog", action = "ServiceOptions" });

            routes.MapRoute(
                name: "ServiceDetails",
                url: "ServiceCatalog/Details/{catalog}/{type}/{id}/{serviceId}",
                defaults: new { controller = "ServiceCatalog", action = "Details", serviceId = UrlParameter.Optional });


            routes.MapRoute(
                name: "ServiceCatalogIndex",
                url: "ServiceCatalog/Index/{type}/{id}",
                defaults: new { controller = "ServiceCatalog", action = "Index", type = UrlParameter.Optional, id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "SearviceCatalogSearch",
                url: "ServiceCatalog/CatalogSearch/{type}/{pageId}/{searchString}",
                defaults: new { controller = "ServiceCatalog", action = "CatalogSearch", pageId = UrlParameter.Optional, searchString = UrlParameter.Optional }
                );

            #endregion

            #region Service Request Maintenance

            routes.MapRoute(
                name: "AddUserInput",
                url: "ServiceRequestMaintenance/AddUserInput/{id}/{type}",
                defaults: new { controller = "ServiceRequestMaintenance", action = "AddUserInput" }
                );

            routes.MapRoute(
                name: "UpdateUserInput",
                url: "ServiceRequestMaintenance/UpdateUserInput/{id}/{type}",
                defaults: new { controller = "ServiceRequestMaintenance", action = "UpdateUserInput" }
                );


            routes.MapRoute(
                name: "ConfirmDeleteUserInput",
                url: "ServiceRequestMaintenance/ConfirmDeleteUserInput/{id}/{type}",
                defaults: new { controller = "ServiceRequestMaintenance", action = "ConfirmDeleteUserInput" }
                );

            routes.MapRoute(
                name: "ShowUserInput",
                url: "ServiceRequestMaintenance/ShowUserInput/{id}/{type}",
                defaults: new { controller = "ServiceRequestMaintenance", action = "ShowUserInput" }
                );

            #endregion

            #region User Management

            routes.MapRoute(
                name: "SystemAccessFilterByRole",
                url: "SystemAccess/FilterByRole/{id}/{pageId}",
                defaults: new { controller = "SystemAccess", action = "FilterByRole", pageId = UrlParameter.Optional }
                );
            #endregion

            #region Service Request System

            routes.MapRoute(name: "ServiceRequest",
                url: "ServiceRequest/Form/{id}/{index}/{serviceOptionId}",
                defaults: new {controller = "ServiceRequest", action = "Form", serviceOptionId = UrlParameter.Optional});

            #endregion

            #region Service Request Approvals
            routes.MapRoute(name: "ServiceRequestApprovals",
                url: "ServiceRequestApproval/Index/{filterBy}/{filterArg}/{pageId}",
                defaults: new {controller = "ServiceRequest", action="Index", filterBy= UrlParameter.Optional, filterArg = UrlParameter.Optional, pageId = UrlParameter.Optional});
            #endregion


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
