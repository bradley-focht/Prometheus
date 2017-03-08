namespace Common.Dto
{
	public interface IServicePackageTag : IUserCreatedEntityDto
	{
		int Id { get; set; }
		int Order { get; set; }
		int ServiceRequestPackageId { get; set; }
		IServiceRequestPackageDto ServiceRequestPackage { get; set; }
	}
}
