using System;
using System.Collections.Generic;

namespace DataService.Models
{
	public interface IServiceBundle
	{
		string BusinessValue { get; set; }
		int CreatedByUserId { get; set; }
		DateTime? DateCreated { get; set; }
		DateTime? DateUpdated { get; set; }
		string Description { get; set; }
		int Id { get; set; }
		string Measures { get; set; }
		string Name { get; set; }
		ICollection<Service> Services { get; set; }
		int UpdatedByUserId { get; set; }
	}
}