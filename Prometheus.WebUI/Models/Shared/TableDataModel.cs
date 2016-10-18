using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prometheus.WebUI.Models.Shared
{
    public class TableDataModel
    {
        public IEnumerable<string> Titles { get; set; }
        public IEnumerable<KeyValuePair<int, IEnumerable<string>>> Data { get; set; }
        public string Type { get; set; }
    }
}