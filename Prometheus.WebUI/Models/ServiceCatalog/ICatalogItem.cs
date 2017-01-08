using System.Collections.Generic;
using Common.Dto;

namespace Prometheus.WebUI.Models.ServiceCatalog
{
	public interface ICatalogItem
	{
		List<ICatalogPublishable> Options { get; set; }
		string ServiceDescription { get; set; }
		int ServiceId { get; set; }
		string ServiceName { get; set; }
	}
}