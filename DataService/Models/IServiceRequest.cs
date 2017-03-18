using System;
using System.Collections.Generic;
using Common.Enums;

namespace DataService.Models
{
	public interface IServiceRequest : IUserCreatedEntity
	{
		int Id { get; set; }

		int DepartmentId { get; set; }
		int ApproverUserId { get; set; }
		int RequestedByUserId { get; set; }
		string RequestedForGuids { get; set; }
		Guid RequestedByGuid { get; set; }
		string Comments { get; set; }
		string Officeuse { get; set; }
		string Name { get; set; }
		DateTime CreationDate { get; set; }

		//State Dates
		DateTime? SubmissionDate { get; set; }
		DateTime? ApprovedDate { get; set; }
		DateTime? DeniedDate { get; set; }
		DateTime? CancelledDate { get; set; }
		DateTime? FulfilledDate { get; set; }
		DateTime RequestedForDate { get; set; }

		ServiceRequestState State { get; set; }

		//Calculated fields
		bool BasicRequest { get; }
		decimal MonthlyPrice { get; }
		decimal UpfrontPrice { get; }

		ServiceRequestAction Action { get; set; }
		int? ServiceOptionId { get; set; }

		Department Department { get; set; }
		ICollection<ServiceRequestOption> ServiceRequestOptions { get; set; }
		ICollection<ServiceRequestUserInput> ServiceRequestUserInputs { get; set; }
	}
}