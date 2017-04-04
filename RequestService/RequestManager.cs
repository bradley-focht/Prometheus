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

		/// <summary>
		/// Changes the state of a service request to the state provided if the action is possible.
		/// </summary>
		/// <param name="userId">ID of user performing the state change</param>
		/// <param name="requestId">ID of Service Request to change the state of</param>
		/// <param name="state">State to change the Service Request to</param>
		/// <param name="comments">Optional: Comments tied to the state change if applicable</param>
		/// <returns>Service Request after state change is attempted</returns>
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

		/// <summary>
		/// Changes the state of a service request to Submitted if the action is possible.
		/// </summary>
		/// <param name="userId">ID of user Submitting the request</param>
		/// <param name="requestId">ID of Service Request to Submit</param>
		/// <returns>Service Request after Submition is attempted</returns>
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

		/// <summary>
		/// Determines if the User with the ID supplied can Submit the request with the ID provided
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="requestId"></param>
		/// <returns></returns>
		public bool UserCanSubmitRequest(int userId, int requestId)
		{
			if (_permissionController.UserHasPermission(userId, ServiceRequestSubmission.CanSubmitRequests))
			{
				var request = RequestFromId(requestId);
				return request.RequestedByUserId == userId && request.State == ServiceRequestState.Incomplete;
			}
			return false;
		}

		/// <summary>
		/// Changes the state of a service request to Cancelled if the action is possible.
		/// </summary>
		/// <param name="userId">ID of user Cancelling the request</param>
		/// <param name="requestId">ID of Service Request to Cancel</param>
		/// <param name="comments">Optional: Comments tied to the submission if applicable</param>
		/// <returns>Service Request after Cancellation is attempted</returns>
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

		/// <summary>
		/// Determines if the User with the ID supplied can Cancel the request with the ID provided
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="requestId"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Changes the state of a service request to the result of the Approval if the action is possible.
		/// </summary>
		/// <param name="userId">ID of user Approving the request</param>
		/// <param name="requestId">ID of Service Request to Approve</param>
		/// <param name="approvalResult">Result of the approval transaction (approved or denied)</param>
		/// <param name="comments">Optional: Comments tied to the Approval if applicable</param>
		/// <returns>Service Request after Approval is attempted</returns>
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

		/// <summary>
		/// Determines if the User with the ID supplied can perform an Approval on the request with the ID provided
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="requestId"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Changes the state of a service request to Fulfilled if the action is possible.
		/// </summary>
		/// <param name="userId">ID of user Fulfilling the request</param>
		/// <param name="requestId">ID of Service Request to Fulfill</param>
		/// <param name="comments">Optional: Comments tied to the Fulfillment if applicable</param>
		/// <returns>Service Request after Fulfillment is attempted</returns>
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

		/// <summary>
		/// Determines if the User with the ID supplied can Fulfill the request with the ID provided
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="requestId"></param>
		/// <returns></returns>
		public bool UserCanFulfillRequest(int userId, int requestId)
		{
			return _permissionController.UserHasPermission(userId, FulfillmentAccess.CanFulfill) && RequestFromId(requestId).State == ServiceRequestState.Approved;
		}

		/// <summary>
		/// Determines if a user can Edit a request
		/// </summary>
		/// <param name="userId">ID of user editing request</param>
		/// <param name="requestId">ID of request to be edited</param>
		/// <returns></returns>
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

		/// <summary>
		/// Returns a list of all states that a user can change a service request to
		/// </summary>
		/// <param name="userId">ID of user changing states</param>
		/// <param name="requestId">ID of request to be changed</param>
		/// <returns></returns>
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
