using System.Collections.Generic;

namespace DataService.Models
{
    public interface IOptionCategory
    {
        string Description { get; set; }
        int Id { get; set; }
        int ServiceId { get; set; }
        string Name { get; set; }
        ICollection<ServiceOption> ServiceOptions { get; set; }
    }
}