using System;
using Common.Dto;


namespace DataService.Models
{
	public interface IServiceOption : IOffering, IRequestable, IUserCreatedEntity
	{
		int? OptionCategoryId { get; set; }
		Guid? Picture { get; set; }
		string PictureMimeType { get; set; }

		double PriceUpFront { get; set; }
		double PriceMonthly { get; set; }
		double Cost { get; set; }
		string Usage { get; set; }


    }
}