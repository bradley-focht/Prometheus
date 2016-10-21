namespace ServicePortfolio.Dto
{
	public interface IUserDto : IUserCreatedEntityDto
	{
		int Id { get; set; }
		int RoleId { get; set; }
		string Name { get; set; }
		string Password { get; set; }
	}
}