using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Common.Dto;
using Common.Enums.Entities;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Infrastructure;
using Prometheus.WebUI.Models.ServicePortfolio;
using Prometheus.WebUI.Models.Shared;
using ServicePortfolioService;

namespace Prometheus.WebUI.Controllers
{
	[Authorize]
	public class ServicePortfolioController : PrometheusController
	{
		private readonly IPortfolioService _portfolioService;

		public ServicePortfolioController(IPortfolioService portfolioService)
		{
			_portfolioService = portfolioService;
		}
		/// <summary>
		/// Main page to display all Service Bundles
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
		{
			var bundlesList = new List<ServiceBundleModel>();

			try
			{
				foreach (var bundle in _portfolioService.GetServiceBundles())
				{
					ServiceBundleModel bundleModel = new ServiceBundleModel {ServiceBundle = bundle};
					bundleModel.ServiceNames = _portfolioService.GetServiceNamesForServiceBundle(bundleModel.ServiceBundle.Id);
					bundlesList.Add(bundleModel);
				}
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to retrieve service bundles, {exception.Message}";
			}

			return View(bundlesList);
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
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to save service bundle due to invalid data";
				return RedirectToAction("Update", serviceBundle.Id);
			}

			if (serviceBundle.Id == 0)
				_portfolioService.ModifyServiceBundle(UserId, serviceBundle, EntityModification.Create);
			else
				_portfolioService.ModifyServiceBundle(UserId, serviceBundle, EntityModification.Update);

			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = $"{serviceBundle.Name} saved successfully";

			return RedirectToAction("Show", new { id = serviceBundle.Id });
		}

		/// <summary>
		/// Returns the Service Portfolio Editor with a model with id = 0;
		/// </summary>
		/// <returns></returns>
		public ActionResult Add()
		{
			return View(new ServiceBundleModel());
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
				try
				{
					serviceBundle = (ServiceBundleDto) _portfolioService.GetServiceBundle(id);
				}
				catch (Exception exception)
				{
					TempData["MessageType"] = WebMessageType.Failure;
					TempData["Message"] = $"failed to get service bundle, {exception}";
				}
			}
			else
			{
				serviceBundle.Id = 0;
			}
			return View(serviceBundle);
		}

		/// <summary>
		/// Update an existing Service Bundle
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult Update(int id = 0)
		{
			ServiceBundleDto serviceBundle;
			try
			{
				serviceBundle = (ServiceBundleDto) _portfolioService.GetServiceBundle(id);
			}
			catch (Exception exception)
			{
				serviceBundle = new ServiceBundleDto();
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed retrieve Service Bundle {exception.Message}";
			}
			return View(serviceBundle);
		}

		/// <summary>
		/// Last chance before deleting a record
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult ConfirmDelete(int id)
		{
			ServiceBundleDto serviceBundle = (ServiceBundleDto)_portfolioService.GetServiceBundle(id);

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
				return RedirectToAction("Show");
			}

			_portfolioService.ModifyServiceBundle(UserId, new ServiceBundleDto() { Id = item.Id }, EntityModification.Delete);
			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = $"Successfully deleted {item.Name}";
			return RedirectToAction("Show");
		}

		/// <summary>
		/// Retrieve a link-list of service bundles
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[ChildActionOnly]
		public ActionResult ShowServiceBundleList(int id = 0)
		{
			LinkListModel serviceBundleModel = new LinkListModel
			{
				AddAction = "Add",
				SelectAction = "Show",
				Controller = "ServicePortfolio",
				Title = "Service Bundles",
				SelectedItemId = id,
				ListItems = _portfolioService.GetServiceBundleNames()
			};

			return PartialView("PartialViews/_LinkList", serviceBundleModel);
		}
	}
}