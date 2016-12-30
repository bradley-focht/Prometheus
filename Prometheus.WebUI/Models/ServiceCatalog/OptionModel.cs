using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Dto;
using Common.Enums.Entities;

namespace Prometheus.WebUI.Models.ServiceCatalog
{
	public class OptionModel
	{
		public ServiceTypeRole Catalog { get; set; }
		public ICatalogable Option { get; set; }
		public List<ICatalogable> Options { get; set; }		
	}
}