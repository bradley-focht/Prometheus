using System;
using System.Collections.Generic;
using Common.Enums;
using Common.Enums.Entities;

namespace Common.Dto
{
	public interface IServiceOptionDto : IUserCreatedEntityDto
	{
		int Id { get; set; }
		int ServiceId { get; set; }
        string Description { get; set; }
        string Name { get; set; }
        string BusinessValue { get; set; }
        string Picture { get; set; }
        ICollection<Tuple<PriceType, double>> Prices { get; set; }
        double Cost { get; set; }

	}
}