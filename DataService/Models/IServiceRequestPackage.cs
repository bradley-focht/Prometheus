using System.Collections.Generic;

namespace DataService.Models
{
	public interface IServiceRequestPackage : IUserCreatedEntity
	{
		int Id { get; set; }
		string Name { get; set; }
		ICollection<ServiceOptionCategory> ServiceOptionCategories { get; set; }
	}
}