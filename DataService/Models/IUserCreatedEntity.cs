namespace DataService.Models
{
	public interface IUserCreatedEntity : ICreatedEntity
	{
		int CreatedByUserId { get; set; }
		int UpdatedByUserId { get; set; }
	}
}
