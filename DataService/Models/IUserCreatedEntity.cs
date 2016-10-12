using System;

namespace DataService.Models
{
	public interface IUserCreatedEntity : ICreatedEntity
	{
		Guid CreatedByUserId { get; set; }
		Guid UpdatedByUserId { get; set; }
	}
}
