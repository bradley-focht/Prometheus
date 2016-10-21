using System.ComponentModel.DataAnnotations;

namespace DataService.Models
{
    public class ServiceWorkUnit : IServiceWorkUnit
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        [Display(Name = "Work Unit")]
        public string WorkUnit { get; set; }
        
        public string Manager { get; set; }
        public string Responsibilities { get; set; }
    }
}
