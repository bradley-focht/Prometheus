using System;

namespace Prometheus.WebUI.Helpers
{
    public interface ITile
    {
        string Action { get; set; }
        string Controller { get; set; }
        Tuple<int, int> Dimensions { get; set; }
        string Icon { get; set; }
        string Text { get; set; }
    }
}