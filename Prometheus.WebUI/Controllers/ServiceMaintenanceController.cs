using System.Collections.Generic;
using System.Web.Mvc;
using Common.Dto;
using Prometheus.WebUI.Models.ServiceMaintenance;
using Prometheus.WebUI.Models.Shared;

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


	    public ActionResult ShowServices(int id = 0)
	    {
			ServiceModel serviceModel = new ServiceModel();
			LinkListModel model = new LinkListModel();
		    model.AddAction = null;
		    model.Controller = "ServiceMaintenance";
		    model.SelectAction = "ShowServices";
		    model.ListItems = null;
		    model.Title = "Services";

		    serviceModel.LinkListModel = model;
		    serviceModel.Service = null;
			

		    return View(serviceModel);
	    }

        /// <summary>
        /// Show details of selected lifecycle or none if no id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ShowLifecycle(int id=0)
        {
            LifecycleModel lm = new LifecycleModel
            {
                CurrentStatus = new LifecycleStatusDto {Id = id},
                Statuses = new List<KeyValuePair<int, string>>()
                {
                    new KeyValuePair<int, string>(10, "Operations")
                }
            };
         

            return View(lm);
        }

        public ActionResult AddLifecycle()
        {
            /*start test region*/
            LifecycleModel lm = new LifecycleModel();
            lm.CurrentStatus = new LifecycleStatusDto();
            lm.CurrentStatus.Id = 0;
            lm.Statuses = new List<KeyValuePair<int, string>>()
            {
                new KeyValuePair<int, string>(1, "Operations")
            };
            /*end test region */

            return View(lm);
        }


        [HttpPost]
        public ActionResult SaveLifecycle(LifecycleStatusDto model)
        {
            if (ModelState.IsValid)
            {
                //do some saving 
            }

            return RedirectToAction("ShowLifecycle");
        }

        public ActionResult UpdateLifecycle(int id=0)
        {                    
            /*start test region*/
            LifecycleModel lm = new LifecycleModel();
            lm.CurrentStatus = new LifecycleStatusDto();
            lm.CurrentStatus.Id = 0;
            lm.Statuses = new List<KeyValuePair<int, string>>()
            {
                new KeyValuePair<int, string>(1, "Operations")
            };
            /*end test region */
            return View(lm);
        }

        public ActionResult ConfirmDeleteLifecycle(int id=0)
        {
            /*start test region*/
            LifecycleModel lm = new LifecycleModel();
            lm.CurrentStatus = new LifecycleStatusDto();
            lm.CurrentStatus.Id = 1;
            lm.Statuses = new List<KeyValuePair<int, string>>()
            {
                new KeyValuePair<int, string>(1, "Operations")
            };
            /*end test region */
            return View(lm);
        }

     
        [HttpPost]
        public ActionResult DeleteLifecycle(int id=0)
        {
            //perform delete

            return RedirectToAction("ShowLifecycle");
        }

	    public ActionResult ConfirmDeleteService(int id = 0)
	    {
		    return View();

	    }
    }
}