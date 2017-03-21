using System.Collections.Generic;
using Common.Dto;

namespace ServiceFulfillmentEngineWebJob.ApiControllers
{
	public interface IPrometheusApiController
	{
		void DeleteRequestById(int serviceRequestId);
		IServiceRequestDto GetServiceRequestById(int serviceRequestId);
		IEnumerable<IServiceRequestDto> GetServiceRequests();
		void UpdateRequestById(int serviceRequestId, IServiceRequestDto serviceRequest);
	}
}