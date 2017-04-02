using Common.Enums;

namespace DataService.Models
{
	/// <summary>
	/// Record created when an Approver performs an approval on a Service Request
	/// </summary>
	public interface IApproval : IUserCreatedEntity
	{
		int Id { get; set; }

		/// <summary>
		/// Service Request that the Approval record is for
		/// </summary>
		int ServiceRequestId { get; set; }


		/// <summary>
		/// User performing Approval
		/// </summary>
		int ApproverId { get; set; }

		/// <summary>
		/// User that requested Service Request being Approved
		/// </summary>
		int RequestorId { get; set; }

		/// <summary>
		/// Result of Approval. Approved or Denied
		/// </summary>
		ApprovalResult Result { get; set; }

		/// <summary>
		/// Comments made by Approver about the Approval
		/// </summary>
		string Comments { get; set; }

		ServiceRequest ServiceRequest { get; set; }
	}
}