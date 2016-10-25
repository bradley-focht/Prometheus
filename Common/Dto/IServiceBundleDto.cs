using System;
using System.Collections.Generic;

namespace Common.Dto
{
	public interface IServiceBundleDto
	{
		string BusinessValue { get; set; }
		int CreatedByUserId { get; set; }
		DateTime? DateCreated { get; set; }
		DateTime? DateUpdated { get; set; }
		string Description { get; set; }
		int Id { get; set; }
		string Measures { get; set; }
		string Name { get; set; }
		ICollection<IServiceDto> Services { get; set; }
		int UpdatedByUserId { get; set; }
	}
}