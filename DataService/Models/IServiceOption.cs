using System;
using Common.Dto;


namespace DataService.Models
{
	public interface IServiceOption : IRequestable, ICatalogPublishable, IUserCreatedEntity
	{
		int ServiceOptionCategoryId { get; set; }
		Guid? Picture { get; set; }
		string PictureMimeType { get; set; }
		bool BasicRequest { get; set; }
		double PriceUpFront { get; set; }
		double PriceMonthly { get; set; }
		double Cost { get; set; }
		string Utilization { get; set; }
		string Included { get; set; }
		string Procurement { get; set; }
		string Description { get; set; }
		string Details { get; set; }
		ServiceOptionCategory ServiceOptionCategory { get; set; }

	}
}