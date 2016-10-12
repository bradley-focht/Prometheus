using System;

namespace DataService.Models
{
	public interface IServiceRequestOption : IUserCreatedEntity
	{
		Guid Id { get; set; }
		Guid ServiceId { get; set; }
	}
}