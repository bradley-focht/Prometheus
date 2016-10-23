using System.Collections.Generic;

namespace Prometheus.WebUI.Models.Shared
{
    public class TableDataModel
    {
        public TableDataModel() {  }

        public TableDataModel(IEnumerable<string> titles, IEnumerable<KeyValuePair<int, IEnumerable<string>>> data )
        {
            Titles = titles;
            Data = data;
        }

        public IEnumerable<string> Titles { get; set; }
        public IEnumerable<KeyValuePair<int, IEnumerable<string>>> Data { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}