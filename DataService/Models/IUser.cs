using System;

namespace DataService.Models
{
	public interface IUser : IUserCreatedEntity
	{
		Guid Id { get; set; }
		Guid RoleId { get; set; }
	}
}