using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
	public class TextInputDto : ITextInputDto
	{
		/// <summary>
		/// PK
		/// </summary>
		[HiddenInput]
		public int Id { get; set; }
		/// <summary>
		/// FK
		/// </summary>
		[HiddenInput]
		public int ServiceOptionId { get; set; }

		/// <summary>
		/// Name as seen in the UI
		/// </summary>
		[Display(Name="Display Name")]
		[Required(ErrorMessage = "Display Name is required")]
		public string DisplayName { get; set; }
		/// <summary>
		/// Display a user-help tip
		/// </summary>
		[Display(Name="Help Tip")]
		[AllowHtml]
		public string HelpToolTip { get; set; }

		/// <summary>
		/// false for Textbox, true for Textarea
		/// </summary>
		public bool MultiLine { get; set; }
	}
}