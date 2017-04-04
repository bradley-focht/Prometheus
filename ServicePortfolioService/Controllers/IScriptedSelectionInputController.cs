using System.Collections.Generic;
using Common.Dto;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface IScriptedSelectionInputController
	{
		/// <summary>
		/// Finds Scripted Selection Input with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="scriptedSelection"></param>
		/// <returns></returns>
		IScriptedSelectionInputDto GetScriptedSelectionInput(int performingUserId, int scriptedSelection);

		/// <summary>
		/// Returns a list of all of the Scripted Selection Inputs found
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <returns></returns>
		IEnumerable<IScriptedSelectionInputDto> GetScriptedSelectionInputs(int performingUserId);

		/// <summary>
		/// Modifies the Scripted Selection Input in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="scriptedSelection"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Scripted Selection Input</returns>
		IScriptedSelectionInputDto ModifyScriptedSelectionInput(int performingUserId, IScriptedSelectionInputDto scriptedSelection, EntityModification modification);
	}
}