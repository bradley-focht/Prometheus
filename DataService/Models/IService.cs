using System;

namespace DataService.Models
{
	public interface IService : IUserCreatedEntity
	{
		Guid Id { get; set; }
		Guid ServiceBundleId { get; set; }
	}
}