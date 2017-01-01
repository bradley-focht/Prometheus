using System;
using System.Collections.Generic;
using Common.Dto;
using Common.Enums.Entities;

namespace Prometheus.WebUI.Models.ServiceCatalog
{
	public class ServiceOptionsModel
	{
		public ServiceTypeRole Catalog { get; set; }
		public string ServiceName { get; set; }
		public int ServiceId { get; set; }
		public IEnumerable<ICatalogable> Options { get; set; }
		public IEnumerable<Tuple<int, string>> ServiceNames { get; set; }
	}
}