using System.Collections.Generic;

namespace DataService.Models
{
    /// <summary>
    /// Just a marker interface at the moment
    /// </summary>
    public interface IRequestable
    {
		//name, id, serviceId

		//help tip?

		ICollection<ITextInput> TextInputs { get; set; }
		ICollection<IScriptedSelectionInput> ScriptedSelecentionInputs { get; set; }
		ICollection<ISelectionInput> SelectionInputs { get; set; }

		//anything else required to be requestable. included items?
    }
}
