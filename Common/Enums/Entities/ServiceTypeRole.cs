using System.ComponentModel.DataAnnotations;

namespace Common.Enums.Entities
{
	/// <summary>
	/// Catalog visibility setting for a service
	/// </summary>
	public enum ServiceTypeRole
	{
		/// <summary>
		/// Visibile in the Business Catalog
		/// </summary>
		[Display(Name = "Customer-Facing Service")]
		Business,

		/// <summary>
		/// Visibile in the Support Catalog
		/// </summary>
		[Display(Name = "Supporting Service")]
		Supporting
	}
}
