namespace DataService.Models
{
	public interface IServiceOptionCategoryTag : IServicePackageTag
	{
		int ServiceOptionCategoryId { get; set; }
		ServiceOptionCategory ServiceOptionCategory { get; set; }
	}
}