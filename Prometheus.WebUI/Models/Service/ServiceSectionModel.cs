using Common.Dto;

namespace Prometheus.WebUI.Models.Service
{
    public class ServiceSectionModel
    {
        public string Section { get; set; }
        public IServiceDto Service;
        public int SectionItemId { get; set; }
    }
}