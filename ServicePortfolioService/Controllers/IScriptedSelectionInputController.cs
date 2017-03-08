using System.Collections.Generic;
using Common.Dto;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface IScriptedSelectionInputController
	{
		/// <summary>
		/// Finds text input with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="scriptedSelection"></param>
		/// <returns></returns>
		IScriptedSelectionInputDto GetScriptedSelectionInput(int performingUserId, int scriptedSelection);


		IEnumerable<IScriptedSelectionInputDto> GetScriptedSelectionInputs(int performingUserId);

		/// <summary>
		/// Modifies the text input in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="scriptedSelection"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Measure</returns>
		IScriptedSelectionInputDto ModifyScriptedSelectionInput(int performingUserId, IScriptedSelectionInputDto scriptedSelection, EntityModification modification);
	}
}