using Common.Enums;

namespace Prometheus.WebUI.Models.ServiceRequestApproval
{
    public class ServiceRequestTableItemModel
    {
        /// <summary>
        /// Navigation properties
        /// </summary>
        public int Id { get; set; }
        public int ServiceOptionId { get; set; }

        /// <summary>
        /// Service Request "name"
        /// </summary>
        public string PackageName { get; set; }
        public string UserName { get; set; }
        public string UserLocation { get; set; }

        /// <summary>
        /// Current State of the Request
        /// </summary>
        public ServiceRequestState State { get; set; }

    
        
    }
}