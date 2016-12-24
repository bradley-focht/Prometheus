

using System.Collections.Generic;
using Common.Dto;

namespace Prometheus.WebUI.Models.Service
{
    public class ServiceViewModel
    {
        public IEnumerable<IServiceDto> Services { get; set; }
        public ServiceViewControlsModel ControlsModel { get; set; }

    }
}