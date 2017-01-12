namespace Common.Dto
{
	public interface IUserDto : IUserCreatedEntityDto
	{
		int Id { get; set; }
		string Name { get; set; }
		string Password { get; set; }
	}
}