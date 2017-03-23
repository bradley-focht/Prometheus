using System.Collections.Generic;
using ServiceFulfillmentEngineWebJob.Api.Models;

namespace ServiceFulfillmentEngineWebJob.Api.Controllers
{
	public interface IPrometheusApiController
	{
		void DeleteRequestById(int serviceRequestId);
		IServiceRequest GetServiceRequestById(int serviceRequestId);
		IEnumerable<IServiceRequest> GetServiceRequests();
		void UpdateRequestById(int serviceRequestId, IServiceRequest serviceRequest);
	}
}