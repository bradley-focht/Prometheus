using System;


namespace DataService.Models
{
	public interface IServiceOption : IRequestable, IUserCreatedEntity
	{
		int Id { get; set; }

		string Name { get; set; }
		int ServiceOptionCategoryId { get; set; }
		Guid? Picture { get; set; }
		string PictureMimeType { get; set; }

		double PriceUpFront { get; set; }
		double PriceMonthly { get; set; }
		double Cost { get; set; }
		string Usage { get; set; }

		ServiceOptionCategory ServiceOptionCategory { get; set; }

	}
}