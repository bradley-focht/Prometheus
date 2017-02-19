using System.Collections.Generic;
using Common.Dto;

namespace DataService.Models
{
	public interface ISelectionInput : ISelectable
	{
		string SelectItems { get; set; }
		string Delimiter { get; set; }

		ICollection<ServiceOption> ServiceOptions { get; set; }
	}
}