using System.Collections.Generic;
using Common.Dto;

namespace Prometheus.WebUI.Models.ServiceRequestMaintenance
{
	/// <summary>
	/// Model to view details of a service package
	/// </summary>
	public class PackageViewModel
	{
		/// <summary>
		/// The service package itself
		/// </summary>
		public IServiceRequestPackageDto ServiceRequestPackage { get; set; }

		/// <summary>
		/// Details useful for display and linking
		/// </summary>
		public IEnumerable<PackageItem> PackageItems { get; set; }
 	}
}