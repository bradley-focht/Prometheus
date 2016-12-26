using Common.Enums;
using System;
using Common.Enums.Entities;

namespace DataService.Models
{
	public interface IServiceGoal : IUserCreatedEntity
	{
		int Id { get; set; }
		int ServiceId { get; set; }

		string Description { get; set; }
		DateTime? EndDate { get; set; }
		string Name { get; set; }
		ServiceGoalType Type { get; set; }
		DateTime? StartDate { get; set; }

		Service Service { get; set; }
	}
}