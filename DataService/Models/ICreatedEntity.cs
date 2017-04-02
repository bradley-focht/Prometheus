using System;

namespace DataService.Models
{
	/// <summary>
	/// Entity that is created. All entities should extend this interface
	/// </summary>
	public interface ICreatedEntity
	{
		/// <summary>
		/// Date that the record was created
		/// </summary>
		DateTime? DateCreated { get; set; }

		/// <summary>
		/// Date that the record was last updated
		/// </summary>
		DateTime? DateUpdated { get; set; }
	}
}
