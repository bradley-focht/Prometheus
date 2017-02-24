
using Common.Dto;

namespace Prometheus.WebUI.Models.ServiceCatalog
{
	/// <summary>
	/// Pass ICatalogPublishable items between service catalog partial views
	/// </summary>
	public class PublishableCatalogItemModel
    {
        /// <summary>
        /// Which catalog is in use
        /// </summary>
        public Helpers.Enums.ServiceCatalog Catalog { get; set; }

        /// <summary>
        /// Data to pass
        /// </summary>
        public ICatalogPublishable Item { get; set; }
    }
}