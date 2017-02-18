using System.Collections.Generic;

namespace Common.Dto
{
	public interface IInputGroupDto
	{
		IEnumerable<IScriptedSelectionInputDto> ScriptedSelectionInputs { get; set; }
		IEnumerable<ISelectionInputDto> SelectionInputs { get; set; }
		IEnumerable<ITextInputDto> TextInputs { get; set; }
	}
}