using System.Collections.Generic;

namespace Common.Dto
{
    /// <summary>
    /// Just a marker interface at the moment
    /// </summary>
    public interface IRequestableDto
    {
		//name, id, serviceId

		//help tip?

		ICollection<ITextInputDto> TextInputs { get; set; }
		ICollection<IScriptedSelectionDto> ScriptedSelecentionInputs { get; set; }
		ICollection<ISelectionDto> SelectionInputs { get; set; }

		//anything else required to be requestable. included items?
    }
}
