namespace DataService.Models
{
	/// <summary>
	/// Entity for mapping the many to many relationship between Service Packages and Service Option Categories
	/// </summary>
	public interface IServiceOptionCategoryTag : IServicePackageTag
	{
		/// <summary>
		/// ID of Service Option Category linked
		/// </summary>
		int ServiceOptionCategoryId { get; set; }
		ServiceOptionCategory ServiceOptionCategory { get; set; }
	}
}