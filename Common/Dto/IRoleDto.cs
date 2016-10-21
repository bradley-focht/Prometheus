namespace ServicePortfolio.Dto
{
	public interface IRoleDto : IUserCreatedEntityDto
	{
		int Id { get; set; }
		string Name { get; set; }
	}
}