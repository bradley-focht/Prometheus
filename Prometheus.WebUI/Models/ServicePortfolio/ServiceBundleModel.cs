using DataService.Models;
using System;
using System.Collections.Generic;

namespace Prometheus.WebUI.Models.ServicePortfolio
{
	public class ServiceBundleModel
	{
        public ServiceBundleModel(ServiceBundle currentServiceBundle)
        {
            CurrentServiceBundle = currentServiceBundle;
        }
		public ServiceBundle CurrentServiceBundle { get; set; }
		public IEnumerable<KeyValuePair<int, string>> ServiceBundles { get; set; }
	}
}