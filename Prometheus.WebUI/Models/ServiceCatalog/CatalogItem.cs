using System;
using System.Collections.Generic;
using System.Linq;
using Common.Dto;

namespace Prometheus.WebUI.Models.ServiceCatalog
{
	public class CatalogItem : ICatalogItem
	{
		public string ServiceName { get; set; }
		public int ServiceId { get; set; }
		public string ServiceDescription { get; set; }
		public List<ICatalogable> Options { get; set; }
	}
}