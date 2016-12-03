using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prometheus.WebUI.Helpers
{
    public class Tile : ITile
    {
        public string Icon { get; set; }
        //  public List<IPermission> Permissions { get; set; }
        /// <summary>
        /// Dimensions in height, width
        /// </summary>
        public Tuple<int, int> Dimensions { get; set; }
        public string Text { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}