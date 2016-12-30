using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Dto;
using Common.Enums.Entities;

namespace Prometheus.WebUI.Models.ServiceCatalog
{
	public class CatalogModel
	{
		public ServiceTypeRole Catalog { get; set; }
		public ICollection<CatalogItem> CatalogItems { get; set; }
	}



}