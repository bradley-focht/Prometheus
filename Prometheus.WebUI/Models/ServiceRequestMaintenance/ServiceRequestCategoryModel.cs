using System.Collections.Generic;
using Common.Dto;

namespace Prometheus.WebUI.Models.ServiceRequestMaintenance
{
    public class ServiceRequestCategoryModel
    {
            public IServiceOptionCategoryDto Category { get; set; }
            public IEnumerable<IUserInput> UserInputs { get; set; }
            public int ServiceId { get; set; }
            public string ServiceName { get; set; }
    }
}