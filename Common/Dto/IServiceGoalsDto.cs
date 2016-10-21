using System;

namespace ServicePortfolio.Dto
{
    public interface IServiceGoalsDto
	{
        string Description { get; set; }
        DateTime? EndDate { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        DateTime? StartDate { get; set; }
    }
}