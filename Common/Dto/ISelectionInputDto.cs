using System.Collections.Generic;

namespace Common.Dto
{
	public interface ISelectionInputDto : ISelectable
	{
		string SelectItems { get; set; }
		string Delimiter { get; set; }
	}
}