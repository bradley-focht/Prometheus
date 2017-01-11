using System.Collections.Generic;

namespace Common.Dto
{
	public interface IServiceOptionDto : IUserCreatedEntityDto
	{
		int Id { get; set; }
		int? CategoryId { get; set; }
		int Popularity { get; set; }
		int ServiceId { get; set; }
		string Description { get; set; }
		string Name { get; set; }
		string BusinessValue { get; set; }
		string Picture { get; set; }
		double Cost { get; set; }

		ICollection<IPriceDto> Prices { get; set; }
	}
}