namespace Common.Dto
{
	public class ServiceOptionCategoryTagDto : IServiceOptionCategoryTagDto
	{
		public int Id { get; set; }

		public int Order { get; set; }
		public int ServiceOptionCategoryId { get; set; }
		public int ServiceRequestPackageId { get; set; }

		public IServiceOptionCategoryDto ServiceOptionCategory { get; set; }
		public IServiceRequestPackageDto ServiceRequestPackage { get; set; }
	}
}
