using System.Collections.Generic;

namespace Prometheus.WebUI.Models.Shared
{
    public class ServiceNavModel
    {
        public ServiceNavModel()
        {
            
        }

        public ServiceNavModel(IEnumerable<string> sections, string selectedSection, int id, string action)
        {
            Sections = sections;
            SelectedSection = selectedSection;
            Id = id;
            Action = action;
        }
        public IEnumerable<string> Sections { get; set; }
        public string SelectedSection { get; set; }
        public int Id { get; set; }
        public string Action { get; set; }
    }
}