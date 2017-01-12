using System.Collections.Generic;

namespace DataService.Models
{
	public class SelectionInput : ISelectionInput
	{
		public int Id { get; set; }

		public int ServiceOptionId { get; set; }

		public string DisplayName { get; set; }
		
		public string HelpToolTip { get; set; }
		public int NumberToSelect { get; set; }
		public IEnumerable<string> SelectItems { get; set; }
	}
}