namespace DataService.Models
{
	public interface IServiceRequestOptionInput
	{
		int ServiceRequestOptionId { get; set; }

		string Value { get; set; }

		ServiceRequestOption ServiceRequestOption { get; set; }
	}
}