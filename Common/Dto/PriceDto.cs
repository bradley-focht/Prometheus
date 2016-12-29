using Common.Enums.Entities;

namespace Common.Dto
{
	public class PriceDto : IPriceDto
	{
		//PK
		public int Id { get; set; }

		//FK
		public int ServiceOptionId { get; set; }

		#region Fields
		public PriceType Type { get; set; }
		public decimal Value { get; set; }
		#endregion

		#region Navigation properties
		public IServiceOptionDto ServiceOption { get; set; }
		#endregion
	}
}
