using Common.Enums.Entities;

namespace Prometheus.WebUI.Models.ServiceCatalog
{
	public class ServiceOptionsModel
	{
		public ServiceTypeRole Catalog { get; set; }

		public string ServiceName { get; set; }
		public int ServiceId { get; set; }
	}
}