using System.Collections.Generic;

namespace DataService.Models
{
	/// <summary>
	/// ITIL Status that a Service can be in
	/// </summary>
	public interface ILifecycleStatus : IUserCreatedEntity
	{
		int Id { get; set; }

		/// <summary>
		/// Attribute to decide if services with this status will be 
		/// visible in business / support catalog
		/// </summary>
		bool CatalogVisible { get; set; }

		/// <summary>
		/// Unique name of each status
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Used to order the display, this does not have any actual function other than display
		/// </summary>
		int Position { get; set; }

		/// <summary>
		/// Services with this Lifecycle Status applied to them
		/// </summary>
		ICollection<Service> Services { get; set; }
	}
}