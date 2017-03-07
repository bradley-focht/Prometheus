using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DataService.Models
{
	public class ScriptedSelectionInput : IScriptedSelectionInput
	{
		public int ScriptId { get; set; }
		public bool ExecutionEnabled { get; set; }
		public int Id { get; set; }
		[Display(Order = 1, Name = "Display Name")]
		[Required(ErrorMessage = "Display Name is required")]
		public string DisplayName { get; set; }
		[Display(Order = 2, Name = "Help Tool Tip")]
		[AllowHtml]
		public string HelpToolTip { get; set; }
		[Display(Order = 4, Name = "Available on Add")]
		public bool AvailableOnAdd { get; set; }

		public bool AvailableOnChange { get; set; }

		[Display(Order = 5, Name = "Available on Remove")]
		public bool AvailableOnRemove { get; set; }
		[Display(Order = 3, Name = "Number to Select")]
		public int NumberToSelect { get; set; }

		public virtual ICollection<ServiceOption> ServiceOptions { get; set; }
	}
}