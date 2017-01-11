using System.Collections.Generic;

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
		string Picture { get; set; }
		double Cost { get; set; }

		Service Service { get; set; }
		ICollection<Price> Prices { get; set; }
	}
}