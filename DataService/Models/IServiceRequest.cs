using System;
using System.Collections.Generic;
using Common.Enums;

namespace DataService.Models
{
	public interface IServiceRequest : IUserCreatedEntity
	{
		int Id { get; set; }

		/// <summary>
		/// Department used for Approval
		/// </summary>
		int DepartmentId { get; set; }

		/// <summary>
		/// Approver
		/// </summary>
		int ApproverUserId { get; set; }

		/// <summary>
		/// User Making the request, the requestor
		/// </summary>
		int RequestedByUserId { get; set; }

		/// <summary>
		/// Requestees 
		/// </summary>
		string RequestedForGuids { get; set; }

		/// <summary>
		/// Requestor
		/// </summary>
		Guid RequestedByGuid { get; set; }

		/// <summary>
		/// Requestor Comments
		/// </summary>
		string Comments { get; set; }

		/// <summary>
		/// In office use such as billing code
		/// </summary>
		string Officeuse { get; set; }

		/// <summary>
		/// Service Request Name (e.g. an invoice name)
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Date that the Service Request was created
		/// </summary>
		DateTime CreationDate { get; set; }

		/// <summary>
		/// Date that the SR was set to the ServiceRequestState.Submitted State
		/// </summary>
		DateTime? SubmissionDate { get; set; }

		/// <summary>
		/// Date that the SR was set to the ServiceRequestState.Approved State
		/// </summary>
		DateTime? ApprovedDate { get; set; }

		/// <summary>
		/// Date that the SR was set to the ServiceRequestState.Denied State
		/// </summary>
		DateTime? DeniedDate { get; set; }

		/// <summary>
		/// Date that the SR was set to the ServiceRequestState.Cancelled State
		/// </summary>
		DateTime? CancelledDate { get; set; }

		/// <summary>
		/// Date that the SR was set to the ServiceRequestState.Fulfilled State
		/// </summary>
		DateTime? FulfilledDate { get; set; }

		/// <summary>
		/// Date that the Service Request is requested for
		/// </summary>
		DateTime RequestedForDate { get; set; }

		/// <summary>
		/// State that the SR is currently in
		/// </summary>
		ServiceRequestState State { get; set; }

		/// <summary>
		/// If all SROs on the SR are basic
		/// </summary>
		bool BasicRequest { get; }

		/// <summary>
		/// Total monthly price of service request
		/// </summary>
		decimal MonthlyPrice { get; }

		/// <summary>
		/// Total upfront price of service request
		/// </summary>
		decimal UpfrontPrice { get; }

		/// <summary>
		/// Total upfront price of service request at the time of Approval
		/// </summary>
		decimal FinalUpfrontPrice { get; set; }

		/// <summary>
		/// Total monthly price of service request at the time of Approval
		/// </summary>
		decimal FinalMonthlyPrice { get; set; }

		/// <summary>
		/// Action being performed
		/// </summary>
		ServiceRequestAction Action { get; set; }

		/// <summary>
		/// Temporary use to rebuild a Service Package and have their option pre-selected
		/// </summary>
		int? ServiceOptionId { get; set; }

		Department Department { get; set; }
		ICollection<ServiceRequestOption> ServiceRequestOptions { get; set; }
		ICollection<ServiceRequestUserInput> ServiceRequestUserInputs { get; set; }
	}
}