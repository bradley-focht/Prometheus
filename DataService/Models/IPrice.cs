using Common.Enums.Entities;

namespace DataService.Models
{
	public interface IPrice : IUserCreatedEntity
	{
		int Id { get; set; }
		ServiceOption ServiceOption { get; set; }
		int ServiceOptionId { get; set; }
		PriceType Type { get; set; }
		decimal Value { get; set; }
	}
}