using Common.Dto;
using System.Collections.Generic;

namespace Prometheus.WebUI.Models.ServicePortfolio
{
	public class ServiceBundleModel
	{
        public ServiceBundleModel(ServiceBundleDto currentServiceBundle)
        {
            CurrentServiceBundle = currentServiceBundle;
        }
		public ServiceBundleDto CurrentServiceBundle { get; set; }
		public IEnumerable<KeyValuePair<int, string>> ServiceBundles { get; set; }
	}
}