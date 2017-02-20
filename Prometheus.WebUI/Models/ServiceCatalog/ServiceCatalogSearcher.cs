
using System.Collections.Generic;
using System.Linq;
using Common.Dto;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Helpers.Enums;
using RequestService.Controllers;

namespace Prometheus.WebUI.Models.ServiceCatalog
{
	/// <summary>
	/// Code that is reused in the ServiceCatalog
	/// </summary>
	public class ServiceCatalogSearcher
	{
		public List<ICatalogPublishable> Search(ServiceCatalogs catalog, string searchString, int userId)
		{
			List<ICatalogPublishable> searchresults = new List<ICatalogPublishable>();      //start the container for catalogables
			ICatalogController rs = InterfaceFactory.CreateCatalogController(userId);

			if (catalog == ServiceCatalogs.Both || catalog == ServiceCatalogs.Business)     //add things from the business catalog
			{
				var services = (from s in rs.RequestBusinessCatalog(userId) select s).ToList();
				searchresults.AddRange(from s in services where searchString != null && s.Name.ToLower().Contains(searchString) select (ICatalogPublishable)s);
				foreach (var service in services)
				{
					searchresults.AddRange(from o in service.ServiceOptions where searchString != null && o.Name.ToLower().Contains(searchString) select (ICatalogPublishable)o);
					searchresults.AddRange(from c in service.ServiceOptionCategories where searchString != null && c.Name.ToLower().Contains(searchString) select (ICatalogPublishable)c);
				}
			}

			if (catalog == ServiceCatalogs.Both || catalog == ServiceCatalogs.Technical)        //add things from the tech catalog	
			{
				var services = (from s in rs.RequestSupportCatalog(userId) select s).ToList();
				searchresults.AddRange(from s in services where searchString != null && s.Name.ToLower().Contains(searchString) select (ICatalogPublishable)s);
				foreach (var service in services)
				{
					searchresults.AddRange(from o in service.ServiceOptions where searchString != null && o.Name.ToLower().ToLower().Contains(searchString) select (ICatalogPublishable)o);
					searchresults.AddRange(from c in service.ServiceOptionCategories where searchString != null && c.Name.ToLower().ToLower().Contains(searchString) select (ICatalogPublishable)c);
				}
			}
			//since search is performed on names only, there isn't much point weighting results. 
			//sorting first by name only
			searchresults = searchresults.OrderByDescending(a => a.Popularity).ToList();
			return searchresults;
		}

	}
}