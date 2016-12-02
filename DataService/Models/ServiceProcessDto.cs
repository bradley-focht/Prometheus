using System;

namespace DataService.Models
{
    public class ServiceProcess : IServiceProcess
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Benefits { get; set; }
        public string Improvements { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
