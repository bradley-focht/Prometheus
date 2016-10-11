using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prometheus.WebUI.Controllers
{
    public class ServiceMaintenanceController : Controller
    {
        // GET: ServiceMaintenance
        public ActionResult Index()
        {
            return View();
        }


		public ActionResult ShowLifecycle()
		{
			return View();
		}

    }
}