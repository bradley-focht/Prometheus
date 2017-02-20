using Common.Dto;
using Common.Enums;
using Common.Exceptions;
using DataService;
using DataService.DataAccessLayer;
using DataService.Models;

namespace RequestService
{
	public class RequestManager
	{
		public IServiceRequestDto ChangeRequestState(int userId, int requestId, ServiceRequestState state, string comments = null)
		{
			IServiceRequestDto request;
			using (var context = new PrometheusContext())
			{
				request = ManualMapper.MapServiceRequestToDto(context.ServiceRequests.Find(requestId));
			}

			if(request == null)
				throw new EntityNotFoundException("", typeof(ServiceRequest), requestId);

			switch (state)
			{
				case ServiceRequestState.Incomplete:
					throw new ServiceRequestStateException("Cannot change the state of a Service Request to Incomplete.");
				case ServiceRequestState.Submitted:
					break;
				case ServiceRequestState.Approved:
				case ServiceRequestState.Denied:
					break;
				case ServiceRequestState.Cancelled:
					break;
				case ServiceRequestState.Fulfilled:
					break;
			}
			return request;
		}

		public bool ApproveRequest(int userId, int requestId, ApprovalResult approval, string comments)
		{
			return false;
		}

		public bool CancelRequest(int userId, int requestId, string comments)
		{
			return false;
		}

		public bool FulfillRequest(int userId, int requestId, string comments)
		{
			return false;
		}
	}
}
