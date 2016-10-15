using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DataService.Models;
using Prometheus.WebUI.Models.Service;

namespace Prometheus.WebUI.Controllers
{
    public class ServiceMaintenanceController : Controller
    {
        // GET: ServiceMaintenance
        public ActionResult Index()
        {
            return View();
        }


		public ActionResult ShowLifecycle(Guid? id)
		{
            /* test region */
            LifecycleModel lm = new LifecycleModel();
            lm.CurrentStatus = new LifecycleStatus() {Id = id, Name = "Test Select"};
            lm.Statuses = new List<KeyValuePair<Guid, string>>() { new KeyValuePair<Guid, string>(Guid.NewGuid(), "Operations") };
            /*end test region */

			return View(lm);
		}

        public ActionResult AddLifecycle()
        {
            /*start test region*/
            LifecycleModel lm = new LifecycleModel();
            lm.CurrentStatus = new LifecycleStatus();
            lm.CurrentStatus.Id = null;
            lm.Statuses = new List<KeyValuePair<Guid, string>>() { new KeyValuePair<Guid, string>(Guid.NewGuid(), "Operations") };
            /*end test region */

            return View(lm);
        }

        
        [HttpPost]
        public ActionResult SaveLifecycle(ILifecycleStatus lifecycleStatus)
        {
            return RedirectToAction("ShowLifecycle");
        }

        public ActionResult UpdateLifecycle(Guid? id)
        {
            return View();
        }
    }
}