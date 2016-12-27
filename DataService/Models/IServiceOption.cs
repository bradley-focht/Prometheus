using System;
using System.Collections.Generic;
using Common.Enums;
using Common.Enums.Entities;

namespace DataService.Models
{
	public interface IServiceOption : IUserCreatedEntity
	{
		int Id { get; set; }
        int Popularity { get; set; }
		int ServiceId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string BusinessValue { get; set; }
        string Picture { get; set; }
        ICollection<Tuple<PriceType, double>> Prices { get; set; }
        double Cost { get; set; }

    }
}