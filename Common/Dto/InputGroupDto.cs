using System.Collections.Generic;

namespace Common.Dto
{
	public class InputGroupDto : IInputGroupDto
	{
		public IEnumerable<ITextInputDto> TextInputs { get; set; }
		public IEnumerable<ISelectionInputDto> SelectionInputs { get; set; }
		public IEnumerable<IScriptedSelectionInputDto> ScriptedSelectionInputs { get; set; }
	}
}
