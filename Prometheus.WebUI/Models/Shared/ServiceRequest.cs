using System;

namespace Prometheus.WebUI.Models.Shared
{
    /// <summary>
    /// intermediary between ServiceRequestDto and UI
    /// </summary>
	public class ServiceRequest
	{
	    public int Id { get; set; }
        public string Comments { get; set; }
        public string OfficeUse { get; set; }
		public string Requestor { get; set; }
		public DateTime? RequiredDate { get; set; }
        public  int InitialOptionId { get; set; }

    }
}