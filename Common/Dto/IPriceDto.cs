using Common.Enums.Entities;

namespace Common.Dto
{
	public interface IPriceDto
	{
		int Id { get; set; }
		int ServiceOptionId { get; set; }
		PriceType Type { get; set; }
		decimal Value { get; set; }
		IServiceOptionDto ServiceOption { get; set; }
	}
}