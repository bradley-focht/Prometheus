using DataService.Models;
using System;
using System.Collections.Generic;

namespace Prometheus.WebUI.Models.ServicePortfolio
{
	public class ServiceBundleModel
	{
        public ServiceBundleModel(IServiceBundle currentServiceBundle)
        {
            CurrentServiceBundle = currentServiceBundle;
        }
		public IServiceBundle CurrentServiceBundle { get; set; }
		public IEnumerable<KeyValuePair<int, string>> ServiceBundles { get; set; }
	}
}