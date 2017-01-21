using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prometheus.WebUI.Models.ServiceRequest
{
    public class ServiceRequestNavigationModel
    {
        public IEnumerable<Tuple<int, string>> Titles { get; set; }
        public int SelectedIndex { get; set; }
    }
}