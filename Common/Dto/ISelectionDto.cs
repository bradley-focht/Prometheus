using System.Collections.Generic;

namespace Common.Dto
{
	public interface ISelectionDto : ISelectable
	{
		IEnumerable<string> SelectItems { get; set; }
	}
}