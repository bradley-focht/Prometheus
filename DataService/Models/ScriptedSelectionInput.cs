using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DataService.Models
{
	/// <summary>
	/// Combobox selection style input where selection options are generated via a script
	/// </summary>
	public class ScriptedSelectionInput : IScriptedSelectionInput
	{
		/// <summary>
		/// Script to execute
		/// </summary>
		public int ScriptId { get; set; }

		/// <summary>
		/// Allow the execution of the script
		/// </summary>
		public bool ExecutionEnabled { get; set; }
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
		[Display(Order = 4, Name = "Available on Add")]
		public bool AvailableOnAdd { get; set; }


		/// <summary>
		/// If the Input is to be used on ServiceRequestAction.Change
		/// </summary>
		public bool AvailableOnChange { get; set; }


		/// <summary>
		/// If the Input is to be used on ServiceRequestAction.Remove
		/// </summary>
		[Display(Order = 5, Name = "Available on Remove")]
		public bool AvailableOnRemove { get; set; }

		/// <summary>
		/// Maximum number of items that can be selected
		/// </summary>
		[Display(Order = 3, Name = "Number to Select")]
		public int NumberToSelect { get; set; }

		public virtual ICollection<ServiceOption> ServiceOptions { get; set; }
	}
}