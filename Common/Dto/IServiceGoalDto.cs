using Common.Enums;
using System;
using Common.Enums.Entities;

namespace Common.Dto
{
	public interface IServiceGoalDto : IUserCreatedEntityDto
	{
		int Id { get; set; }
		int ServiceId { get; set; }

		string Description { get; set; }
		DateTime? EndDate { get; set; }
		string Name { get; set; }
		DateTime? StartDate { get; set; }
		ServiceGoalType Type { get; set; }
	}
}