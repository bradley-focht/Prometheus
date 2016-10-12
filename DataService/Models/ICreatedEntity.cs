using System;

namespace DataService.Models
{
	public interface ICreatedEntity
	{
		DateTime? DateCreated { get; set; }
		DateTime? DateUpdated { get; set; }
	}
}
