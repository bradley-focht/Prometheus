namespace DataService.Models
{
	public interface IServiceOptionCategoryTag : IUserCreatedEntity
	{
		int Id { get; set; }

		int ServiceOptionCategoryId { get; set; }

		int Order { get; set; }

		ServiceOptionCategory ServiceOptionCategory { get; set; }
	}
}