using System.Collections.Generic;
using Common.Dto;
using Common.Enums.Entities;

namespace Prometheus.WebUI.Models.ServiceCatalog
{
	public class OptionModel
	{
		public ServiceTypeRole Catalog { get; set; }
		public ICatalogable Option { get; set; }
		public IEnumerable<ICatalogable> Options { get; set; }
		public string ServiceName { get; set; }
		public int ServiceId { get; set; }
	}
}