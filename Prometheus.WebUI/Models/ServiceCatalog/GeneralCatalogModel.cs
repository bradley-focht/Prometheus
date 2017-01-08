using System.Collections.Generic;
using Common.Dto;
using Common.Enums.Entities;
using Prometheus.WebUI.Helpers.Enums;

namespace Prometheus.WebUI.Models.ServiceCatalog
{
	public class GeneralCatalogModel
	{
		public int PageId { get; set; }
		public int TotalPages { get; set; }
		public ServiceCatalogs Catalog { get; set; }
		public IEnumerable<ICatalogPublishable> Results { get; set; }
	}
}