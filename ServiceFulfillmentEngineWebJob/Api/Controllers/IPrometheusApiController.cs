using System.Collections.Generic;
using ServiceFulfillmentEngineWebJob.Api.Models;

namespace ServiceFulfillmentEngineWebJob.Api.Controllers
{
	public interface IPrometheusApiController
	{
		/// <summary>
		/// Makes Prometheus DELETE /Requests/{id} call for an ID
		/// </summary>
		/// <param name="serviceRequestId"></param>
		void DeleteRequestById(int serviceRequestId);


		/// <summary>
		/// Makes Prometheus GET /Requests/{id} call for an ID
		/// </summary>
		/// <param name="serviceRequestId"></param>
		IServiceRequest GetServiceRequestById(int serviceRequestId);

		/// <summary>
		/// Makes Prometheus GET /Requests call
		/// </summary>
		IEnumerable<ServiceRequest> GetServiceRequests();

		/// <summary>
		/// Makes Prometheus PUT /Requests/{id} call for a SR provided
		/// </summary>
		/// <param name="serviceRequestId"></param>
		/// <param name="serviceRequest">Updated SR</param>
		IServiceRequest UpdateRequestById(int serviceRequestId, IServiceRequest serviceRequest);
	}
}