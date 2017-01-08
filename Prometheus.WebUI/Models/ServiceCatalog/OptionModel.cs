using System.Collections.Generic;
using Common.Dto;
using Common.Enums.Entities;

namespace Prometheus.WebUI.Models.ServiceCatalog
{
	public class OptionModel
	{
		public ServiceTypeRole Catalog { get; set; }
		public ICatalogPublishable Option { get; set; }
		public IEnumerable<ICatalogPublishable> Options { get; set; }
		public string ServiceName { get; set; }
		public int ServiceId { get; set; }
	}
}