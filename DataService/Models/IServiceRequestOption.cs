namespace DataService.Models
{
	public interface IServiceRequestOption : IUserCreatedEntity
	{
		int Id { get; set; }
		int ServiceId { get; set; }
		Service Service { get; set; }
	}
}