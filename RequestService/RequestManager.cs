using System;
using System.Collections.Generic;
using System.Data.Entity;
using Common.Dto;
using Common.Enums;
using Common.Enums.Permissions;
using Common.Exceptions;
using DataService;
using DataService.DataAccessLayer;
using DataService.Models;
using UserManager.Controllers;

namespace RequestService
{
	public class RequestManager : IRequestManager
	{
		private readonly IPermissionController _permissionController;
		public RequestManager(IPermissionController permissionController)
		{
			_permissionController = permissionController;
		}

		public IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> ChangeRequestState(int userId, int requestId, ServiceRequestState state, string comments = null)
		{
			IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> request = RequestFromId(requestId);

			switch (state)
			{
				case ServiceRequestState.Incomplete:
					throw new ServiceRequestStateException("Cannot change the state of a Service Request to Incomplete.");
				case ServiceRequestState.Submitted:
					return SubmitRequest(userId, requestId);
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

		public IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> SubmitRequest(int userId, int requestId)
		{
			IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> request = RequestFromId(requestId);
			if (request.State != ServiceRequestState.Incomplete)
			{
				throw new ServiceRequestStateException(
					   string.Format("Cannot change the state of a Service Request to \"{0}\". " +
									 "Service Request is in the \"{1}\" state and must be in the " +
									 "\"{2}\" state to perform this action.",
									 ServiceRequestState.Submitted, request.State, ServiceRequestState.Incomplete));
			}

			if (UserCanSubmitRequest(userId, requestId))
			{
				using (var context = new PrometheusContext())
				{
					var requestEntity = context.ServiceRequests.Find(requestId);

					//Change state of the entity
					ClearTemporaryFields(requestEntity);
					requestEntity.State = ServiceRequestState.Submitted;
					requestEntity.SubmissionDate = DateTime.UtcNow;

					//Save
					context.Entry(requestEntity).State = EntityState.Modified;
					context.SaveChanges(userId);
					request = ManualMapper.MapServiceRequestToDto(requestEntity);
				}
			}

			return request;
		}

		public bool UserCanSubmitRequest(int userId, int requestId)
		{
			if (_permissionController.UserHasPermission(userId, ServiceRequestSubmission.CanSubmitRequests))
			{
				var request = RequestFromId(requestId);
				return request.RequestedByUserId == userId && request.State == ServiceRequestState.Incomplete;
			}
			return false;
		}

		public IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> CancelRequest(int userId, int requestId, string comments)
		{
			IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> request = RequestFromId(requestId);

			if (request.State != ServiceRequestState.Incomplete && request.State != ServiceRequestState.Submitted)
			{
				throw new ServiceRequestStateException(
					string.Format("Cannot change the state of a Service Request to \"{0}\". " +
								  "Service Request is in the \"{1}\" state and must be in the " +
								  "\"{2}\" or \"{3}\" state to perform this action.",
								  ServiceRequestState.Cancelled, request.State, ServiceRequestState.Incomplete, ServiceRequestState.Submitted));
			}

			if (UserCanCancelRequest(userId, requestId))
			{
				using (var context = new PrometheusContext())
				{
					var requestEntity = context.ServiceRequests.Find(requestId);

					//Change state of the entity
					requestEntity.State = ServiceRequestState.Cancelled;
					requestEntity.CancelledDate = DateTime.UtcNow;

					//Save
					context.Entry(requestEntity).State = EntityState.Modified;
					context.SaveChanges(userId);
					request = ManualMapper.MapServiceRequestToDto(requestEntity);
				}
			}

			return request;
		}

		public bool UserCanCancelRequest(int userId, int requestId)
		{
			if (_permissionController.UserHasPermission(userId, ServiceRequestSubmission.CanSubmitRequests))
			{
				var request = RequestFromId(requestId);

				return request.RequestedByUserId == userId
					   && (request.State == ServiceRequestState.Incomplete /*|| request.State == ServiceRequestState.Submitted */);
			}
			return false;
		}

		public IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> ApproveRequest(int userId, int requestId, ApprovalResult approvalResult, string comments)
		{
			IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> request = RequestFromId(requestId);

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

					//Approve or deny
					if (approvalResult == ApprovalResult.Approved)
					{
						requestEntity.State = ServiceRequestState.Approved;
						requestEntity.ApprovedDate = DateTime.UtcNow;
					}
					else
					{
						requestEntity.State = ServiceRequestState.Denied;
						requestEntity.DeniedDate = DateTime.UtcNow;
					}

					requestEntity.FinalUpfrontPrice = requestEntity.UpfrontPrice;
					requestEntity.FinalMonthlyPrice = requestEntity.MonthlyPrice;

					context.Entry(requestEntity).State = EntityState.Modified;
					context.SaveChanges(userId);
					request = ManualMapper.MapServiceRequestToDto(requestEntity);
				}
			}

			return request;
		}

		public bool UserCanApproveRequest(int userId, int requestId)
		{
			var request = RequestFromId(requestId);

			if (request.State == ServiceRequestState.Submitted)
			{
				if (_permissionController.UserHasPermission(userId, ApproveServiceRequest.ApproveAnyRequests))
				{
					//All submitted requests
					return true;
				}

				if (_permissionController.UserHasPermission(userId, ApproveServiceRequest.ApproveDepartmentRequests))
				{
					//Submitted requests with the same department ID as the approver
					using (var context = new PrometheusContext())
					{
						//DO NOT RETURN RESULT. What if this request was for another department but its a basic request? Proceed to next permission check
						//Will never be null. UserHasPermission will catch that
						if (context.Users.Find(userId).DepartmentId == request.DepartmentId)
							return true;
					}
				}

				if (_permissionController.UserHasPermission(userId, ApproveServiceRequest.ApproveBasicRequests))
				{
					//Basic Requests that the approver submitted
					using (var context = new PrometheusContext())
					{
						return request.RequestedByUserId == userId && context.ServiceRequests.Find(requestId).BasicRequest;
					}
				}
			}
			return false;
		}


		public IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> FulfillRequest(int userId, int requestId, string comments)
		{
			IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> request = RequestFromId(requestId);

			if (request.State != ServiceRequestState.Approved)
			{
				throw new ServiceRequestStateException(
					string.Format("Cannot change the state of a Service Request to \"{0}\". " +
								  "Service Request is in the \"{1}\" state and must be in the " +
								  "\"{2}\" state to perform this action.",
								  ServiceRequestState.Fulfilled, request.State, ServiceRequestState.Approved));
			}

			if (UserCanFulfillRequest(userId, requestId))
			{
				using (var context = new PrometheusContext())
				{
					var requestEntity = context.ServiceRequests.Find(requestId);

					//Change state of the entity
					requestEntity.State = ServiceRequestState.Fulfilled;
					requestEntity.FulfilledDate = DateTime.UtcNow;

					//Save
					context.Entry(requestEntity).State = EntityState.Modified;
					context.SaveChanges(userId);
					request = ManualMapper.MapServiceRequestToDto(requestEntity);
				}
			}

			return request;
		}

		public bool UserCanFulfillRequest(int userId, int requestId)
		{
			return _permissionController.UserHasPermission(userId, FulfillmentAccess.CanFulfill) && RequestFromId(requestId).State == ServiceRequestState.Approved;
		}

		public bool UserCanEditRequest(int userId, int requestId)
		{
			if (_permissionController.UserHasPermission(userId, ServiceRequestSubmission.CanSubmitRequests))
			{
				var request = RequestFromId(requestId);

				//Requestor or Approver and SUBMITTED
				if (request.State == ServiceRequestState.Submitted && (request.RequestedByUserId == userId || request.ApproverUserId == userId))
					return true;

				//Requestor and INCOMPLETE
				if (request.State == ServiceRequestState.Incomplete && request.RequestedByUserId == userId)
					return true;
			}
			return false;
		}

		public IEnumerable<ServiceRequestState> ValidStates(int userId, int requestId)
		{
			var states = new List<ServiceRequestState>();

			if (UserCanApproveRequest(userId, requestId))
			{
				states.Add(ServiceRequestState.Approved);
				states.Add(ServiceRequestState.Denied);
			}

			if (UserCanCancelRequest(userId, requestId))
			{
				states.Add(ServiceRequestState.Cancelled);
			}

			if (UserCanFulfillRequest(userId, requestId))
			{
				states.Add(ServiceRequestState.Fulfilled);
			}

			if (UserCanSubmitRequest(userId, requestId))
			{
				states.Add(ServiceRequestState.Submitted);
			}

			return states;
		}

		private IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> RequestFromId(int requestId)
		{
			IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> request;
			using (var context = new PrometheusContext())
			{
				request = ManualMapper.MapServiceRequestToDto(context.ServiceRequests.Find(requestId));
			}

			if (request == null)
				throw new EntityNotFoundException("", typeof(ServiceRequest), requestId);

			return request;
		}

		private void ClearTemporaryFields(IServiceRequest requestEntity)
		{
			requestEntity.ServiceOptionId = null;
		}
	}
}
