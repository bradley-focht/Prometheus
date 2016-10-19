using System;

namespace DataService.Models
{
    public interface IServiceGoals
    {
        string Description { get; set; }
        DateTime? EndDate { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        DateTime? StartDate { get; set; }
    }
}