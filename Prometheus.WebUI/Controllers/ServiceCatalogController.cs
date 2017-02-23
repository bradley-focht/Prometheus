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
		private readonly int _dummId = 1;
		private readonly int _pageSize;
		private readonly ICatalogController _requestService;

		public ServiceCatalogController()
		{
			_requestService = InterfaceFactory.CreateCatalogController(_dummId);
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
		public ActionResult CatalogSearch(string searchString, ServiceCatalogs type)
		{
			searchString = searchString?.ToLower();                                          //compare everything in lowercase
			
			CatalogModel model = new CatalogModel { Catalog = type };
			model.Controls = new CatalogControlsModel { CatalogType = type };

			ServiceCatalogSearcher searcher = new ServiceCatalogSearcher();

			List<ICatalogPublishable> searchresults = searcher.Search(type, searchString, _dummId);
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
		public ActionResult CatalogSearch(string searchString, ServiceCatalogs type, int pageId)
		{
			searchString = searchString?.ToLower() ?? "";                                          //compare everything in lowercase

			var model = new CatalogModel
			{
				Catalog = type,
				Controls = new CatalogControlsModel { CatalogType = type, PageNumber = pageId }
			};

			var searcher = new ServiceCatalogSearcher();

			var searchresults = searcher.Search(type, searchString, _dummId);
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
					if (service.ServiceOptionCategories != null)
					{
						i.Options.AddRange((from o in service.ServiceOptionCategories select (ICatalogPublishable) o).ToList());
							//find the top 3 items
					}
					if (service.ServiceOptions != null)
					{
						i.Options.AddRange((from o in service.ServiceOptions select (ICatalogPublishable) o).ToList());
					}
					int take;
					try
					{
						take = int.Parse(ConfigHelper.GetScTopAmount());
					}
					catch(Exception) { take = 3; }

					i.Options = i.Options.OrderBy(o => o.Name).Take(take).ToList();

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
					i.Options.AddRange((from o in service.ServiceOptionCategories select (ICatalogPublishable)o).ToList());    //find the top 3 items
					i.Options.AddRange((from o in service.ServiceOptions select (ICatalogPublishable)o).ToList());
					i.Options = i.Options.OrderBy(o => o.Name).Take(3).ToList();

					model.CatalogItems.Add(i);
				}
			}

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
		public ActionResult Details(ServiceCatalogs catalog, CatalogableType type, int id)
		{
			var ps = InterfaceFactory.CreatePortfolioService(_dummId);
			int serviceId = 0;
			switch (type)
			{
				case CatalogableType.Option:
					serviceId = ps.GetServiceOptionCategory(ps.GetServiceOption(id).ServiceOptionCategoryId).ServiceId;
					break;
				case CatalogableType.Category:
					serviceId = ps.GetServiceOptionCategory(id).ServiceId;
					break;
				case CatalogableType.Service:
					serviceId = id;
					break;
			}
			var service = ps.GetService(serviceId);

			if (service != null)
			{
				OptionModel model = new OptionModel { Catalog = catalog };                  //pack a list of options and categories
				if (type == CatalogableType.Category)
					model.Option = service.ServiceOptionCategories.FirstOrDefault(o => o.Id == id);
				else if (type == CatalogableType.Option)
					model.Option = (ICatalogPublishable) service.ServiceOptions.FirstOrDefault(s => s.Id == id);

				model.ServiceId = service.Id;
				model.ServiceName = service.Name;
				//add data
				List<ICatalogPublishable> options = (from o in service.ServiceOptionCategories select (ICatalogPublishable)o).ToList(); //build list of options & categories
				if (service.ServiceOptions != null)
				{
					options.AddRange(from o in service.ServiceOptions select (ICatalogPublishable) o); //sort by name
				}
				options = options.OrderBy(o => o.Name).ToList();
				model.Options = options;

				//now create the controls model
				model.Controls = new CatalogControlsModel {CatalogType = catalog};

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
		public ActionResult ServiceOptions(ServiceCatalogs type, int serviceId)
		{
			var model = new ServiceOptionsModel { Catalog = type, ServiceId = serviceId };
			model.Controls = new CatalogControlsModel();

			IList<IServiceDto> services = null;

			if (type == ServiceCatalogs.Business || type == ServiceCatalogs.Business)               //create list of available services to view
				services = _requestService.RequestBusinessCatalog(_dummId).ToList();
			else if (type == ServiceCatalogs.Technical || type == ServiceCatalogs.Technical)
				services = _requestService.RequestSupportCatalog(_dummId).ToList();

			if (services != null)
			{
				var service = services.FirstOrDefault(s => s.Id == serviceId);
				model.ServiceName = service.Name;               //fill in the services list
				model.ServiceNames = from s in services
									 select new Tuple<int, string>(s.Id, s.Name);

				List<ICatalogPublishable> options = new List<ICatalogPublishable>();

				if (service.ServiceOptionCategories != null)
				{
					 options = (from o in service.ServiceOptionCategories select (ICatalogPublishable) o).ToList();
				}
			   
				model.Options = options.OrderBy(o => o.Name);
			}
			return View(model);
		}
	}
}