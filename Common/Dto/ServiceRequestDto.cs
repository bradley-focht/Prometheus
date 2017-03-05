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

		[Required(ErrorMessage = "Approval Department is required")]
		public int DepartmentId { get; set; }

		/// <summary>
		/// User Making the request, the requestor
		/// </summary>
		public int RequestedByUserId { get; set; }
		public string Comments { get; set; }    /*fields added by brad */

		/// <summary>
		/// In office use such as billing code
		/// </summary>
		public string Officeuse { get; set; }
		public string Name { get; set; }
		public DateTime CreationDate { get; set; }
		[Required(ErrorMessage = "Requested for date is required")]
		public DateTime RequestedForDate { get; set; }

		/// <summary>
		/// Action being performed
		/// </summary>
		public ServiceRequestAction Action { get; set; }
		public DateTime? SubmissionDate { get; set; }
		public DateTime? ApprovalDate { get; set; } /* end here */
		public int ApproverUserId { get; set; }
		public ServiceRequestState State { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		public virtual ICollection<IServiceRequestOptionDto> ServiceRequestOptions { get; set; }
		public virtual ICollection<IServiceRequestUserInputDto> ServiceRequestUserInputs { get; set; }
	}
}
