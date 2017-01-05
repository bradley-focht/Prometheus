using System;

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
        Guid? Picture { get; set; }
		string PictureMimeType { get; set; }
        double PriceUpFront { get; set; }
		double PriceMonthly { get; set; }
        double Cost { get; set; }

	}
}