using System.Collections.Generic;
using Common.Dto;

namespace DataService.Models
{
	public interface ISelectionInput : ISelectable
	{
		IEnumerable<string> SelectItems { get; set; }
	}
}