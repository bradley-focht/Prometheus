using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
	/// <summary>
	/// Text Input for Service Requests
	/// </summary>
	public class TextInputDto : ITextInputDto
	{
		/// <summary>
		/// PK
		/// </summary>
		[HiddenInput]
		public int Id { get; set; }

		/// <summary>
		/// Name as seen in the UI
		/// </summary>
		[Display(Order = 1, Name = "Display Name")]
		[Required(ErrorMessage = "Display Name is required")]
		public string DisplayName { get; set; }

		/// <summary>
		/// Display a user-help tip
		/// </summary>
		[Display(Order = 2, Name = "Help Tip")]
		[AllowHtml]
		public string HelpToolTip { get; set; }

		/// <summary>
		/// If the Input is to be used on ServiceRequestAction.New
		/// </summary>
		[Display(Order = 4, Name = "Available on Add")]
		public bool AvailableOnAdd { get; set; }

		/// <summary>
		/// If the Input is to be used on ServiceRequestAction.Change
		/// </summary>
		[Display(Order = 5, Name = "Available on Remove")]
		public bool AvailableOnChange { get; set; }

		/// <summary>
		/// If the Input is to be used on ServiceRequestAction.Remove
		/// </summary>
		[Display(Order = 6, Name = "Available on Remove")]
		public bool AvailableOnRemove { get; set; }

		/// <summary>
		/// false for Textbox, true for Textarea
		/// </summary>
		[Display(Order = 3, Name = "Multi Line")]
		public bool MultiLine { get; set; }
	}
}