using System.Web.Mvc;
using Prometheus.WebUI.Infrastructure;

namespace Prometheus.WebUI.Controllers
{
   [Authorize]
    public class HomeController : PrometheusController
	{
        /// <summary>
        /// Default home page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}