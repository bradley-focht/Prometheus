namespace Common.Dto
{
	public interface IServiceWorkUnitDto : IUserCreatedEntityDto
	{
		string Contact { get; set; }
		int Id { get; set; }
		string Responsibilities { get; set; }
		int ServiceId { get; set; }
		string Name { get; set; }
        string Department { get; set; }
	}
}