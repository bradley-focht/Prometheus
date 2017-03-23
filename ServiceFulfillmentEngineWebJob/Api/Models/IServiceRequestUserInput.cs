using ServiceFulfillmentEngineWebJob.Api.Models.Enums;

namespace ServiceFulfillmentEngineWebJob.Api.Models
{
	public interface IServiceRequestUserInput
	{
		int Id { get; set; }
		int ServiceRequestId { get; set; }
		int InputId { get; set; }
		UserInputType UserInputType { get; set; }
		string Name { get; set; }
		string Value { get; set; }
	}
}