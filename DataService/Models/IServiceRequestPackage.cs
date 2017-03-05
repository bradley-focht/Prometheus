using System.Collections.Generic;
using Common.Enums;

namespace DataService.Models
{
	public interface IServiceRequestPackage : IUserCreatedEntity
	{
		int Id { get; set; }
		string Name { get; set; }
		ServiceRequestAction Action { get; set; }
		ICollection<ServiceOptionCategoryTag> ServiceOptionCategoryTags { get; set; }
	}
}