using Common.Enums;
using System.Collections.Generic;

namespace DataService.Models
{
	public interface IServiceSwot : IUserCreatedEntity
	{
		int Id { get; set; }
		int ServiceId { get; set; }

		string Description { get; set; }
		string Item { get; set; }
		ServiceSwotType Type { get; set; }

		IService Service { get; set; }
		ICollection<ISwotActivity> SwotActivities { get; set; }
	}
}