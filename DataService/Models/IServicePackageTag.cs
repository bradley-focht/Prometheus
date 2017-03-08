namespace DataService.Models
{
	public interface IServicePackageTag : IUserCreatedEntity
	{
		int Id { get; set; }
		int Order { get; set; }
		int ServiceRequestPackageId { get; set; }
		ServiceRequestPackage ServiceRequestPackage { get; set; }
	}
}