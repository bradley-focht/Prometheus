using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prometheus.WebUI.Models.ServiceRequestApproval
{
    /// <summary>
    /// Composite class for Service Request screen
    /// </summary>
    public class ServiceRequestApprovalModel
    {
        public ServiceRequestApprovalControls Controls { get; set; }
        private ICollection<ServiceRequestTableItemModel> ServiceRequests { get; set; }
        
    }
}