using System;

namespace DataService.Models
{
	public interface IServiceBundle : IUserCreatedEntity
	{
		Guid Id { get; set; }
	}
}