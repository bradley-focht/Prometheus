using System.Collections.Generic;
using Common.Enums;

namespace DataService.Models
{
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