using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Common.Dto;
using Common.Enums;

namespace RESTAPI.Models
{
	/// <summary>
	/// Model used for serializing and deserializing Requests for the REST API
	/// </summary>
	[DataContract]
	public class Request : IServiceRequestDto<ServiceRequestOptionDto, ServiceRequestUserInputDto>
	{
		public Request() { }

		//ID of the Service Request
		[DataMember]
		public int Id { get; set; }

		/// <summary>
		/// Service Request Name (e.g. an invoice name)
		/// </summary>
		[DataMember]
		public string Name { get; set; }

		/// <summary>
		/// Temporary use to rebuild a Service Package and have their option pre-selected
		/// </summary>
		[DataMember]
		public int? ServiceOptionId { get; set; }

		/// <summary>
		/// Department used for Approval
		/// </summary>
		[DataMember]
		public int DepartmentId { get; set; }

		/// <summary>
		/// Approver
		/// </summary>
		[DataMember]
		public int ApproverUserId { get; set; }

		/// <summary>
		/// User Making the request, the requestor
		/// </summary>
		[DataMember]
		public int RequestedByUserId { get; set; }

		/// <summary>
		/// Requestees 
		/// </summary>
		[DataMember]
		public string RequestedForGuids { get; set; }

		/// <summary>
		/// Requestor
		/// </summary>
		[DataMember]
		public Guid RequestedByGuid { get; set; }

		/// <summary>
		/// Requestor Comments
		/// </summary>
		[DataMember]
		public string Comments { get; set; }

		/// <summary>
		/// Date that the Service Request was created
		/// </summary>
		[DataMember]
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// Date that the Service Request is requested for
		/// </summary>
		[DataMember]
		public DateTime RequestedForDate { get; set; }

		/// <summary>
		/// In office use such as billing code
		/// </summary>
		[DataMember]
		public string Officeuse { get; set; }

		/// <summary>
		/// State that the SR is currently in
		/// </summary>
		[DataMember]
		public ServiceRequestState State { get; set; }

		/// <summary>
		/// Action being performed
		/// </summary>
		[DataMember]
		public ServiceRequestAction Action { get; set; }

		/// <summary>
		/// Date that the SR was set to the ServiceRequestState.Submitted State
		/// </summary>
		[DataMember]
		public DateTime? SubmissionDate { get; set; }

		/// <summary>
		/// Date that the SR was set to the ServiceRequestState.Approved State
		/// </summary>
		[DataMember]
		public DateTime? ApprovedDate { get; set; }

		/// <summary>
		/// Date that the SR was set to the ServiceRequestState.Denied State
		/// </summary>
		[DataMember]
		public DateTime? DeniedDate { get; set; }

		/// <summary>
		/// Date that the SR was set to the ServiceRequestState.Cancelled State
		/// </summary>
		[DataMember]
		public DateTime? CancelledDate { get; set; }

		/// <summary>
		/// Date that the SR was set to the ServiceRequestState.Fulfilled State
		/// </summary>
		[DataMember]
		public DateTime? FulfilledDate { get; set; }

		/// <summary>
		/// Total monthly price of service request at the time of Approval
		/// </summary>
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

		/// <summary>
		/// Total upfront price of service request
		/// </summary>
		[DataMember]
		public decimal UpfrontPrice { get; set; }

		/// <summary>
		/// Total monthly price of service request
		/// </summary>
		[DataMember]
		public decimal MonthlyPrice { get; set; }

		/// <summary>
		/// Total upfront price of service request at the time of Approval
		/// </summary>
		[DataMember]
		public decimal FinalUpfrontPrice { get; set; }

		[DataMember]
		public ICollection<ServiceRequestOptionDto> ServiceRequestOptions { get; set; }

		[DataMember]
		public ICollection<ServiceRequestUserInputDto> ServiceRequestUserInputs { get; set; }

		/// <summary>
		/// Creates a Request object copy from a IServiceRequestDto
		/// </summary>
		/// <param name="src"></param>
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