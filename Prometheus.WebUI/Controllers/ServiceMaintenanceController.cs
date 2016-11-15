using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Common.Dto;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Models.ServiceMaintenance;
using Prometheus.WebUI.Models.Shared;
using ServicePortfolioService;
using ServicePortfolioService.Controllers;

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
            IPortfolioService sps = new PortfolioService(0, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
            

		    return View((ServiceDto)sps.GetService(id));
	    }
        /// <summary>
        /// Show details of selected lifecycle or none if no id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ShowLifecycle(int id=0)
        {
            IPortfolioService sps = new PortfolioService(0, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
         
            return View((LifecycleStatusDto)sps.GetLifecycleStatus(id));
        }

        public ActionResult ShowLifeCycleList(int id = 0)
        {
            IPortfolioService sps = new PortfolioService(0, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
            
            LinkListModel servicesModel = new LinkListModel();
            servicesModel.SelectedItemId = id;

            servicesModel.ListItems = sps.GetLifecycleStatusNames();

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
                IPortfolioService sps = new PortfolioService(0, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());

                sps.SaveLifecycleStatus(model);

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
            IPortfolioService sps = new PortfolioService(0, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
            return View((LifecycleStatusDto)sps.GetLifecycleStatus(id));
        }

        public ActionResult ConfirmDeleteLifecycle(int id=0)
        {
            IPortfolioService sps = new PortfolioService(0, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
            return View((LifecycleStatusDto)sps.GetLifecycleStatus(id));
        }

    
        [HttpPost]
        public ActionResult DeleteLifecycle(int id=0)
        {
            IPortfolioService sps = new PortfolioService(0, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
            
            TempData["messageType"] = WebMessageType.Success;
            TempData["message"] = "successfully deleted lifecycle status";

            return RedirectToAction("ShowLifecycle");
        }

	    public ActionResult ConfirmDeleteService(int id = 0)
	    {
            IPortfolioService sps = new PortfolioService(0, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
            
            LifecycleStatusDto lf = new LifecycleStatusDto();
	        lf.Name = "Operational";
	        lf.Id = 5;
		    return View(lf);

	    }

        [HttpPost]
        public ActionResult DeleteService(int id)
        {
            IPortfolioService sps = new PortfolioService(0, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
            sps.DeleteService(id);

            TempData["messageType"] = WebMessageType.Success;
            TempData["message"] = "Successfully deleted Service";

            return RedirectToAction("ShowServices");
        }
    }
}