using System.Collections.Generic;
using Common.Dto;
using Prometheus.WebUI.Helpers.Enums;

namespace Prometheus.WebUI.Models.ServiceCatalog
{
	public class CatalogModel
	{
		public ServiceCatalogs Catalog { get; set; }
		public ICollection<ICatalogPublishable> CatalogItems { get; set; }
		public CatalogControlsModel Controls { get; set; }
	}



}