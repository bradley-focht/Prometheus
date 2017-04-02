using System.Collections.Generic;
using Common.Dto;

namespace DataService.Models
{
	/// <summary>
	/// Combobox selection style input
	/// </summary>
	public interface ISelectionInput : ISelectable
	{
		/// <summary>
		/// List of items selected separated by the delimiter
		/// </summary>
		string SelectItems { get; set; }

		/// <summary>
		/// Delimiter for separation of selected items
		/// </summary>
		string Delimiter { get; set; }

		ICollection<ServiceOption> ServiceOptions { get; set; }
	}
}