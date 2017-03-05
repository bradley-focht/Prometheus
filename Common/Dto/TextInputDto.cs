using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
    /// <summary>
    /// Text Input for forms
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
		[Display(Order = 1, Name="Display Name")]
		[Required(ErrorMessage = "Display Name is required")]
		public string DisplayName { get; set; }
		/// <summary>
		/// Display a user-help tip
		/// </summary>
		[Display(Order = 2, Name="Help Tip")]
		[AllowHtml]
		public string HelpToolTip { get; set; }

	    public bool AvailableOnAdd { get; set; }
	    public bool AvailableOnRemove { get; set; }

	    /// <summary>
		/// false for Textbox, true for Textarea
		/// </summary>
		[Display(Order = 3, Name="Multi Line")]
		public bool MultiLine { get; set; }
	}
}