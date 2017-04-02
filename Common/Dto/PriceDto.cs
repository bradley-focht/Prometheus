using Common.Enums.Entities;

namespace Common.Dto
{
	/// <summary>
	/// Price of a Service Option
	/// </summary>
	public class PriceDto : IPriceDto
	{
		//PK
		public int Id { get; set; }

		//FK
		/// <summary>
		/// Service Option that the Price is for
		/// </summary>
		public int ServiceOptionId { get; set; }

		#region Fields
		/// <summary>
		/// Type of price interval
		/// </summary>
		public PriceType Type { get; set; }

		/// <summary>
		/// Price value in dollars
		/// </summary>
		public decimal Value { get; set; }
		#endregion

		#region Navigation properties
		public IServiceOptionDto ServiceOption { get; set; }
		#endregion
	}
}
