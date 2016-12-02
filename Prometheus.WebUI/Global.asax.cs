
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using DataService.DataAccessLayer;
using DataService.Models;

namespace Prometheus.WebUI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Database.SetInitializer<PrometheusContext>(new DropCreateDatabaseIfModelChanges<PrometheusContext>());
        }
    }
}
