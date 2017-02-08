using Common.Enums;

namespace Prometheus.WebUI.Models.ServiceRequestApproval
{
    public class ServiceRequestTableItemModel
    {
        public int Id { get; set; }
        public string PackageName { get; set; }
        public string UserName { get; set; }
        public string UserLocation { get; set; }

        /// <summary>
        /// Current State of the Request
        /// </summary>
        public ServiceRequestState State { get; set; }
        
    }
}