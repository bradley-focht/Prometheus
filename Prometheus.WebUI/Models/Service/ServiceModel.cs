using DataService.Models;

namespace Prometheus.WebUI.Models.Service
{
    public class ServiceModel
    {
        public ServiceModel()
        {
            
        }
        public ServiceModel(IService service, string selectedSection)     
        {
            Service = service;
            SelectedSection = selectedSection;
        }

        public IService Service { get; set; }
        public string SelectedSection { get; set; }
    }
}