using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Common.Dto;
using Common.Enums;

namespace RESTAPI.Models
{
	[DataContract]
	public class Request : IServiceRequestDto<ServiceRequestOptionDto, ServiceRequestUserInputDto>
	{
		public Request() { }

		//ID of the Service Request
		[DataMember]
		public int Id { get; set; }

		//Name of the Service Request
		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public int? ServiceOptionId { get; set; }

		[DataMember]
		public int DepartmentId { get; set; }

		[DataMember]
		public int ApproverUserId { get; set; }

		[DataMember]
		public int RequestedByUserId { get; set; }

		[DataMember]
		public string RequestedForGuids { get; set; }

		[DataMember]
		public Guid RequestedByGuid { get; set; }

		[DataMember]
		public string Comments { get; set; }

		[DataMember]
		public DateTime? ApprovalDate { get; set; }

		[DataMember]
		public DateTime CreationDate { get; set; }

		[DataMember]
		public DateTime RequestedForDate { get; set; }

		[DataMember]
		public string Officeuse { get; set; }

		[DataMember]
		public ServiceRequestState State { get; set; }

		[DataMember]
		public ServiceRequestAction Action { get; set; }

		[DataMember]
		public DateTime? SubmissionDate { get; set; }

		[DataMember]
		public DateTime? ApprovedDate { get; set; }

		[DataMember]
		public DateTime? DeniedDate { get; set; }

		[DataMember]
		public DateTime? CancelledDate { get; set; }

		[DataMember]
		public DateTime? FulfilledDate { get; set; }

		[DataMember]
		public decimal FinalMonthlyPrice { get; set; }

		[DataMember]
		public DateTime? DateCreated { get; set; }

		[DataMember]
		public DateTime? DateUpdated { get; set; }

		[DataMember]
		public int CreatedByUserId { get; set; }

		[DataMember]
		public int UpdatedByUserId { get; set; }

		[DataMember]
		public decimal UpfrontPrice { get; set; }

		[DataMember]
		public decimal MonthlyPrice { get; set; }

		[DataMember]
		public decimal FinalUpfrontPrice { get; set; }

		[DataMember]
		public ICollection<ServiceRequestOptionDto> ServiceRequestOptions { get; set; }

		[DataMember]
		public ICollection<ServiceRequestUserInputDto> ServiceRequestUserInputs { get; set; }

		internal Request(IServiceRequestDto<IServiceRequestOptionDto, IServiceRequestUserInputDto> src)
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

			ServiceRequestOptions = new List<ServiceRequestOptionDto>();
			foreach (var sro in src.ServiceRequestOptions)
			{
				ServiceRequestOptions.Add((ServiceRequestOptionDto)sro);
			}

			ServiceRequestUserInputs = new List<ServiceRequestUserInputDto>();
			foreach (var sro in src.ServiceRequestUserInputs)
			{
				ServiceRequestUserInputs.Add((ServiceRequestUserInputDto)sro);
			}
		}
	}
}