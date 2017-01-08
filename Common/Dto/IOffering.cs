namespace Common.Dto
{
	public interface IOffering : ICatalogPublishable
	{
		int ServiceId { get; set; }
		string Features { get; set; }
		string Benefits { get; set; }
		string Support { get; set; }
		string Description { get; set; }
	}
}
