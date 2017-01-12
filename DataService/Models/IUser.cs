using System.Collections.Generic;

namespace DataService.Models
{
	public interface IUser : IUserCreatedEntity
	{
		int Id { get; set; }

		string Name { get; set; }
		string Password { get; set; }

		ICollection<Role> Roles { get; set; }
	}
}