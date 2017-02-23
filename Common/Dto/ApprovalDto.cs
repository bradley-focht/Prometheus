using System;
using Common.Enums;

namespace Common.Dto
{
	public class ApprovalDto : IApprovalDto
	{
		//PK
		public int Id { get; set; }

		//FK
		public int ServiceRequestId { get; set; }

		#region Fields
		public int ApproverId { get; set; }
		public int RequestorId { get; set; }
		public ApprovalResult Result { get; set; }
		public string Comments { get; set; }

		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		#endregion
	}
}
