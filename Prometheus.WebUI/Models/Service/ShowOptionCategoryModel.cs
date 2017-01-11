
using System.Collections.Generic;
using Common.Dto;

namespace Prometheus.WebUI.Models.Service
{
    public class ShowOptionCategoryModel
    {
        public IOptionCategoryDto OptionCategory { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
		public IEnumerable<IServiceOptionDto> Options { get; set; }
    }
}