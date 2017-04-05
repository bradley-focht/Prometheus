using System;

namespace Common.Dto
{
	/// <summary>
	/// Logical grouping of ITIL Services
	/// </summary>
	public interface IServiceBundleDto
	{
		int Id { get; set; }

		/// <summary>
		/// Extra text for those who just enjoy reading that much
		/// </summary>
		string BusinessValue { get; set; }

		int CreatedByUserId { get; set; }
		DateTime? DateCreated { get; set; }
		DateTime? DateUpdated { get; set; }

		/// <summary>
		/// Free text field
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// List of measures, should be comma separated, but won't be enforced
		/// </summary>
		string Measures { get; set; }

		/// <summary>
		/// Unique name must be provided
		/// </summary>
		string Name { get; set; }
		int UpdatedByUserId { get; set; }
	}
}