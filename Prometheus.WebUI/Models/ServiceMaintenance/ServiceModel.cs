using DataService.Models;
using Prometheus.WebUI.Models.Shared;

namespace Prometheus.WebUI.Models.ServiceMaintenance
{
	public class ServiceModel
	{
		public LinkListModel LinkListModel { get; set; }
		public IService Service { get; set; }
	}
}