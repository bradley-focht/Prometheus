using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
	/// <summary>
	/// Combobox selection style input
	/// </summary>
	public class SelectionInputDto : ISelectionInputDto
	{
		[HiddenInput]
		public int Id { get; set; }

		/// <summary>
		/// User friendly name displayed
		/// </summary>
		[Display(Order = 1, Name = "Display Name")]
		[Required(ErrorMessage = "Display Name is required")]
		public string DisplayName { get; set; }

		/// <summary>
		/// Helpful tool tip
		/// </summary>
		[Display(Order = 2, Name = "Help Tool Tip")]
		[AllowHtml]
		public string HelpToolTip { get; set; }

		/// <summary>
		/// If the Input is to be used on ServiceRequestAction.New
		/// </summary>
		[Display(Order = 5, Name = "Available on Add")]
		public bool AvailableOnAdd { get; set; }

		/// <summary>
		/// If the Input is to be used on ServiceRequestAction.Change
		/// </summary>
		[Display(Order = 6, Name = "Available on Change")]
		public bool AvailableOnChange { get; set; }

		/// <summary>
		/// If the Input is to be used on ServiceRequestAction.Remove
		/// </summary>
		[Display(Order = 7, Name = "Available on Remove")]
		public bool AvailableOnRemove { get; set; }

		/// <summary>
		/// Maximum number of items that can be selected
		/// </summary>
		[Display(Order = 3, Name = "Number of Selections")]
		public int NumberToSelect { get; set; }

		/// <summary>
		/// List of items selected separated by the delimiter
		/// </summary>
		[Display(Order = 4, Name = "Selection Items")]
		public string SelectItems { get; set; }

		/// <summary>
		/// Delimiter for separation of selected items
		/// </summary>
		[HiddenInput]
		public string Delimiter { get; set; }
	}
}