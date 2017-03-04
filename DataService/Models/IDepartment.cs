using System.Collections.Generic;

namespace DataService.Models
{
	public interface IDepartment : IUserCreatedEntity
	{
		int Id { get; set; }
		string Name { get; set; }

		ICollection<User> Users { get; set; }
		ICollection<ServiceRequest> ServiceRequests { get; set; }
	}
}