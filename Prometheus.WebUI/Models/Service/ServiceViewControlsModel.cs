using System;
using System.Collections.Generic;

namespace Prometheus.WebUI.Models.Service
{
    public class ServiceViewControlsModel
    {
        public ICollection<Tuple<string, string, IEnumerable<Tuple<int, string>>>> FilterMenu { get; set; }

        public string AppliedFilter { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public string FilterBy { get; set; }
        public string FilterArg { get; set; }


    }
}