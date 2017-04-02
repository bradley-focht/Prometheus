using System.Collections.Generic;

namespace DataService.Models
{
	/// <summary>
	/// Combobox selection style input
	/// </summary>
	public class SelectionInput : ISelectionInput
	{
		public int Id { get; set; }

		/// <summary>
		/// User friendly name displayed
		/// </summary>
		public string DisplayName { get; set; }

		/// <summary>
		/// Helpful tool tip
		/// </summary>
		public string HelpToolTip { get; set; }

		/// <summary>
		/// If the Input is to be used on ServiceRequestAction.New
		/// </summary>
		public bool AvailableOnAdd { get; set; }

		/// <summary>
		/// If the Input is to be used on ServiceRequestAction.Change
		/// </summary>
		public bool AvailableOnChange { get; set; }
		/// <summary>
		/// If the Input is to be used on ServiceRequestAction.Remove
		/// </summary>
		public bool AvailableOnRemove { get; set; }

		/// <summary>
		/// Maximum number of items that can be selected
		/// </summary>
		public int NumberToSelect { get; set; }

		/// <summary>
		/// List of items selected separated by the delimiter
		/// </summary>
		public string SelectItems { get; set; }

		/// <summary>
		/// Delimiter for separation of selected items
		/// </summary>
		public string Delimiter { get; set; }
		public virtual ICollection<ServiceOption> ServiceOptions { get; set; }
	}
}