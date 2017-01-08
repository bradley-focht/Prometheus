using System.Collections.Generic;
using Prometheus.WebUI.Helpers.Enums;

namespace Prometheus.WebUI.Models.ServiceCatalog
{
	public class CatalogModel
	{
		public ServiceCatalogs Catalog { get; set; }
		public ICollection<CatalogItem> CatalogItems { get; set; }
	}



}