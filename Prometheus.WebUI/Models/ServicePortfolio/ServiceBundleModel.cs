using System;
using System.Collections.Generic;
using Common.Dto;

namespace Prometheus.WebUI.Models.ServicePortfolio
{
	/// <summary>
	/// A single service bundle and related services
	/// </summary>
	public class ServiceBundleModel
	{
		public IServiceBundleDto ServiceBundle { get; set; }

		public IEnumerable<Tuple<int, string>> ServiceNames { get; set; }
	}
}