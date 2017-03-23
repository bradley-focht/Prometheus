namespace ServiceFulfillmentEngineWebJob.EntityFramework.Models
{
	public interface IScript
	{
		string ApplicableCode { get; set; }
		string FileName { get; set; }
		int Id { get; set; }
		string Name { get; set; }
	}
}