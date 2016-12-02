using System;

namespace DataService.Models
{
    public interface IServiceProcess
    {
        string Benefits { get; set; }
        DateTime? DateCreated { get; set; }
        DateTime? DateUpdated { get; set; }
        int Id { get; set; }
        string Improvements { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        int ServiceId { get; set; }
    }
}