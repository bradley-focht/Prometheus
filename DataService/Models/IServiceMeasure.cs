namespace DataService.Models
{
	public interface IServiceMeasure : IUserCreatedEntity
	{
		int Id { get; set; }
		int ServiceId { get; set; }
		string Method { get; set; }
		string Outcome { get; set; }

		IService Service { get; set; }
	}
}