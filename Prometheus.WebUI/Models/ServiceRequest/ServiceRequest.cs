using System;
using System.ComponentModel.DataAnnotations;

namespace Prometheus.WebUI.Models.Shared
{
    /// <summary>
    /// intermediary between ServiceRequestDto and UI
    /// </summary>
	public class ServiceRequest
	{
	    public int Id { get; set; }
        public int PackageId { get; set; }
        public string Comments { get; set; }
        public string OfficeUse { get; set; }
		public string Requestor { get; set; }
        [Required]
		public DateTime RequestedDate { get; set; }
        public  int InitialOptionId { get; set; }

    }
}