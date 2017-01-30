using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Common.Dto;

namespace Prometheus.WebUI.Models.ServiceRequestMaintenance
{
    [Bind(Exclude = "Services")]
    public class PackageModel
    {
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        public ICollection<int> Associations { get; set; }
        public ICollection<ServiceDto> Services { get; set; }
    }
}