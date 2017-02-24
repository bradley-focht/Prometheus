namespace Prometheus.WebUI.Models.ServiceCatalog
{
	/// <summary>
	/// Specifically for the controls at the top of the Service Catalog page
	/// </summary>
	public class CatalogControlsModel
	{
		public Helpers.Enums.ServiceCatalog CatalogType {get; set;}
		public int TotalPages { get; set; }
		public int PageNumber { get; set; }
		public string SearchString { get; set; }
	}
}