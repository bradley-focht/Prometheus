using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Dto;
using Common.Enums.Entities;
using Prometheus.WebUI.Models.ServiceCatalog;
using RequestService;

namespace Prometheus.WebUI.Controllers
{
	public class ServiceCatalogController : Controller
	{
		private int _dummId;
		// GET: Catalog
		public ActionResult Business()
		{
			ICatalogController rs = new CatalogController(_dummId);
			CatalogModel model = new CatalogModel { Catalog = ServiceTypeRole.Business, CatalogItems = new List<CatalogItem>() };
			var services = rs.BusinessCatalog;
			if (services != null)
			{
				foreach (var service in services)
				{
					var i = new CatalogItem
					{
						ServiceName = service.Name,
						ServiceId = service.Id,
						ServiceDescription = service.Description,
						Options = new List<ICatalogable>()
					};
					i.Options.AddRange((from o in service.OptionCategories select (ICatalogable)o).ToList());
					i.Options.AddRange((from o in service.ServiceOptions
											where o.CategoryId == null
											select (ICatalogable)o).ToList());
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
			CatalogModel model = new CatalogModel { Catalog = ServiceTypeRole.Supporting };

			return View("ServiceCatalog", model);
		}

		public ActionResult Details(int serviceId, string type, int id)
		{
			ICatalogController rs = new CatalogController(_dummId);
			var service = rs.BusinessCatalog.FirstOrDefault(s => s.Id == serviceId);

			if (service != null)
			{
				OptionModel model = new OptionModel {Catalog = service.ServiceTypeRole};
				if (type == "cat")
					model.Option = (ICatalogable) service.OptionCategories.FirstOrDefault(o => o.Id == id);
				else if (type == "opt")
					model.Option = (ICatalogable) service.ServiceOptions.FirstOrDefault(s => s.Id == id);

				return View(model);
			}


			return View();//not sure what to do here yet
		}


		public ActionResult ServiceOptions(ServiceTypeRole catalog, int id)
		{
			var model = new ServiceOptionsModel {Catalog = catalog};
			return View(model);
		}
	}
}