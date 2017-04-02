using System.Collections.Generic;

namespace DataService.Models
{
	/// <summary>
	/// Service Request Queue Associated with users
	/// </summary>
	public interface IDepartment : IUserCreatedEntity
	{
		int Id { get; set; }

		/// <summary>
		/// Queue name to match script result
		/// </summary>
		string Name { get; set; }


		/// <summary>
		/// Users that belong to the Department
		/// </summary>
		ICollection<User> Users { get; set; }
		ICollection<ServiceRequest> ServiceRequests { get; set; }
	}
}