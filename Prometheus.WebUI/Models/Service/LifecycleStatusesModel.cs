using System;
using System.Collections.Generic;

namespace Prometheus.WebUI.Models.Service
{
    public class LifecycleStatusesModel
    {
        public int SelectedStatus { get; set; }
        public ICollection<Tuple<int, string>> LifecycleStatuses { get; set; }
    }
}