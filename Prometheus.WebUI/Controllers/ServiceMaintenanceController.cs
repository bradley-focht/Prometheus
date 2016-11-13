using System.Collections.Generic;
using System.Web.Mvc;
using Common.Dto;
using Prometheus.WebUI.Helpers;
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


        /// <summary>
        /// Basic list of services
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
                    new KeyValuePair<int, string>(10, "Operational")
                }
            };
         

            return View(lm);
        }

        public ActionResult ShowLifeCycleList(int id = 0)
        {
           
            LinkListModel servicesModel = new LinkListModel();
            servicesModel.SelectedItemId = id;
            /* I am here for the looks, remove me */
            servicesModel.ListItems = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(10, "Chartered"),
                new KeyValuePair<int, string>(11, "Operational")
            };
            /* end test data injection */
            servicesModel.AddAction = "Add";
            servicesModel.SelectAction = "ShowLifecyle";
            servicesModel.Controller = "ServiceMaintenance";
            servicesModel.Title = "Lifecycle Statuses";

            return PartialView("PartialViews/_LinkList", servicesModel);
        }

        public ActionResult AddLifecycle()
        {
            return View();
        }


        /// <summary>
        /// Save or update Lifecycle Statuses
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveLifecycle(LifecycleStatusDto model)
        {
            if (ModelState.IsValid)
            {
                TempData["messageType"] = WebMessageType.Success;
                TempData["message"] = "successfully saved lifecycle status";
                return RedirectToAction("ShowLifecycle");
            }
            TempData["messageType"] = WebMessageType.Failure;
                TempData["message"] = "failed to save lifecycle status";
                return RedirectToAction("AddLifecycle");
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
            LifecycleStatusDto model = new LifecycleStatusDto {Id = 10, Name = "Operational"};
            /*end test region */
            return View(model);
        }

    
        [HttpPost]
        public ActionResult DeleteLifecycle(int id=0)
        {
            TempData["messageType"] = WebMessageType.Success;
            TempData["message"] = "successfully deleted lifecycle status";

            return RedirectToAction("ShowLifecycle");
        }

	    public ActionResult ConfirmDeleteService(int id = 0)
	    {
            LifecycleStatusDto lf = new LifecycleStatusDto();
	        lf.Name = "Operational";
	        lf.Id = 5;
		    return View(lf);

	    }

        [HttpPost]
        public ActionResult DeleteService(int id)
        {
            return RedirectToAction("ShowServices");
        }
    }
}