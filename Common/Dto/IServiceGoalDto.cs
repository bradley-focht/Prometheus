using System;

namespace Common.Dto
{
	public interface IServiceGoalDto
	{
		string Description { get; set; }
		DateTime? EndDate { get; set; }
		int Id { get; set; }
		string Name { get; set; }
		DateTime? StartDate { get; set; }
	}
}