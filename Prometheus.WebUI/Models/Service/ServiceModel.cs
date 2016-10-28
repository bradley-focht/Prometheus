using Common.Dto;
namespace Prometheus.WebUI.Models.Service
{
    public class ServiceModel
    {
        public ServiceModel()
        {
            
        }
        public ServiceModel(IServiceDto service, string selectedSection)     
        {
            Service = service;
            SelectedSection = selectedSection;
        }

        public IServiceDto Service { get; set; }
        public string SelectedSection { get; set; }
    }
}