using System;
using System.Collections.Generic;
using Common.Dto;
using Common.Enums;

namespace RESTAPI.Models
{
	public class Request : IServiceRequestDto
	{
		//ID of the Service Request
		public int Id { get; set; }

		//Name of the Service Request
		public string Name { get; set; }

		public int? ServiceOptionId { get; set; }
		public int DepartmentId { get; set; }
		public int ApproverUserId { get; set; }
		public int RequestedByUserId { get; set; }
		public string Comments { get; set; }
		public DateTime? ApprovalDate { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime RequestedForDate { get; set; }
		public string Officeuse { get; set; }
		public ServiceRequestState State { get; set; }
		public ServiceRequestAction Action { get; set; }
		public DateTime? SubmissionDate { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		public ICollection<IServiceRequestOptionDto> ServiceRequestOptions { get; set; }
		public ICollection<IServiceRequestUserInputDto> ServiceRequestUserInputs { get; set; }
	}
}