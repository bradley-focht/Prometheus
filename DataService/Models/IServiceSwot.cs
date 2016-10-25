using Common.Enums;
using System.Collections.Generic;

namespace DataService.Models
{
	interface IServiceSwot
	{
		string Description { get; set; }
		int Id { get; set; }
		ICollection<SwotActivity> SwotActivities { get; set; }
		ServiceSwotType Type { get; set; }
	}
}