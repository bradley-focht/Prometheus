namespace Common.Dto
{
	public interface IServiceOptionCategoryTagDto : IServicePackageTag
	{
		int ServiceOptionCategoryId { get; set; }
		IServiceOptionCategoryDto ServiceOptionCategory { get; set; }
	}
}
