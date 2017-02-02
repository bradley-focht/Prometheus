using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
	public class SelectionInputDto : ISelectionInputDto 
	{
		[HiddenInput]
		public int Id { get; set; }
		[Display(Order = 1, Name="Display Name")]
		[Required(ErrorMessage = "Display Name is required")]
		public string DisplayName { get; set; }

		[Display(Order = 2, Name="Help Tool Tip")]
		[AllowHtml]
		public string HelpToolTip { get; set; }
		[Display(Order = 3, Name = "Number of Selections")]
		public int NumberToSelect { get; set; }
		[Display(Order = 4, Name = "Selection Items")]
		public string SelectItems { get; set; }
		[HiddenInput]
		public string Delimiter { get; set; }
	}
}