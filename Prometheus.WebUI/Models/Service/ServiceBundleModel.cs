using System;
using System.Collections.Generic;

namespace Prometheus.WebUI.Models.Service
{
    public class ServiceBundleModel
    {
        public IEnumerable<Tuple<int, string>> ServiceBundles { get; set; }
        public int SelectedServiceBundle { get; set; }
    }
}