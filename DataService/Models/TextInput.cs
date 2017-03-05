using System.Collections.Generic;

namespace DataService.Models
{
	public class TextInput : ITextInput
	{
		public int Id { get; set; }
		public string DisplayName { get; set; }
		public string HelpToolTip { get; set; }
		public bool MultiLine { get; set; }
		public virtual ICollection<ServiceOption> ServiceOptions { get; set; }

		public bool AvailableOnAdd { get; set; }
		public bool AvailableOnRemove { get; set; }
	}
}