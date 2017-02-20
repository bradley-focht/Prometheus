using System.Collections.Generic;
using Common.Dto;
using Common.Enums.Entities;

namespace RequestService.Controllers
{
	public interface IScriptFileController
	{
		/// <summary>
		/// Retrieve a single script
		/// </summary>
		/// <param name="id">script id</param>
		/// <returns></returns>
		ScriptDto GetScript(int id);

		/// <summary>
		/// return all scripts
		/// </summary>
		/// <returns></returns>
		IEnumerable<ScriptDto> GetScripts();

		/// <summary>
		/// Modify a script
		/// </summary>
		/// <param name="script">script to modify</param>
		/// <param name="modification">change to make</param>
		/// <returns></returns>
		ScriptDto ModifyScript(IScriptDto script, EntityModification modification);
	}
}
