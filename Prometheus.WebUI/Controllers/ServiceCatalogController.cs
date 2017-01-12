using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common.Dto;
using Common.Enums.Entities;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Helpers.Enums;
using Prometheus.WebUI.Models.ServiceCatalog;
using RequestService.Controllers;

namespace Prometheus.WebUI.Controllers
{
	public class ServiceCatalogController : Controller
	{
		private int _dummId = 1;
		private const int CatalogPageSize = 12;
		private readonly ICatalogController _requestService;

		public ServiceCatalogController()
		{
			_requestService = InterfaceFactory.CreateCatalogController(_dummId);
		}

		/// <summary>
		/// Search feature
		/// </summary>
		/// <param name="searchString">user entered search argument</param>
		/// <param name="type"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult CatalogSearch(string searchString, ServiceCatalogs type)
		{
			searchString = searchString?.ToLower();                                          //compare everything in lowercase

			CatalogModel model = new CatalogModel { Catalog = type };
			model.Controls = new CatalogControlsModel { CatalogType = type };

			ServiceCatalogSearcher searcher = new ServiceCatalogSearcher();

			List<ICatalogPublishable> searchresults = searcher.Search(type, searchString, _dummId);
			//pagination
			if (searchresults.Count > CatalogPageSize)
			{
				model.Controls.TotalPages = (searchresults.Count + CatalogPageSize - 1) / CatalogPageSize;
				searchresults = (searchresults.Skip(0).Take(CatalogPageSize)).ToList();
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
		public ActionResult CatalogSearch(string searchString, ServiceCatalogs type, int pageId)
		{
			searchString = searchString == null ? "" : searchString.ToLower();                                          //compare everything in lowercase

			var model = new CatalogModel
			{
				Catalog = type,
				Controls = new CatalogControlsModel { CatalogType = type, PageNumber = pageId }
			};

			var searcher = new ServiceCatalogSearcher();

			var searchresults = searcher.Search(type, searchString, _dummId);
			//pagination
			if (searchresults.Count > CatalogPageSize)
			{
				model.Controls.TotalPages = (searchresults.Count + CatalogPageSize - 1) / CatalogPageSize;
				searchresults = (searchresults.Skip(CatalogPageSize * pageId).Take(CatalogPageSize)).ToList();
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
		public ActionResult Index(ServiceCatalogs type = ServiceCatalogs.Business, int id = 0)
		{
			CatalogModel model = new CatalogModel { Catalog = type, CatalogItems = new List<ICatalogPublishable>() };
			model.Controls = new CatalogControlsModel { SearchString = "", CatalogType = type };         //setup info for the controls

			IEnumerable<IServiceDto> services;                                                           //lazy loaded data for filtering and use later

			if (type == ServiceCatalogs.Both || type == ServiceCatalogs.Business)                       //add things from the business catalog
			{
				services = (from s in _requestService.RequestBusinessCatalog(_dummId) select s).ToList();
				foreach (var service in services)                                                       //add services to the catalog model
				{
					var i = new ServiceSummary
					{
						Name = service.Name,
						Id = service.Id,
						BusinessValue = service.Description,
						Options = new List<ICatalogPublishable>()
					};
					i.Options.AddRange((from o in service.OptionCategories select (ICatalogPublishable)o).ToList());    //find the top 3 items
					i.Options.AddRange((from o in service.ServiceOptions
										where o.CategoryId == null
										select (ICatalogPublishable)o).ToList());
					i.Options = i.Options.OrderBy(o => o.Name).Take(3).ToList();

					model.CatalogItems.Add(i);
				}

			}

			if (type == ServiceCatalogs.Both || type == ServiceCatalogs.Technical)                  //add things from the tech catalog	
			{
				services = (from s in _requestService.RequestSupportCatalog(_dummId) select s).ToList();
				foreach (var service in services)                                                   //add services to the catalog model
				{
					var i = new ServiceSummary
					{
						Name = service.Name,
						Id = service.Id,
						BusinessValue = service.Description,
						Options = new List<ICatalogPublishable>()
					};
					i.Options.AddRange((from o in service.OptionCategories select (ICatalogPublishable)o).ToList());    //find the top 3 items
					i.Options.AddRange((from o in service.ServiceOptions
										where o.CategoryId == null
										select (ICatalogPublishable)o).ToList());
					i.Options = i.Options.OrderBy(o => o.Name).Take(3).ToList();

					model.CatalogItems.Add(i);
				}
			}

			if (model.CatalogItems != null && model.CatalogItems.Count > CatalogPageSize)
			{
				model.Controls.TotalPages = (model.CatalogItems.Count + CatalogPageSize - 1) / CatalogPageSize;
				model.CatalogItems = (List<ICatalogPublishable>)model.CatalogItems.Skip(CatalogPageSize * id).Take(CatalogPageSize);
			}

			return View("ServiceCatalog", model);
		}

		/// <summary>
		/// Return View of an option
		/// </summary>
		/// <param name="serviceId"></param>
		/// <param name="type"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult Details(int serviceId, CatalogableTypes type, int id)
		{
			var ps = InterfaceFactory.CreatePortfolioService(_dummId);
			var service = ps.GetService(serviceId);

			if (service != null)
			{
				OptionModel model = new OptionModel { Catalog = service.ServiceTypeRole };                  //pack a list of options and categories
				if (type == CatalogableTypes.Category)
					model.Option = service.OptionCategories.FirstOrDefault(o => o.Id == id);
				else if (type == CatalogableTypes.Option)
					model.Option = service.ServiceOptions.FirstOrDefault(s => s.Id == id);

				model.ServiceId = service.Id;
				model.ServiceName = service.Name;

				List<ICatalogPublishable> options = (from o in service.OptionCategories select (ICatalogPublishable)o).ToList(); //build list of options & categories
				options.AddRange(from o in service.ServiceOptions where o.CategoryId == null select (ICatalogPublishable)o);    //sort by name
				options = options.OrderBy(o => o.Name).ToList();
				model.Options = options;

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
		public ActionResult ServiceOptions(ServiceTypeRole type, int serviceId)
		{
			var model = new ServiceOptionsModel { Catalog = type, ServiceId = serviceId };

			IList<IServiceDto> services = null;

			if (type == ServiceTypeRole.Business)               //create list of available services to view
				services = _requestService.RequestBusinessCatalog(_dummId).ToList();
			else if (type == ServiceTypeRole.Supporting)
				services = _requestService.RequestSupportCatalog(_dummId).ToList();

			if (services != null)
			{
				var service = services.FirstOrDefault(s => s.Id == serviceId);
				model.ServiceName = service.Name;               //fill in the services list
				model.ServiceNames = from s in services
									 select new Tuple<int, string>(s.Id, s.Name);

				var options = ((from o in service.OptionCategories select (ICatalogPublishable)o).ToList());
				options.AddRange((from o in service.ServiceOptions
								  where o.CategoryId == null
								  select (ICatalogPublishable)o).ToList());
				model.Options = options.OrderBy(o => o.Name);
			}
			return View(model);
		}
	}
}