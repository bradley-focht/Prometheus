﻿using Common.Dto;
using Prometheus.WebUI.Models.ServicePortfolio;
using ServicePortfolioService;
using ServicePortfolioService.Controllers;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Models.Shared;

namespace Prometheus.WebUI.Controllers
{
    public class ServicePortfolioController : Controller
    {
        //TODO: Brad change this to user ID
        //TODO: Sean make user Ids Guids
        private const int DummyUserId = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            /* create interface to service portfolio */
            //var sps = new PortfolioService(DummyUserId, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());

            //var portfolioBundles = sps.GetServiceBundles();
            IEnumerable<IServiceBundleDto> portfolioBundles = new List<IServiceBundleDto> {new ServiceBundleDto
            {
                Name = "Employee Productivity Services",
                Description = "Enable secure, anytime, anywhere, stable work capabilities and access to required information to meet personal computing requirements and increase customer satisfaction",
                BusinessValue = "This service will provide you with <ul><li>Increased employee productivity</li><li>Value created through enterprise procurement with standard offerings in order to reduce cost</li></ul>",
                Services = new List<IServiceDto> {new ServiceDto { Name = "Identity and Access Management"}, new ServiceDto { Name="Hardware Services"} },
                Measures = "Customer satisfaction surveys, Customer reports"
            } };

            return View(portfolioBundles);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceBundle"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(ServiceBundleDto serviceBundle)
        {
            if (!ModelState.IsValid)
            {
                TempData["MessageType"] = WebMessageType.Success;
                TempData["Message"] = $"{serviceBundle.Name} saved successfully";

                return RedirectToAction("Show");
            }

            var sps = new PortfolioService(DummyUserId, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());

            serviceBundle.Id = 0;
            sps.SaveServiceBundle(serviceBundle);

            TempData["MessageType"] = WebMessageType.Failure;
            TempData["Message"] = $"{serviceBundle.Name} saved successfully";

            return RedirectToAction("Show");
        }

        /// <summary>
        /// Returns the Service Portfolio Editor with a model with id = 0;
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            ServiceBundleModel model = new ServiceBundleModel(new ServiceBundleDto());

            return View(model);
        }


        /// <summary>
        /// Show the initial service portfolio editor and if an item is selected, otherwise 
        ///   currentSelection is null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Show(int id = 0)
        {
            ServiceBundleDto serviceBundle = new ServiceBundleDto();

            if (id > 0)
            {
                var sps = new PortfolioService(DummyUserId, new ServiceBundleController(),
                    new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
                serviceBundle = (ServiceBundleDto)sps.GetServiceBundle(id);
            }
            else
            {
                serviceBundle.Id = 0;
            }
            return View(serviceBundle);
        }

        public ActionResult Update(int id = 0)
        {

            var sps = new PortfolioService(DummyUserId, new ServiceBundleController(),
    new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
            ServiceBundleDto serviceBundle = (ServiceBundleDto)sps.GetServiceBundle(id);
            return View("Update", serviceBundle);
        }

        /// <summary>
        /// Last chance before deleting a record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ConfirmDelete(int id)
        {
            var sps = new PortfolioService(DummyUserId, new ServiceBundleController(),
    new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
            ServiceBundleDto serviceBundle = (ServiceBundleDto)sps.GetServiceBundle(id);

            return View(serviceBundle);
        }


        /// <summary>
        /// Delete a service bundle
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(DeleteModel item)
        {
            if (!ModelState.IsValid)
            {
                TempData["MessageType"] = WebMessageType.Failure;
                TempData["Message"] = $"Failed to delete {item.Name}";
                return View("Show");
            }

            var sps = new PortfolioService(DummyUserId, new ServiceBundleController(),
new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
            sps.DeleteServiceBundle(item.Id);
            TempData["MessageType"] = WebMessageType.Success;
            TempData["Message"] = $"Successfully deleted {item.Name}";
            return View("Show");
        }

        [ChildActionOnly]
        public ActionResult ShowServiceBundleList(int id = 0)
        {
            var ps = new PortfolioService(DummyUserId, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());

            LinkListModel serviceBundleModel = new LinkListModel
            {
                AddAction = "Add",
                SelectAction = "Show",
                Controller = "ServicePortfolio",
                Title = "Service Bundles",
                SelectedItemId = id,
                ListItems = ps.GetServiceBundleNames()
            };

            return PartialView("PartialViews/_LinkList", serviceBundleModel);
        }

    }
}