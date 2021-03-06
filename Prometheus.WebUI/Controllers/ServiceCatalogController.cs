﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common.Dto;
using Common.Enums.Entities;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Helpers.Enums;
using Prometheus.WebUI.Infrastructure;
using Prometheus.WebUI.Models.ServiceCatalog;
using RequestService.Controllers;
using ServicePortfolioService;

namespace Prometheus.WebUI.Controllers
{
	[Authorize]
	public class ServiceCatalogController : PrometheusController
	{
		private readonly int _pageSize;
		private readonly ICatalogController _catalogController;
		private readonly IPortfolioService _portfolioService;

		public ServiceCatalogController(ICatalogController catalogController, IPortfolioService portfolioService)
		{
			_catalogController = catalogController;
			_portfolioService = portfolioService;
			try { _pageSize = ConfigHelper.GetPaginationSize(); }       //set pagination size
			catch (Exception) { _pageSize = 12;     /*just in case */  }
		}

		/// <summary>
		/// Search feature
		/// </summary>
		/// <param name="searchString">user entered search argument</param>
		/// <param name="type"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult CatalogSearch(string searchString, ServiceCatalog type)
		{
			searchString = searchString?.ToLower();                                          //compare everything in lowercase

			CatalogModel model = new CatalogModel { Catalog = type };
			model.Controls = new CatalogControlsModel { CatalogType = type };

			ServiceCatalogSearcher searcher = new ServiceCatalogSearcher(_catalogController);

			List<ICatalogPublishable> searchresults = searcher.Search(type, searchString, UserId);
			//pagination
			if (searchresults.Count > _pageSize)
			{
				model.Controls.TotalPages = (searchresults.Count + _pageSize - 1) / _pageSize;
				searchresults = (searchresults.Skip(0).Take(_pageSize)).ToList();
			}

			model.CatalogItems = searchresults;
			return View("ServiceCatalogGeneral", model);
		}

		/// <summary>
		/// Pagination from search results go here
		/// </summary>
		/// <param name="searchString"></param>
		/// <param name="type"></param>
		/// <param name="pageId"></param>
		/// <returns></returns>
		[HttpGet]
		public ActionResult CatalogSearch(string searchString, ServiceCatalog type, int pageId)
		{
			searchString = searchString?.ToLower() ?? "";                                          //compare everything in lowercase

			var model = new CatalogModel
			{
				Catalog = type,
				Controls = new CatalogControlsModel { CatalogType = type, PageNumber = pageId }
			};

			var searcher = new ServiceCatalogSearcher(_catalogController);

			var searchresults = searcher.Search(type, searchString, UserId);
			//pagination
			if (searchresults.Count > _pageSize)
			{
				model.Controls.TotalPages = (searchresults.Count + _pageSize - 1) / _pageSize;
				searchresults = (searchresults.Skip(_pageSize * pageId).Take(_pageSize)).ToList();
			}

			model.CatalogItems = searchresults;
			return View("ServiceCatalogGeneral", model);
		}

		/// <summary>
		/// Service Catalog Index, either business, technical, or both
		/// </summary>
		/// <param name="type"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult Index(ServiceCatalog type = ServiceCatalog.Business, int id = 0)
		{
			CatalogModel model = new CatalogModel { Catalog = type, CatalogItems = new List<ICatalogPublishable>() };
			model.Controls = new CatalogControlsModel { SearchString = "", CatalogType = type };         //setup info for the controls

			IEnumerable<IServiceDto> services;                                                           //lazy loaded data for filtering and use later

			if (type == ServiceCatalog.Both || type == ServiceCatalog.Business)                       //add things from the business catalog
			{
				services = (from s in _catalogController.RequestBusinessCatalog(UserId) select s).ToList();
				foreach (var service in services)                                                       //add services to the catalog model
				{
					var i = new ServiceSummary
					{
						Name = service.Name,
						Id = service.Id,
						BusinessValue = service.Description,
						Options = new List<ICatalogPublishable>()
					};
					if (service.ServiceOptionCategories != null)
					{
						i.Options.AddRange((from o in service.ServiceOptionCategories select (ICatalogPublishable)o).ToList());
						//find the top 3 items
					}
					if (service.ServiceOptions != null)
					{
						//i.Options.AddRange((from o in service.ServiceOptions select (ICatalogPublishable) o).ToList()); removed upon request
					}
					int take;
					try
					{
						take = ConfigHelper.GetScTopAmount();
					}
					catch (Exception) { take = 3; }

					i.Options = i.Options.OrderByDescending(o => o.Popularity).Take(take).ToList();

					model.CatalogItems.Add(i);
				}

			}

			if (type == ServiceCatalog.Both || type == ServiceCatalog.Technical)                  //add things from the tech catalog	
			{
				services = (from s in _catalogController.RequestSupportCatalog(UserId) select s).ToList();
				foreach (var service in services)                                                   //add services to the catalog model
				{
					var i = new ServiceSummary
					{
						Name = service.Name,
						Id = service.Id,
						BusinessValue = service.Description,
						Options = new List<ICatalogPublishable>()
					};
					i.Options.AddRange((from o in service.ServiceOptionCategories select (ICatalogPublishable)o).ToList());    //find the top 3 items
					//i.Options.AddRange((from o in service.ServiceOptions select (ICatalogPublishable)o).ToList());
					i.Options = i.Options.OrderByDescending(o => o.Popularity).Take(ConfigHelper.GetScTopAmount()).ToList();

					model.CatalogItems.Add(i);
				}
			}
			model.CatalogItems = model.CatalogItems.OrderByDescending(x => x.Popularity).ToList();

			if (model.CatalogItems != null && model.CatalogItems.Count > _pageSize)
			{
				model.Controls.TotalPages = (model.CatalogItems.Count + _pageSize - 1) / _pageSize;
				model.CatalogItems = (List<ICatalogPublishable>)model.CatalogItems.Skip(_pageSize * id).Take(_pageSize);
			}

			return View("ServiceCatalog", model);
		}

		/// <summary>
		/// Return View of an option
		/// </summary>
		/// <param name="catalog"></param>
		/// <param name="type"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult Details(ServiceCatalog catalog, CatalogableType type, int id)
		{
			int serviceId = 0;
			switch (type)
			{
				case CatalogableType.Option:
					serviceId = _portfolioService.GetServiceOptionCategory(UserId, _portfolioService.GetServiceOption(UserId, id).ServiceOptionCategoryId).ServiceId;
					break;
				case CatalogableType.Category:
					serviceId = _portfolioService.GetServiceOptionCategory(UserId, id).ServiceId;
					break;
				case CatalogableType.Service:
					serviceId = id;
					break;
			}
			var service = _portfolioService.GetService(serviceId);

			if (service != null)
			{
				OptionModel model = new OptionModel { Catalog = catalog };                  //pack a list of options and categories
				if (type == CatalogableType.Category)
					model.Option = service.ServiceOptionCategories.FirstOrDefault(o => o.Id == id);
				else if (type == CatalogableType.Option)
					model.Option = (ICatalogPublishable)service.ServiceOptions.FirstOrDefault(s => s.Id == id);

				model.ServiceId = service.Id;
				model.ServiceName = service.Name;
				//add data
				List<ICatalogPublishable> options = (from o in service.ServiceOptionCategories select (ICatalogPublishable)o).ToList(); //build list of options & categories
				if (service.ServiceOptions != null)
				{
					//options.AddRange(from o in service.ServiceOptions select (ICatalogPublishable)o); //sort by name
				}
				options = options.OrderBy(o => o.Name).ToList();
				model.Options = options;

				//now create the controls model
				model.Controls = new CatalogControlsModel { CatalogType = catalog };

				return View(model);
			}


			return View();          //not sure how you got here...
		}

		/// <summary>
		/// Returns View of all options for a Service
		/// </summary>
		/// <param name="type"></param>
		/// <param name="serviceId"></param>
		/// <returns></returns>
		public ActionResult ServiceOptions(ServiceCatalog type, int serviceId)
		{
			var model = new ServiceOptionsModel { Catalog = type, ServiceId = serviceId };
			model.Controls = new CatalogControlsModel();

			IList<IServiceDto> services = null;

			if (type == ServiceCatalog.Business || type == ServiceCatalog.Business)               //create list of available services to view
				services = _catalogController.RequestBusinessCatalog(UserId).ToList();
			else if (type == ServiceCatalog.Technical || type == ServiceCatalog.Technical)
				services = _catalogController.RequestSupportCatalog(UserId).ToList();

			if (services != null)
			{
				var service = services.FirstOrDefault(s => s.Id == serviceId);
				model.ServiceName = service.Name;               //fill in the services list
				model.ServiceBusinessValue = service.BusinessValue;
				model.ServiceNames = from s in services
									 select new Tuple<int, string>(s.Id, s.Name);

				List<ICatalogPublishable> options = new List<ICatalogPublishable>();

				if (service.ServiceOptionCategories != null)
				{
					options = (from o in service.ServiceOptionCategories select (ICatalogPublishable)o).ToList();
				}

				model.Options = options.OrderByDescending(o => o.Popularity);
			}
			return View(model);
		}

	}
}