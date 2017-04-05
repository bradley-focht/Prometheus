using System.Collections.Generic;
using Common.Enums;

namespace DataService.Models
{
	/// <summary>
	/// A collection of orderable items in the Catalog that a Service Request is built from. Items in a Service Request Package
	/// are ones that should be normally ordered together.
	/// </summary>
	public interface IServiceRequestPackage : IUserCreatedEntity
	{
		int Id { get; set; }
		string Name { get; set; }

		/// <summary>
		/// Action for SR to perform
		/// </summary>
		ServiceRequestAction Action { get; set; }
		ICollection<ServiceOptionCategoryTag> ServiceOptionCategoryTags { get; set; }
	}
}