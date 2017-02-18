using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Prometheus.WebUI.Models.ServiceRequestMaintenance
{
    /// <summary>
    /// updated attributes from service request maintenance
    /// </summary>
    public class ServiceCategoryAbbreviatedModel
    {
        public int Id { get; set; }
        [Required]
        public int ServiceId { get; set; }
        public int Popularity { get; set; }
        [Required]
        public string Name { get; set; }
        [AllowHtml]
        public string BusinessValue { get; set; }
        [Required]
        public bool Quantifiable { get; set; }
    }
}