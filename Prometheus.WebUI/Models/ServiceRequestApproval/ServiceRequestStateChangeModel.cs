using System.Collections.Generic;
using Common.Enums;
using Prometheus.WebUI.Models.ServiceRequest;

namespace Prometheus.WebUI.Models.ServiceRequestApproval
{
	/// <summary>
	/// For the change of service request states
	/// </summary>
	public class ServiceRequestStateChangeModel
	{
		public ServiceRequestModel ServiceRequestModel { get; set; }
		public IEnumerable<DisplayListModel> DisplayList { get; set; }
		public ServiceRequestState NextState { get; set; }

	}
}