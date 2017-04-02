using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	/// <summary>
	/// Logical grouping for Service Options offered by a Service
	/// </summary>
	public class ServiceOptionCategory : IServiceOptionCategory
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		/// <summary>
		/// ID for Service that the Category belongs to
		/// </summary>
		public int ServiceId { get; set; }

		/// <summary>
		/// Used for sorting in the Service Catalog. Larger is more popular
		/// </summary>
		public int Popularity { get; set; }

		/// <summary>
		/// These attributes come from the Service Design Package
		/// </summary>
		public string Features { get; set; }

		/// <summary>
		/// product or service benefits to the customer
		/// </summary>
		public string Benefits { get; set; }

		/// <summary>
		/// how is this product or service supported
		/// </summary>
		public string Support { get; set; }

		/// <summary>
		/// SR name code
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// product or service over view, not serice catalog visible
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// If a quantity of the Service Option can be requested
		/// </summary>
		public bool Quantifiable { get; set; }

		/// <summary>
		/// Catalog visible overview of product or service
		/// </summary>
		public string BusinessValue { get; set; }

		/// <summary>
		/// Visible in results
		/// </summary>
		public bool Published { get; set; }

		/// <summary>
		/// Unique name
		/// </summary>
		public string Name { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public virtual Service Service { get; set; }
		public virtual ICollection<ServiceOption> ServiceOptions { get; set; }
		public virtual ICollection<ServiceRequestPackage> ServiceRequestPackages { get; set; }
	}
}
