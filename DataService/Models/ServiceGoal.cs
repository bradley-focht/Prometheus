using System;

namespace DataService.Models
{
    public class ServiceGoal : IServiceGoal
    {
        public int ServiceId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        ///TODO: Brad Not sure what to do with this one, maybe make it an enum?
        /// Add this to interface
        public string Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
