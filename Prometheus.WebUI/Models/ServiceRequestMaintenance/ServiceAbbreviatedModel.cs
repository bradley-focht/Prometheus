using System.ComponentModel.DataAnnotations;
using Common.Dto;

namespace Prometheus.WebUI.Models.ServiceRequestMaintenance
{
    /// <summary>
    /// Used for post back of catalog-relevant fields only
    /// </summary>
    public class ServiceAbbreviatedModel : ICatalogPublishable
    {
        [Required]
        public int Id { get; set; }
        public int Popularity { get; set; }
        public string Name { get; set; }
        public string BusinessValue { get; set; }
	    public bool Published { get; set; }
    }
}