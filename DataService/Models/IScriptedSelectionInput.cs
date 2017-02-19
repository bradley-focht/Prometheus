using System.Collections.Generic;
using Common.Dto;

namespace DataService.Models
{
	public interface IScriptedSelectionInput : ISelectable
	{
		string Script { get; set; }
		bool ExecutionEnabled { get; set; }

		ICollection<ServiceOption> ServiceOptions { get; set; }
	}
}
