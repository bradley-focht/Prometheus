using System;
using System.Collections.Generic;
using Common.Enums;
using Common.Enums.Entities;

namespace DataService.Models
{
	public interface IServiceOption : IUserCreatedEntity
	{
		int Id { get; set; }
		int? OptionCategoryId { get; set; }
        int Popularity { get; set; }
		int ServiceId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string BusinessValue { get; set; }
		Guid? Picture { get; set; }
		string PictureMimeType { get; set; }

		double PriceUpFront { get; set; }
		double PriceMonthly { get; set; }
		double Cost { get; set; }

    }
}