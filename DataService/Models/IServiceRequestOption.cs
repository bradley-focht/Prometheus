namespace DataService.Models
{
	public interface IServiceRequestOption : IUserCreatedEntity
	{
		int Id { get; set; }
		int ServiceId { get; set; }
		//TODO: Sean Add service ID
	}
}