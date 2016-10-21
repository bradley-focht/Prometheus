namespace ServicePortfolio.Dto
{
	public interface IUserCreatedEntityDto : ICreatedEntityDto
	{
		int CreatedByUserId { get; set; }
		int UpdatedByUserId { get; set; }
	}
}
