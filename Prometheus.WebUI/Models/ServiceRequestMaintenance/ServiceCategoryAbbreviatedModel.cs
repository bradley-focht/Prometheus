using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Prometheus.WebUI.Models.ServiceRequestMaintenance
{
    public class ServiceCategoryAbbreviatedModel
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int Popularity { get; set; }
        [Required]
        public string Name { get; set; }
        [AllowHtml]
        public string BusinessValue { get; set; }
    }
}