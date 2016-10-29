using Common.Enums;
using System;

namespace DataService.Models
{
	public interface IServiceGoal : IUserCreatedEntity
	{
		string Description { get; set; }
		DateTime? EndDate { get; set; }
		int Id { get; set; }
		string Name { get; set; }
		ServiceGoalType Type { get; set; }
		DateTime? StartDate { get; set; }
	}
}