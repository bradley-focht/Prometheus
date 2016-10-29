namespace Common.Dto
{
	public interface IServiceMeasureDto : IUserCreatedEntityDto
	{
		int Id { get; set; }
		string Method { get; set; }
		string Outcome { get; set; }
		int ServiceId { get; set; }
	}
}