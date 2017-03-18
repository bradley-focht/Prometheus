using System;
using System.Collections.Generic;
using Common.Enums;

namespace Common.Dto
{
	public interface IServiceRequestDto
	{
		int Id { get; set; }

		int? ServiceOptionId { get; set; }

		int DepartmentId { get; set; }

		int ApproverUserId { get; set; }
		int RequestedByUserId { get; set; }
		string RequestedForGuids { get; set; }
		Guid RequestedByGuid { get; set; }
		string Comments { get; set; }
		DateTime CreationDate { get; set; }
		DateTime RequestedForDate { get; set; }

		string Officeuse { get; set; }
		string Name { get; set; }
		ServiceRequestState State { get; set; }
		ServiceRequestAction Action { get; set; }

		//State Dates
		DateTime? SubmissionDate { get; set; }
		DateTime? ApprovedDate { get; set; }
		DateTime? DeniedDate { get; set; }
		DateTime? CancelledDate { get; set; }
		DateTime? FulfilledDate { get; set; }

		decimal UpfrontPrice { get; set; }
		decimal MonthlyPrice { get; set; }

		DateTime? DateCreated { get; set; }
		DateTime? DateUpdated { get; set; }
		int CreatedByUserId { get; set; }
		int UpdatedByUserId { get; set; }

		ICollection<IServiceRequestOptionDto> ServiceRequestOptions { get; set; }
		ICollection<IServiceRequestUserInputDto> ServiceRequestUserInputs { get; set; }
	}
}