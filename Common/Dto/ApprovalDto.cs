using System;
using Common.Enums;

namespace Common.Dto
{
	/// <summary>
	/// Record created when an Approver performs an approval on a Service Request
	/// </summary>
	public class ApprovalDto : IApprovalDto
	{
		//PK
		public int Id { get; set; }

		//FK
		/// <summary>
		/// Service Request that the Approval record is for
		/// </summary>
		public int ServiceRequestId { get; set; }

		#region Fields
		/// <summary>
		/// User performing Approval
		/// </summary>
		public int ApproverId { get; set; }

		/// <summary>
		/// User that requested Service Request being Approved
		/// </summary>
		public int RequestorId { get; set; }

		/// <summary>
		/// Result of Approval. Approved or Denied
		/// </summary>
		public ApprovalResult Result { get; set; }

		/// <summary>
		/// Comments made by Approver about the Approval
		/// </summary>
		public string Comments { get; set; }

		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		#endregion
	}
}
