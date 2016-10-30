namespace Common.Dto
{
	public interface IServiceMeasureDto : IUserCreatedEntityDto
	{
		int Id { get; set; }
		int ServiceId { get; set; }

		string Method { get; set; }
		string Outcome { get; set; }
	}
}