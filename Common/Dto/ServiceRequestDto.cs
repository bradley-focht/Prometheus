using Common.Enums;
using System;
using System.Collections.Generic;

namespace Common.Dto
{
	public class ServiceRequestDto : IServiceRequestDto
	{
		public int Id { get; set; }

		public int RequestedByUserId { get; set; }
		public string Comments { get; set; }    /*fields added by brad */
		public string Officeuse { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime? SubmissionDate { get; set; }
		public DateTime? ApprovalDate { get; set; } /* end here */
		public int ApproverUserId { get; set; }
		public ServiceRequestState State { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public virtual ICollection<IServiceRequestOptionDto> ServiceRequestOptions { get; set; }
	}
}
