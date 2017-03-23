using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceFulfillmentEngineWebJob.Api.Models
{
	public class ServiceRequestOption : IServiceRequestOption
	{
		public int Id { get; set; }
		public int ServiceOptionId { get; set; }
		public int ServiceRequestId { get; set; }
		public int Quantity { get; set; }
		public string Code { get; set; }
		public string ServiceOptionName { get; set; }
		public int ApproverUserId { get; set; }
		public int CreatedByUserId { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int RequestedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		public bool BasicRequest { get; set; }
		public IServiceOption ServiceOption { get; set; }
		public IServiceRequest ServiceRequest { get; set; }
	}
}
