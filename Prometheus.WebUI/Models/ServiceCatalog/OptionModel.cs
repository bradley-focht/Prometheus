using System.Collections.Generic;
using Common.Dto;

namespace Prometheus.WebUI.Models.ServiceCatalog
{
	public class OptionModel
	{
        /// <summary>
        /// Catalog controls
        /// </summary>
	    public CatalogControlsModel Controls { get; set; }
        /// <summary>
        /// data to send
        /// </summary>
        public Helpers.Enums.ServiceCatalog Catalog { get; set; }
		public ICatalogPublishable Option { get; set; }
		public IEnumerable<ICatalogPublishable> Options { get; set; }
		public string ServiceName { get; set; }
		public int ServiceId { get; set; }
	}
}