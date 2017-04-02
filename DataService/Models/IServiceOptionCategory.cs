using System.Collections.Generic;
using Common.Dto;

namespace DataService.Models
{
	/// <summary>
	/// Logical grouping for Service Options offered by a Service
	/// </summary>
	public interface IServiceOptionCategory : ICatalogPublishable, IUserCreatedEntity
	{
		Service Service { get; set; }

		/// <summary>
		/// ID for Service that the Category belongs to
		/// </summary>
		int ServiceId { get; set; }

		/// <summary>
		/// These attributes come from the Service Design Package
		/// </summary>
		string Features { get; set; }

		/// <summary>
		/// SR name code
		/// </summary>
		string Code { get; set; }

		/// <summary>
		/// product or service benefits to the customer
		/// </summary>
		string Benefits { get; set; }

		/// <summary>
		/// how is this product or service supported
		/// </summary>
		string Support { get; set; }

		/// <summary>
		/// product or service over view, not serice catalog visible
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// If a quantity of the Service Option can be requested
		/// </summary>
		bool Quantifiable { get; set; }
		ICollection<ServiceRequestPackage> ServiceRequestPackages { get; set; }
		ICollection<ServiceOption> ServiceOptions { get; set; }
	}
}