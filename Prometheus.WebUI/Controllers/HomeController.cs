using System.Web.Mvc;

namespace Prometheus.WebUI.Controllers
{
   [Authorize]
    public class HomeController : Controller
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