using Common.Enums;
using System;
using System.Collections.Generic;

namespace Common.Dto
{
	public interface IServiceRequestDto
	{
		int Id { get; set; }
		DateTime? ApprovalDate { get; set; }
		int ApproverUserId { get; set; }
		string Comments { get; set; }
		int CreatedByUserId { get; set; }
		DateTime CreationDate { get; set; }
        DateTime RequestedForDate { get; set; }
        DateTime? DateCreated { get; set; }
		DateTime? DateUpdated { get; set; }
		string Officeuse { get; set; }
		int RequestedByUserId { get; set; }
		ServiceRequestState State { get; set; }
		DateTime? SubmissionDate { get; set; }
		int UpdatedByUserId { get; set; }

		ICollection<IServiceRequestOptionDto> ServiceRequestOptions { get; set; }
	}
}