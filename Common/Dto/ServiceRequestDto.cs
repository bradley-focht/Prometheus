using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Enums;

namespace Common.Dto
{
	public class ServiceRequestDto : IServiceRequestDto
	{
		public int Id { get; set; }

		/// <summary>
		/// Temporary use to rebuild a Service Package and have their option pre-selected
		/// </summary>
		public int? ServiceOptionId { get; set; }

		/// <summary>
		/// Approval Department
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
		public DateTime CreationDate { get; set; }
		[Required(ErrorMessage = "Requested for date is required")]
		public DateTime RequestedForDate { get; set; }

		/// <summary>
		/// Action being performed
		/// </summary>
		public ServiceRequestAction Action { get; set; }
		public DateTime? SubmissionDate { get; set; }
		public DateTime? ApprovedDate { get; set; } /* end here */
		public DateTime? DeniedDate { get; set; }
		public DateTime? CancelledDate { get; set; }
		public DateTime? FulfilledDate { get; set; }
		public int ApproverUserId { get; set; }
		public ServiceRequestState State { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public decimal UpfrontPrice { get; set; }

		public decimal MonthlyPrice { get; set; }
		public decimal FinalUpfrontPrice { get; set; }
		public decimal FinalMonthlyPrice { get; set; }

		public virtual ICollection<IServiceRequestOptionDto> ServiceRequestOptions { get; set; }
		public virtual ICollection<IServiceRequestUserInputDto> ServiceRequestUserInputs { get; set; }
	}
}
