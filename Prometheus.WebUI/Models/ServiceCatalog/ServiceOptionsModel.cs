using System;
using System.Collections.Generic;
using Common.Dto;
using Prometheus.WebUI.Helpers.Enums;

namespace Prometheus.WebUI.Models.ServiceCatalog
{
	public class ServiceOptionsModel
	{
		public ServiceCatalogs Catalog { get; set; }
		public string ServiceName { get; set; }
		public int ServiceId { get; set; }
		public IEnumerable<ICatalogPublishable> Options { get; set; }
		public IEnumerable<Tuple<int, string>> ServiceNames { get; set; }
        public CatalogControlsModel Controls { get; set; }
    }
}