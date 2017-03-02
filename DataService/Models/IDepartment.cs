namespace DataService.Models
{
	public interface IDepartment : IUserCreatedEntity
	{
		int Id { get; set; }
		string Name { get; set; }
	}
}