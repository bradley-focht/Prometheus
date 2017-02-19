using System.Collections.Generic;

namespace DataService.Models
{
	public class SelectionInput : ISelectionInput
	{
		public int Id { get; set; }

		public string DisplayName { get; set; }
		
		public string HelpToolTip { get; set; }
		public int NumberToSelect { get; set; }
		public string SelectItems { get; set; }
		public string Delimiter { get; set; }
		public virtual ICollection<ServiceOption> ServiceOptions { get; set; }
	}
}