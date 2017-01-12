using System.Collections.Generic;

namespace Common.Dto
{
	public interface ISelectionInputDto : ISelectable
	{
		IEnumerable<string> SelectItems { get; set; }
	}
}