namespace DataService.Models
{
	public interface IServiceMeasure : IUserCreatedEntity
	{
		int Id { get; set; }
		string Method { get; set; }
		string Outcome { get; set; }
		int ServiceId { get; set; }
	}
}