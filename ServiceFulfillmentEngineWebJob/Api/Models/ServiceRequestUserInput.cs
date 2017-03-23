using ServiceFulfillmentEngineWebJob.Api.Models.Enums;

namespace ServiceFulfillmentEngineWebJob.Api.Models
{
	public class ServiceRequestUserInput : IServiceRequestUserInput
	{
		public int Id { get; set; }
		public int ServiceRequestId { get; set; }
		public int InputId { get; set; }
		public UserInputType UserInputType { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }
	}
}
