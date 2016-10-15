using System;
using System.Collections.Generic;

namespace DataService.Models
{
	public interface IServiceBundle
	{
		string BusinessValue { get; set; }
		Guid CreatedByUserId { get; set; }
		DateTime? DateCreated { get; set; }
		DateTime? DateUpdated { get; set; }
		string Description { get; set; }
		Guid? Id { get; set; }
		string Measures { get; set; }
		string Name { get; set; }
		ICollection<IService> Services { get; set; }
		Guid UpdatedByUserId { get; set; }
	}
}