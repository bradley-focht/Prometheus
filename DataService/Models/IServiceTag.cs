namespace DataService.Models
{
	public interface IServiceTag : IServicePackageTag
	{
		int ServiceId { get; set; }
		Service Service { get; set; }
	}
}