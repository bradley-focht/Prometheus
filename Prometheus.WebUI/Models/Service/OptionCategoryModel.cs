using Common.Dto;

namespace Prometheus.WebUI.Models.Service
{
    public class OptionCategoryModel
    {
        public OptionCategoryDto OptionCategory { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Action { get; set; }
    }
}