using DataService.Models;

namespace Prometheus.WebUI.Models.Service
{
    public class ServiceSectionModel
    {
        public string Section { get; set; }
        public IService Service;
        public int SectionItemId { get; set; }
    }
}