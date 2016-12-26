using Common.Enums;
using System.Collections.Generic;
using Common.Enums.Entities;

namespace Common.Dto
{
	public interface IServiceSwotDto : IUserCreatedEntityDto
	{
		int Id { get; set; }
		int ServiceId { get; set; }

		string Description { get; set; }
		string Item { get; set; }
		ServiceSwotType Type { get; set; }

		ICollection<ISwotActivityDto> SwotActivities { get; set; }
	}
}