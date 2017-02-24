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
		/// <param name="performingUserId"></param>
		/// <param name="id">script id</param>
		/// <returns></returns>
		IScriptDto GetScript(int performingUserId, int id);

		/// <summary>
		/// return all scripts
		/// </summary>
		/// <returns></returns>
		IEnumerable<IScriptDto> GetScripts(int performingUserId);

		/// <summary>
		/// Modify a script
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="script">script to modify</param>
		/// <param name="modification">change to make</param>
		/// <returns></returns>
		IScriptDto ModifyScript(int performingUserId, IScriptDto script, EntityModification modification);
	}
}
