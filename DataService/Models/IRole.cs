using System.Collections.Generic;

namespace DataService.Models
{
	public interface IRole : IUserCreatedEntity
	{
		int Id { get; set; }
		string Name { get; set; }
		ICollection<IUser> Users { get; set; }
	}
}