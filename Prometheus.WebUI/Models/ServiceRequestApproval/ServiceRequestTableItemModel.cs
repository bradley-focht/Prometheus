using System;
using Common.Enums;

namespace Prometheus.WebUI.Models.ServiceRequestApproval
{
    public class ServiceRequestTableItemModel
    {
        /// <summary>
        /// Navigation properties
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Service Request "name"
        /// </summary>
        public string PackageName { get; set; }
        /// <summary>
        /// Display Name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Branch or office location
        /// </summary>
        public string UserLocation { get; set; }

        /// <summary>
        /// Current State of the Request
        /// </summary>
        public ServiceRequestState State { get; set; }

        public DateTime DateRequired { get; set; }
        public DateTime? DateSubmitted { get; set; }

    
        
    }
}