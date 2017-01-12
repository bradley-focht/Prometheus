using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
	public class SelectionInputDto : ISelectionInputDto 
	{
		[HiddenInput]
		public int Id { get; set; }

		public int ServiceOptionId { get; set; }

		[Display(Order = 1, Name="Display Order")]
		public string DisplayName { get; set; }


		public string Name { get; set; }
		[Display(Name="Help Tool Tip")]
		public string HelpToolTip { get; set; }
		public int NumberToSelect { get; set; }
		public IEnumerable<string> SelectItems { get; set; }
	}
}