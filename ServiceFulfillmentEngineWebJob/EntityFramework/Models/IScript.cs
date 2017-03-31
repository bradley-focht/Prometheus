using ServiceFulfillmentEngineWebJob.Api.Models.Enums;
using ServiceFulfillmentEngineWebJob.EntityFramework.Models.Enums;

namespace ServiceFulfillmentEngineWebJob.EntityFramework.Models
{
	public interface IScript
	{
		string ApplicableCode { get; set; }
		string FileName { get; set; }
		int Id { get; set; }
		ServiceRequestAction Action { get; set; }
		string Name { get; set; }
		Priority Priority { get; set; }
	}
}