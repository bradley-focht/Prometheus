using System.Collections.Generic;

namespace DataService.Models
{
	/// <summary>
	/// Text Input for Service Requests
	/// </summary>
	public class TextInput : ITextInput
	{
		public int Id { get; set; }

		/// <summary>
		/// Name as seen in the UI
		/// </summary>
		public string DisplayName { get; set; }

		/// <summary>
		/// Display a user-help tip
		/// </summary>
		public string HelpToolTip { get; set; }

		/// <summary>
		/// false for Textbox, true for Textarea
		/// </summary>
		public bool MultiLine { get; set; }

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
		public virtual ICollection<ServiceOption> ServiceOptions { get; set; }
	}
}