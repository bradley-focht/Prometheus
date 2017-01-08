using System;

namespace Common.Dto
{
	public interface IServiceOptionDto : IOffering, IRequestable, IUserCreatedEntityDto
	{
		int? CategoryId { get; set; }
        Guid? Picture { get; set; }
		string PictureMimeType { get; set; }
        double PriceUpFront { get; set; }
		double PriceMonthly { get; set; }
        double Cost { get; set; }
		string Usage { get; set; }

	}
}