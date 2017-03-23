using System;

namespace ServiceFulfillmentEngineWebJob.Api.Models
{
	public interface IServiceRequestOption
	{
		int Id { get; set; }

		int ServiceOptionId { get; set; }
		int ServiceRequestId { get; set; }
		int Quantity { get; set; }
		string Code { get; set; }
		string ServiceOptionName { get; set; }
		int ApproverUserId { get; set; }
		int CreatedByUserId { get; set; }
		DateTime? DateCreated { get; set; }
		DateTime? DateUpdated { get; set; }
		int RequestedByUserId { get; set; }
		int UpdatedByUserId { get; set; }
		bool BasicRequest { get; set; }

		IServiceOption ServiceOption { get; set; }
		IServiceRequest ServiceRequest { get; set; }
	}
}
