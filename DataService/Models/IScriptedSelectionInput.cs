using System.Collections.Generic;
using Common.Dto;

namespace DataService.Models
{
	/// <summary>
	/// Combobox selection style input where selection options are generated via a script
	/// </summary>
	public interface IScriptedSelectionInput : ISelectable
	{
		/// <summary>
		/// Script to execute
		/// </summary>
		int ScriptId { get; set; }

		/// <summary>
		/// Allow the execution of the script
		/// </summary>
		bool ExecutionEnabled { get; set; }

		ICollection<ServiceOption> ServiceOptions { get; set; }
	}
}
