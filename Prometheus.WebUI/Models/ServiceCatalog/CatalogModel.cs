using System.Collections.Generic;
using Common.Dto;

namespace Prometheus.WebUI.Models.ServiceCatalog
{
	public class CatalogModel
	{
		public Helpers.Enums.ServiceCatalog Catalog { get; set; }
		public ICollection<ICatalogPublishable> CatalogItems { get; set; }
		public CatalogControlsModel Controls { get; set; }
	}



}