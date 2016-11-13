namespace DataService.Models
{
	public interface IUser : IUserCreatedEntity
	{
		int Id { get; set; }
		int RoleId { get; set; }

		string Name { get; set; }
		string Password { get; set; }

		Role Role { get; set; }
	}
}