using System;
using System.Collections.Generic;

namespace Prometheus.WebUI.Models.SystemAccess
{
    public class UserControlsModel
    {
        public IEnumerable<Tuple<int, string>> Roles { get; set; }
        public string AppliedFilter { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}