using System.Collections.Generic;
using Common.Dto;
using Common.Enums;

namespace RequestService
{
	public interface IRequestManager
	{
		/// <summary>
		/// Changes the state of a service request to the state provided if the action is possible.
		/// </summary>
		/// <param name="userId">ID of user performing the state change</param>
		/// <param name="requestId">ID of Service Request to change the state of</param>
		/// <param name="state">State to change the Service Request to</param>
		/// <param name="comments">Optional: Comments tied to the state change if applicable</param>
		/// <returns>Service Request after state change is attempted</returns>
		IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> ChangeRequestState(int userId, int requestId, ServiceRequestState state, string comments = null);

		/// <summary>
		/// Changes the state of a service request to Submitted if the action is possible.
		/// </summary>
		/// <param name="userId">ID of user Submitting the request</param>
		/// <param name="requestId">ID of Service Request to Submit</param>
		/// <returns>Service Request after Submition is attempted</returns>
		IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> SubmitRequest(int userId, int requestId);

		bool UserCanSubmitRequest(int userId, int requestId);

		/// <summary>
		/// Changes the state of a service request to Cancelled if the action is possible.
		/// </summary>
		/// <param name="userId">ID of user Cancelling the request</param>
		/// <param name="requestId">ID of Service Request to Cancel</param>
		/// <param name="comments">Optional: Comments tied to the submission if applicable</param>
		/// <returns>Service Request after Cancellation is attempted</returns>
		IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> CancelRequest(int userId, int requestId, string comments);

		bool UserCanCancelRequest(int userId, int requestId);

		/// <summary>
		/// Changes the state of a service request to the result of the Approval if the action is possible.
		/// </summary>
		/// <param name="userId">ID of user Approving the request</param>
		/// <param name="requestId">ID of Service Request to Approve</param>
		/// <param name="approval">Result of the approval transaction (approved or denied)</param>
		/// <param name="comments">Optional: Comments tied to the Approval if applicable</param>
		/// <returns>Service Request after Approval is attempted</returns>
		IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> ApproveRequest(int userId, int requestId, ApprovalResult approval, string comments);

		bool UserCanApproveRequest(int userId, int requestId);

		/// <summary>
		/// Changes the state of a service request to Fulfilled if the action is possible.
		/// </summary>
		/// <param name="userId">ID of user Fulfilling the request</param>
		/// <param name="requestId">ID of Service Request to Fulfill</param>
		/// <param name="comments">Optional: Comments tied to the Fulfillment if applicable</param>
		/// <returns>Service Request after Fulfillment is attempted</returns>
		IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> FulfillRequest(int userId, int requestId, string comments);

		bool UserCanFulfillRequest(int userId, int requestId);

		/// <summary>
		/// Determines if a user can Edit a request
		/// </summary>
		/// <param name="userId">ID of user editing request</param>
		/// <param name="requestId">ID of request to be edited</param>
		/// <returns></returns>
		bool UserCanEditRequest(int userId, int requestId);

		/// <summary>
		/// Returns a list of all states that a user can change a service request to
		/// </summary>
		/// <param name="userId">ID of user changing states</param>
		/// <param name="requestId">ID of request to be changed</param>
		/// <returns></returns>
		IEnumerable<ServiceRequestState> ValidStates(int userId, int requestId);
	}
}