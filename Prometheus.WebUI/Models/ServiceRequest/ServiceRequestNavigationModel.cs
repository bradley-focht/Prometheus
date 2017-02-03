using System.Collections.Generic;
using Common.Dto;

namespace Prometheus.WebUI.Models.ServiceRequest
{
    /// <summary>
    /// Navigation model
    /// </summary>
    public class ServiceRequestNavigationModel
    {
        public ICollection<IServiceOptionCategoryDto> Titles { get; set; }
        public int SelectedIndex { get; set; }
    }
}