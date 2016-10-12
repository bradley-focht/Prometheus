using System;

namespace DataService.Models
{
	public interface IRole : IUserCreatedEntity
	{
		Guid Id { get; set; }
		string Name { get; set; }
	}
}