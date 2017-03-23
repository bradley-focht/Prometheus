using ServiceFulfillmentEngineWebJob.Api.Models.Enums;

namespace ServiceFulfillmentEngineWebJob.EntityFramework.Models
{
	public interface IScript
	{
		string ApplicableCode { get; set; }
		string FileName { get; set; }
		int Id { get; set; }
		ServiceRequestAction Action { get; set; }
		string Name { get; set; }
	}
}