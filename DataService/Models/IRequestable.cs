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

		ICollection<TextInput> TextInputs { get; set; }
		ICollection<ScriptedSelectionInput> ScriptedSelectionInputs { get; set; }
		ICollection<SelectionInput> SelectionInputs { get; set; }

		//anything else required to be requestable. included items?
	}
}
