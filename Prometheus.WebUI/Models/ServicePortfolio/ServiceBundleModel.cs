using System;
using System.Collections.Generic;
using Common.Dto;

namespace Prometheus.WebUI.Models.ServicePortfolio
{
	public class ServiceBundleModel
	{
		public ServiceBundleModel(ServiceBundleDto currentServiceBundle)
		{
			CurrentServiceBundle = currentServiceBundle;
		}
		public ServiceBundleDto CurrentServiceBundle { get; set; }
		public IEnumerable<Tuple<int, string>> ServiceBundles { get; set; }
	}
}