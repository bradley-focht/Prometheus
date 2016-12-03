using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prometheus.WebUI.Helpers
{
    public class TileSets
    {
        public ICollection<ITile> AdministrationTiles { get; set; }
        public ICollection<ITile> ApplicationTiles { get; set; }
    }
}