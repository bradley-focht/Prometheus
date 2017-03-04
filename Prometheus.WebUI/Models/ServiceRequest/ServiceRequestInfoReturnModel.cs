using System;
using System.ComponentModel.DataAnnotations;
using Prometheus.WebUI.Helpers.Enums;

namespace Prometheus.WebUI.Models.ServiceRequest
{
    /// <summary>
    /// intermediary between ServiceRequestDto and UI
    /// </summary>
	public class ServiceRequestInfoReturnModel
	{
        /// <summary>
        /// Service Request Id
        /// </summary>
	    public int Id { get; set; }
        /// <summary>
        /// Initial Option selected Id
        /// </summary>
        public int ServiceOptionId { get; set; }

        /// <summary>
        /// User Comments
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Office Use comments
        /// </summary>
        public string OfficeUse { get; set; }
        /// <summary>
        /// Requestor Id
        /// </summary>
		public string Requestor { get; set; }

        /// <summary>
        /// Date requested for
        /// </summary>
        [Required]
		public DateTime RequestedDate { get; set; }

		[Required]
		public int DepartmentId { get; set; }
	    public ServiceRequestMode Mode { get; set; }
    }
}