using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Enums;

namespace DataService.Models
{
	/// <summary>
	/// A collection of orderable items in the Catalog that a Service Request is built from. Items in a Service Request Package
	/// are ones that should be normally ordered together.
	/// </summary>
	public class ServiceRequestPackage : IServiceRequestPackage
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		/// <summary>
		/// Action for SR to perform
		/// </summary>
		public ServiceRequestAction Action { get; set; }

		public string Name { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public virtual ICollection<ServiceOptionCategoryTag> ServiceOptionCategoryTags { get; set; }
		public virtual ICollection<ServiceTag> ServiceTags { get; set; }
	}
}
