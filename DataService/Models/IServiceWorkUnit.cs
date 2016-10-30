namespace DataService.Models
{
	public interface IServiceWorkUnit : IUserCreatedEntity
	{
		int Id { get; set; }
		int ServiceId { get; set; }

		string Contact { get; set; }
		string Responsibilities { get; set; }
		string WorkUnit { get; set; }

		IService Service { get; set; }
	}
}