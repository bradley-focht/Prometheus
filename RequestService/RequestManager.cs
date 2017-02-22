using System.Data.Entity;
using Common.Dto;
using Common.Enums;
using Common.Exceptions;
using DataService;
using DataService.DataAccessLayer;
using DataService.Models;

namespace RequestService
{
	public class RequestManager : IRequestManager
	{
		public IServiceRequestDto ChangeRequestState(int userId, int requestId, ServiceRequestState state, string comments = null)
		{
			IServiceRequestDto request = RequestFromId(requestId);
			
			switch (state)
			{
				case ServiceRequestState.Incomplete:
					throw new ServiceRequestStateException("Cannot change the state of a Service Request to Incomplete.");
				case ServiceRequestState.Submitted:
					return SubmitRequest(userId, requestId, comments);
				case ServiceRequestState.Approved:
				case ServiceRequestState.Denied:
					ApprovalResult approval = state == ServiceRequestState.Approved ? ApprovalResult.Approved : ApprovalResult.Denied;
					return ApproveRequest(userId, requestId, approval, comments);
				case ServiceRequestState.Cancelled:
					return CancelRequest(userId, requestId, comments);
				case ServiceRequestState.Fulfilled:
					return FulfillRequest(userId, requestId, comments);
			}
			return request;
		}

		public IServiceRequestDto SubmitRequest(int userId, int requestId, string comments)
		{
			IServiceRequestDto request = RequestFromId(requestId);
			if (request.State != ServiceRequestState.Incomplete)
			{
				throw new ServiceRequestStateException(
					   string.Format("Cannot change the state of a Service Request to \"{0}\". " +
									 "Service Request is in the \"{1}\" state and must be in the " +
									 "\"{2}\" state to perform this action.",
									 ServiceRequestState.Submitted, request.State, ServiceRequestState.Incomplete));
			}

			return request;
		}

		public IServiceRequestDto CancelRequest(int userId, int requestId, string comments)
		{
			IServiceRequestDto request = RequestFromId(requestId);

			if (request.State != ServiceRequestState.Incomplete && request.State != ServiceRequestState.Submitted)
			{
				throw new ServiceRequestStateException(
					string.Format("Cannot change the state of a Service Request to \"{0}\". " +
								  "Service Request is in the \"{1}\" state and must be in the " +
								  "\"{2}\" or \"{3}\" state to perform this action.",
								  ServiceRequestState.Cancelled, request.State, ServiceRequestState.Incomplete, ServiceRequestState.Submitted));
			}

			return request;
		}

		public IServiceRequestDto ApproveRequest(int userId, int requestId, ApprovalResult approvalResult, string comments)
		{
			IServiceRequestDto request = RequestFromId(requestId);

			if (request.State != ServiceRequestState.Submitted)
			{
				throw new ServiceRequestStateException(
					   string.Format("Cannot change the state of a Service Request to \"{0}\". " +
									 "Service Request is in the \"{1}\" state and must be in the " +
									 "\"{2}\" state to perform this action.", 
									 approvalResult, request.State, ServiceRequestState.Submitted));
			}

			if (UserCanApproveRequest(userId, requestId))
			{
				using (var context = new PrometheusContext())
				{
					//Build and save approval transaction
					Approval approval = new Approval()
					{
						ApproverId = userId,
						Comments = comments,
						RequestorId = request.RequestedByUserId,
						ServiceRequestId = request.Id,
						Result = approvalResult
					};
					context.Approvals.Add(approval);
					context.SaveChanges(userId);
					
					//Change state of the entity
					var requestEntity = context.ServiceRequests.Find(requestId);
					requestEntity.State = approvalResult == ApprovalResult.Approved
						? ServiceRequestState.Approved
						: ServiceRequestState.Denied;
					context.Entry(requestEntity).State = EntityState.Modified;
					context.SaveChanges(userId);
					request = ManualMapper.MapServiceRequestToDto(requestEntity);
				}
			}

			return request;
		}

		private bool UserCanApproveRequest(int userId, int requestId)
		{
			//TODO: Do this Sean
			return true;
		}

		public IServiceRequestDto FulfillRequest(int userId, int requestId, string comments)
		{
			IServiceRequestDto request = RequestFromId(requestId);

			if (request.State != ServiceRequestState.Approved)
			{
				throw new ServiceRequestStateException(
					string.Format("Cannot change the state of a Service Request to \"{0}\". " +
								  "Service Request is in the \"{1}\" state and must be in the " +
								  "\"{2}\" state to perform this action.",
								  ServiceRequestState.Fulfilled, request.State, ServiceRequestState.Approved));
			}
			
			return request;
		}

		private IServiceRequestDto RequestFromId(int requestId)
		{
			IServiceRequestDto request;
			using (var context = new PrometheusContext())
			{
				request = ManualMapper.MapServiceRequestToDto(context.ServiceRequests.Find(requestId));
			}

			if (request == null)
				throw new EntityNotFoundException("", typeof(ServiceRequest), requestId);

			return request;
		}
	}
}
