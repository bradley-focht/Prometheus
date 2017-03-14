using System;
using System.Collections.Generic;
using Common.Dto;

namespace Prometheus.WebUI.Models.ServicePortfolio
{
	public class ServiceBundlesModel
	{
		public ServiceBundleModel CurrentServiceBundle { get; set; }
		public IEnumerable<Tuple<int, string>> ServiceBundles { get; set; }
	}
}