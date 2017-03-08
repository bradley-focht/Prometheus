namespace Common.Dto
{
	public interface IServiceOptionCategoryTagDto
	{
		int Id { get; set; }

		int ServiceOptionCategoryId { get; set; }
		int ServiceRequestPackageId { get; set; }

		int Order { get; set; }

		IServiceOptionCategoryDto ServiceOptionCategory { get; set; }
		IServiceRequestPackageDto ServiceRequestPackage { get; set; }
	}
}