using System.Web.Mvc;

namespace DataService.Models
{
    public class ServiceMeasure : IServiceMeasure
    {
        [HiddenInput]
        public int Id { get; set; }
        [HiddenInput]
        public int ServiceId { get; set; }
        public string Method { get; set; }
        public string Outcome { get; set; }
    }
}
