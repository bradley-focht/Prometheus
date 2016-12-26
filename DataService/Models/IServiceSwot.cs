using Common.Enums;
using System.Collections.Generic;
using Common.Enums.Entities;

namespace DataService.Models
{
	public interface IServiceSwot : IUserCreatedEntity
	{
		int Id { get; set; }
		int ServiceId { get; set; }

		string Description { get; set; }
		string Item { get; set; }
		ServiceSwotType Type { get; set; }

		Service Service { get; set; }
		ICollection<SwotActivity> SwotActivities { get; set; }
	}
}