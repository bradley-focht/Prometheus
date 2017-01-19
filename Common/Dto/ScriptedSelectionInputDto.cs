using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
	/// <summary>
	/// Results are retrieved by running this script
	/// </summary>
	public class ScriptedSelectionInputDto : IScriptedSelectionInputDto
	{
		/// <summary>
		/// Script to execute
		/// </summary>
		[Display(Order = 5, Name="Script")]
		public string Script { get; set; }

		/// <summary>
		/// Disallow the execution of the script
		/// </summary>
		[Display(Order = 4, Name = "Execution Enabled")]
		public bool ExecutionEnabled { get; set; }

		/// <summary>
		/// PK
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// FK
		/// </summary>
		public int ServiceOptionId { get; set; }

		/// <summary>
		/// User friendly name displayed
		/// </summary>
		[Display(Order=1, Name = "Display Name")]
		public string DisplayName { get; set; }

		/// <summary>
		/// Helpful tool tip
		/// </summary>
		[Display(Order = 2, Name = "Help Tool Tip")]
		[AllowHtml]
		public string HelpToolTip { get; set; }

		/// <summary>
		/// Maximum number of items that can be selected
		/// </summary>
		[Display(Order = 3, Name="Number of Selections")]
		public int NumberToSelect { get; set; }
	}
}