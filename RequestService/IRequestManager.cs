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
		IServiceRequestDto ChangeRequestState(int userId, int requestId, ServiceRequestState state, string comments = null);

		/// <summary>
		/// Changes the state of a service request to Submitted if the action is possible.
		/// </summary>
		/// <param name="userId">ID of user Submitting the request</param>
		/// <param name="requestId">ID of Service Request to Submit</param>
		/// <param name="comments">Optional: Comments tied to the submission if applicable</param>
		/// <returns>Service Request after Submition is attempted</returns>
		IServiceRequestDto SubmitRequest(int userId, int requestId, string comments);

		/// <summary>
		/// Changes the state of a service request to Cancelled if the action is possible.
		/// </summary>
		/// <param name="userId">ID of user Cancelling the request</param>
		/// <param name="requestId">ID of Service Request to Cancel</param>
		/// <param name="comments">Optional: Comments tied to the submission if applicable</param>
		/// <returns>Service Request after Cancellation is attempted</returns>
		IServiceRequestDto CancelRequest(int userId, int requestId, string comments);

		/// <summary>
		/// Changes the state of a service request to the result of the Approval if the action is possible.
		/// </summary>
		/// <param name="userId">ID of user Approving the request</param>
		/// <param name="requestId">ID of Service Request to Approve</param>
		/// <param name="approval">Result of the approval transaction (approved or denied)</param>
		/// <param name="comments">Optional: Comments tied to the Approval if applicable</param>
		/// <returns>Service Request after Approval is attempted</returns>
		IServiceRequestDto ApproveRequest(int userId, int requestId, ApprovalResult approval, string comments);

		/// <summary>
		/// Changes the state of a service request to Fulfilled if the action is possible.
		/// </summary>
		/// <param name="userId">ID of user Fulfilling the request</param>
		/// <param name="requestId">ID of Service Request to Fulfill</param>
		/// <param name="comments">Optional: Comments tied to the Fulfillment if applicable</param>
		/// <returns>Service Request after Fulfillment is attempted</returns>
		IServiceRequestDto FulfillRequest(int userId, int requestId, string comments);
	}
}