using System;
using System.Collections.Generic;

namespace Prometheus.WebUI.Models.Service
{
    public class LifecycleStatusesModel
    {
        public int SelectedStatus { get; set; }
        public IEnumerable<Tuple<int, string>> LifecycleStatuses { get; set; }
    }
}