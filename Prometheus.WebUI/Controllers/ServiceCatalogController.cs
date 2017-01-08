using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common.Dto;
using Common.Enums.Entities;
using Prometheus.WebUI.Helpers.Enums;
using Prometheus.WebUI.Models.ServiceCatalog;
using RequestService;

namespace Prometheus.WebUI.Controllers
{
	public class ServiceCatalogController : Controller
	{
		private int _dummId;
		
		[HttpPost]
		public ActionResult CatalogSearch(string searchString, ServiceCatalogs catalog, int pageId = 0)
		{
			GeneralCatalogModel model = new GeneralCatalogModel {Catalog = catalog};
			List<ICatalogPublishable> searchresults = new List<ICatalogPublishable>();       //start the container for catalogables
			ICatalogController rs = new CatalogController(_dummId);

			
			if (catalog == ServiceCatalogs.Both || catalog == ServiceCatalogs.Business)			//add things from the business catalog
			{
				var services = (from s in rs.BusinessCatalog select s).ToList();
				searchresults.AddRange(from s in services where s.Name.Contains(searchString) select (ICatalogPublishable)s);
				foreach (var service in services)
				{
					searchresults.AddRange(from o in service.ServiceOptions where o.Name.Contains(searchString) select (ICatalogPublishable)o);
					searchresults.AddRange(from c in service.OptionCategories where c.Name.Contains(searchString) select (ICatalogPublishable)c);
				}
			}

			if (catalog == ServiceCatalogs.Both || catalog == ServiceCatalogs.Technical)		//add things from the tech catalog	
			{
				var services = (from s in rs.SupportCatalog select s).ToList();
				searchresults.AddRange(from s in services where s.Name.Contains(searchString) select (ICatalogPublishable)s);
				foreach (var service in services)
				{
					searchresults.AddRange(from o in service.ServiceOptions where o.Name.Contains(searchString) select (ICatalogPublishable)o);
					searchresults.AddRange(from c in service.OptionCategories where c.Name.Contains(searchString) select (ICatalogPublishable)c);
				}
			}
			//instead of sorting by number of occurences of the search string, since only names are searched anyways, sort items by name
			model.Results = searchresults.OrderByDescending(a => a.Name);


			return View("ServiceCatalogGeneral", model);
		}


		public ActionResult Business()
		{
			ICatalogController rs = new CatalogController(_dummId);
			CatalogModel model = new CatalogModel { Catalog = ServiceCatalogs.Business, CatalogItems = new List<CatalogItem>() };
			var services = rs.BusinessCatalog;
			if (services != null)
			{
				foreach (var service in services)											//add services to the catalog model
				{
					var i = new CatalogItem
					{
						ServiceName = service.Name,
						ServiceId = service.Id,
						ServiceDescription = service.Description,
						Options = new List<ICatalogPublishable>()
					};
					i.Options.AddRange((from o in service.OptionCategories select (ICatalogPublishable)o).ToList());	//find the top 3 items
					i.Options.AddRange((from o in service.ServiceOptions
											where o.CategoryId == null
											select (ICatalogPublishable)o).ToList());
					i.Options = i.Options.OrderBy(o => o.Name).Take(3).ToList();

					model.CatalogItems.Add(i);
				}
			}

			return View("ServiceCatalog", model);
		}

		/// <summary>
		/// Technical Service Catalog
		/// </summary>
		/// <returns></returns>
		public ActionResult Technical()
		{
			ICatalogController rs = new CatalogController(_dummId);
			CatalogModel model = new CatalogModel { Catalog = ServiceCatalogs.Technical, CatalogItems = new List<CatalogItem>() };
			var services = rs.SupportCatalog;
			if (services != null)
			{
				foreach (var service in services)
				{
					var i = new CatalogItem
					{
						ServiceName = service.Name,
						ServiceId = service.Id,
						ServiceDescription = service.Description,
						Options = new List<ICatalogPublishable>()
					};
					i.Options.AddRange((from o in service.OptionCategories select (ICatalogPublishable)o).ToList());
					i.Options.AddRange((from o in service.ServiceOptions
										where o.CategoryId == null
										select (ICatalogPublishable)o).ToList());
					i.Options = i.Options.OrderBy(o => o.Name).Take(3).ToList();

					model.CatalogItems.Add(i);
				}
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
			ICatalogController rs = new CatalogController(_dummId);
			var service = rs.BusinessCatalog.FirstOrDefault(s => s.Id == serviceId);

			if (service != null)
			{
				OptionModel model = new OptionModel {Catalog = service.ServiceTypeRole};					//pack a list of options and categories
				if (type == CatalogableTypes.Category)
					model.Option = (ICatalogPublishable) service.OptionCategories.FirstOrDefault(o => o.Id == id);
				else if (type == CatalogableTypes.Option)
					model.Option = (ICatalogPublishable) service.ServiceOptions.FirstOrDefault(s => s.Id == id);

				model.ServiceId = service.Id;
				model.ServiceName = service.Name;

				List<ICatalogPublishable> options = (from o in service.OptionCategories select (ICatalogPublishable)o).ToList(); //build list of options & categories
				options.AddRange(from o in service.ServiceOptions where o.CategoryId == null select (ICatalogPublishable)o);	//sort by name
				options = options.OrderBy(o => o.Name).ToList();
				model.Options = options;

				return View(model);
			}


			return View();//not sure what to do here yet
		}

		/// <summary>
		/// Returns View of all options for a Service
		/// </summary>
		/// <param name="type"></param>
		/// <param name="serviceId"></param>
		/// <returns></returns>
		public ActionResult ServiceOptions(ServiceTypeRole type, int serviceId)
		{
			var model = new ServiceOptionsModel {Catalog = type, ServiceId = serviceId};
			ICatalogController rs = new CatalogController(_dummId);
			IList<IServiceDto> services = null;

			if (type == ServiceTypeRole.Business)				//create list of available services to view
				services = rs.BusinessCatalog.ToList();
			else if (type == ServiceTypeRole.Supporting)
				services = rs.SupportCatalog.ToList();

			if (services != null)
			{
				var service = services.FirstOrDefault(s => s.Id == serviceId);
				model.ServiceName = service.Name;				//fill in the services list
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