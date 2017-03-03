using System;
using System.Collections.Generic;
using Common.Enums;

namespace Common.Dto
{
	public interface IServiceRequestDto
	{
		int Id { get; set; }

		int? ServiceOptionId { get; set; }

		int ApproverUserId { get; set; }
		int RequestedByUserId { get; set; }

		string Comments { get; set; }
		DateTime? ApprovalDate { get; set; }
		DateTime CreationDate { get; set; }
		DateTime RequestedForDate { get; set; }

		string Officeuse { get; set; }
		string Name { get; set; }
		ServiceRequestState State { get; set; }
		DateTime? SubmissionDate { get; set; }

		DateTime? DateCreated { get; set; }
		DateTime? DateUpdated { get; set; }
		int CreatedByUserId { get; set; }
		int UpdatedByUserId { get; set; }

		ICollection<IServiceRequestOptionDto> ServiceRequestOptions { get; set; }
		ICollection<IServiceRequestUserInputDto> ServiceRequestUserInputs { get; set; }
	}
}