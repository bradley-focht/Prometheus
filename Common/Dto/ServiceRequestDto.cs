using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Enums;

namespace Common.Dto
{
	/// <summary>
	/// A request made by a User to order a collection of Service Options from a Catalog
	/// </summary>
	public class ServiceRequestDto : IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto>
	{
		public int Id { get; set; }

		/// <summary>
		/// Temporary use to rebuild a Service Package and have their option pre-selected
		/// </summary>
		public int? ServiceOptionId { get; set; }

		/// <summary>
		/// Department used for Approval
		/// </summary>
		[Required(ErrorMessage = "Approval Department is required")]
		public int DepartmentId { get; set; }

		/// <summary>
		/// User Making the request, the requestor
		/// </summary>
		public int RequestedByUserId { get; set; }

		/// <summary>
		/// Requestees 
		/// </summary>
		public string RequestedForGuids { get; set; }

		/// <summary>
		/// Requestor
		/// </summary>
		public Guid RequestedByGuid { get; set; }

		/// <summary>
		/// Requestor Comments
		/// </summary>
		public string Comments { get; set; }

		/// <summary>
		/// In office use such as billing code
		/// </summary>
		public string Officeuse { get; set; }

		/// <summary>
		/// Service Request Name (e.g. an invoice name)
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Date that the Service Request was created
		/// </summary>
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// Date that the Service Request is requested for
		/// </summary>
		[Required(ErrorMessage = "Requested for date is required")]
		public DateTime RequestedForDate { get; set; }

		/// <summary>
		/// Action being performed
		/// </summary>
		public ServiceRequestAction Action { get; set; }

		/// <summary>
		/// Date that the SR was set to the ServiceRequestState.Submitted State
		/// </summary>
		public DateTime? SubmissionDate { get; set; }

		/// <summary>
		/// Date that the SR was set to the ServiceRequestState.Approved State
		/// </summary>
		public DateTime? ApprovedDate { get; set; }

		/// <summary>
		/// Date that the SR was set to the ServiceRequestState.Denied State
		/// </summary>
		public DateTime? DeniedDate { get; set; }

		/// <summary>
		/// Date that the SR was set to the ServiceRequestState.Cancelled State
		/// </summary>
		public DateTime? CancelledDate { get; set; }

		/// <summary>
		/// Date that the SR was set to the ServiceRequestState.Fulfilled State
		/// </summary>
		public DateTime? FulfilledDate { get; set; }

		/// <summary>
		/// Approver
		/// </summary>
		public int ApproverUserId { get; set; }

		/// <summary>
		/// State that the SR is currently in
		/// </summary>
		public ServiceRequestState State { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		/// <summary>
		/// Total upfront price of service request
		/// </summary>
		public decimal UpfrontPrice { get; set; }

		/// <summary>
		/// Total monthly price of service request
		/// </summary>
		public decimal MonthlyPrice { get; set; }

		/// <summary>
		/// Total upfront price of service request at the time of Approval
		/// </summary>
		public decimal FinalUpfrontPrice { get; set; }

		/// <summary>
		/// Total monthly price of service request at the time of Approval
		/// </summary>
		public decimal FinalMonthlyPrice { get; set; }

		public virtual ICollection<IServiceRequestOptionDto> ServiceRequestOptions { get; set; }
		public virtual ICollection<IServiceRequestUserInputDto> ServiceRequestUserInputs { get; set; }
	}
}
