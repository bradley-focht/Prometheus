namespace Common.Dto
{
	public interface IDepartmentDto : IUserCreatedEntityDto
	{
		int Id { get; set; }
		string Name { get; set; }
	}
}