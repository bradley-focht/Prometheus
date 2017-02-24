using System;
using System.Collections.Generic;
using Common.Dto;

namespace Prometheus.WebUI.Models.ServiceCatalog
{
	public class ServiceOptionsModel
	{
		public Helpers.Enums.ServiceCatalog Catalog { get; set; }
		public string ServiceName { get; set; }
		public int ServiceId { get; set; }
		public IEnumerable<ICatalogPublishable> Options { get; set; }
		public IEnumerable<Tuple<int, string>> ServiceNames { get; set; }
        public CatalogControlsModel Controls { get; set; }
    }
}