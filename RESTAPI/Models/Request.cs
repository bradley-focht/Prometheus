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
		public string RequestedForGuids { get; set; }
		public Guid RequestedByGuid { get; set; }
		public string Comments { get; set; }
		public DateTime? ApprovalDate { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime RequestedForDate { get; set; }
		public string Officeuse { get; set; }
		public ServiceRequestState State { get; set; }
		public ServiceRequestAction Action { get; set; }
		public DateTime? SubmissionDate { get; set; }
		public DateTime? ApprovedDate { get; set; }
		public DateTime? DeniedDate { get; set; }
		public DateTime? CancelledDate { get; set; }
		public DateTime? FulfilledDate { get; set; }
		public decimal FinalMonthlyPrice { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		public decimal UpfrontPrice { get; set; }
		public decimal MonthlyPrice { get; set; }
		public decimal FinalUpfrontPrice { get; set; }
		public ICollection<IServiceRequestOptionDto> ServiceRequestOptions { get; set; }
		public ICollection<IServiceRequestUserInputDto> ServiceRequestUserInputs { get; set; }

		internal Request(IServiceRequestDto src)
		{
			Id = src.Id;
			Name = src.Name;
			Action = src.Action;
			State = src.State;
			ApprovedDate = src.ApprovedDate;
			ApproverUserId = src.ApproverUserId;
			Comments = src.Comments;
			CreationDate = src.CreationDate;
			Officeuse = src.Officeuse;
			RequestedByUserId = src.RequestedByUserId;
			RequestedForGuids = src.RequestedForGuids;
			RequestedByGuid = src.RequestedByGuid;
			SubmissionDate = src.SubmissionDate;
			RequestedForDate = src.RequestedForDate;
			ServiceOptionId = src.ServiceOptionId;
			DepartmentId = src.DepartmentId;
			CancelledDate = src.CancelledDate;
			DeniedDate = src.DeniedDate;
			FulfilledDate = src.FulfilledDate;
			FinalMonthlyPrice = src.FinalMonthlyPrice;
			FinalUpfrontPrice = src.FinalUpfrontPrice;
		}
	}
}