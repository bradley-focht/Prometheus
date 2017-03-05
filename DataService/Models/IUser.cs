using System;
using System.Collections.Generic;

namespace DataService.Models
{
	public interface IUser : IUserCreatedEntity
	{
		int Id { get; set; }

		int DepartmentId { get; set; }
		string Name { get; set; }
		Guid AdGuid { get; set; }

		Department Department { get; set; }
		ICollection<Role> Roles { get; set; }
	}
}