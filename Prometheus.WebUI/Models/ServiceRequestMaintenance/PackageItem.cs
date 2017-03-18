using Common.Dto;

namespace Prometheus.WebUI.Models.ServiceRequestMaintenance
{
	/// <summary>
	/// Used to display details of the tags in a package
	/// </summary>
	public class PackageItem
	{
		public string Name { get; set; }
		public int Id { get; set; }
		public IServicePackageTag Tag { get; set; }
	}
}