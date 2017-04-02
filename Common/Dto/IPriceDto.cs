using Common.Enums.Entities;

namespace Common.Dto
{
	/// <summary>
	/// Price of a Service Option
	/// </summary>
	public interface IPriceDto
	{
		int Id { get; set; }

		/// <summary>
		/// Service Option that the Price is for
		/// </summary>
		int ServiceOptionId { get; set; }

		/// <summary>
		/// Type of price interval
		/// </summary>
		PriceType Type { get; set; }

		/// <summary>
		/// Price value in dollars
		/// </summary>
		decimal Value { get; set; }
		IServiceOptionDto ServiceOption { get; set; }
	}
}