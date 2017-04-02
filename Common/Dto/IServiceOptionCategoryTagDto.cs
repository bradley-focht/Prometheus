namespace Common.Dto
{
	/// <summary>
	/// Entity for mapping the many to many relationship between Service Packages and Service Option Categories
	/// </summary>
	public interface IServiceOptionCategoryTagDto : IServicePackageTag
	{
		/// <summary>
		/// ID of Service Option Category linked
		/// </summary>
		int ServiceOptionCategoryId { get; set; }
		IServiceOptionCategoryDto ServiceOptionCategory { get; set; }
	}
}
