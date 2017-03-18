using System.Collections.Generic;
using Common.Dto;

namespace Prometheus.WebUI.Models.ServiceCatalog
{
	public class ServiceSummary : ICatalogPublishable
	{
		public List<ICatalogPublishable> Options { get; set; }
		public int Id { get; set; }
		public int Popularity { get; set; }
		public string Name { get; set; }
		public string BusinessValue { get; set; }
		public bool Published { get; set; }
	}
}