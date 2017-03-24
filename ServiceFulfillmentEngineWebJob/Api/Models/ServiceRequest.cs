using System;
using System.Collections.Generic;
using ServiceFulfillmentEngineWebJob.Api.Models.Enums;

namespace ServiceFulfillmentEngineWebJob.Api.Models
{
	public class ServiceRequest : IServiceRequest
	{
		public int Id { get; set; }
		public int? ServiceOptionId { get; set; }
		public int DepartmentId { get; set; }
		public int ApproverUserId { get; set; }
		public int RequestedByUserId { get; set; }
		public string RequestedForGuids { get; set; }
		public Guid RequestedByGuid { get; set; }
		public string Comments { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime RequestedForDate { get; set; }
		public string Officeuse { get; set; }
		public string Name { get; set; }
		public ServiceRequestState State { get; set; }
		public ServiceRequestAction Action { get; set; }
		public DateTime? SubmissionDate { get; set; }
		public DateTime? ApprovedDate { get; set; }
		public DateTime? DeniedDate { get; set; }
		public DateTime? CancelledDate { get; set; }
		public DateTime? FulfilledDate { get; set; }
		public decimal UpfrontPrice { get; set; }
		public decimal MonthlyPrice { get; set; }
		public decimal FinalUpfrontPrice { get; set; }
		public decimal FinalMonthlyPrice { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		public IEnumerable<ServiceRequestOption> ServiceRequestOptions { get; set; }
		public IEnumerable<ServiceRequestUserInput> ServiceRequestUserInputs { get; set; }
	}
}
