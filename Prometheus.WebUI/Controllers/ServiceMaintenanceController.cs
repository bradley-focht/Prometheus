using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DataService.Models;
using Prometheus.WebUI.Models;
using Prometheus.WebUI.Models.Service;

namespace Prometheus.WebUI.Controllers
{
    public class ServiceMaintenanceController : Controller
    {
        /// <summary>
        /// Returns main scree, this is the menu options
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Show details of selected lifecycle or none if no id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ShowLifecycle(int id=0)
        {
            /* test region */
            LifecycleModel lm = new LifecycleModel();
            lm.CurrentStatus = new LifecycleStatus() {Id = 1, Name = "Test Select"};
            lm.Statuses = new List<KeyValuePair<int, string>>()
            {
                new KeyValuePair<int, string>(id, "Operations")
            };
            /*end test region */

            return View(lm);
        }

        public ActionResult AddLifecycle()
        {
            /*start test region*/
            LifecycleModel lm = new LifecycleModel();
            lm.CurrentStatus = new LifecycleStatus();
            lm.CurrentStatus.Id = 0;
            lm.Statuses = new List<KeyValuePair<int, string>>()
            {
                new KeyValuePair<int, string>(1, "Operations")
            };
            /*end test region */

            return View(lm);
        }


        [HttpPost]
        public ActionResult SaveLifecycle(LifecycleStatus model)
        {
            if (ModelState.IsValid)
            {
                //do some saving 
            }

            return RedirectToAction("ShowLifecycle");
        }

        public ActionResult UpdateLifecycle(int id)
        {                    
            /*start test region*/
            LifecycleModel lm = new LifecycleModel();
            lm.CurrentStatus = new LifecycleStatus();
            lm.CurrentStatus.Id = 0;
            lm.Statuses = new List<KeyValuePair<int, string>>()
            {
                new KeyValuePair<int, string>(1, "Operations")
            };
            /*end test region */
            return View(lm);
        }

        public ActionResult ConfirmDeleteLifecycle(int id)
        {
            /*start test region*/
            LifecycleModel lm = new LifecycleModel();
            lm.CurrentStatus = new LifecycleStatus();
            lm.CurrentStatus.Id = 1;
            lm.Statuses = new List<KeyValuePair<int, string>>()
            {
                new KeyValuePair<int, string>(1, "Operations")
            };
            /*end test region */
            return View(lm);
        }

        [HttpPost]
        public ActionResult DeleteLifecylce(int id)
        {
            //perform delete

            return RedirectToAction("ShowLifecycle");
        }
    }
}