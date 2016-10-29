using System.Collections.Generic;

namespace Prometheus.WebUI.Models.Shared
{
    public class ServiceNavModel
    {
        public ServiceNavModel()
        {
        }

        public ServiceNavModel(IEnumerable<KeyValuePair<string, string>> navLinks, string selectedRouteString, int id, string action)
        {
            SelectedRouteString = selectedRouteString;
            Id = id;
            Action = action;
            NavLinks = navLinks;
        }
        //key is the routeArg and value is the text
        public IEnumerable<KeyValuePair<string, string>> NavLinks { get; set; }

        //the selected route string
        public string SelectedRouteString { get; set; }
        public int Id { get; set; }
        public string Action { get; set; }
    }
}