using System.Collections.Generic;
using Common.Dto;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface IScriptedSelectionController : IUserController
	{
		/// <summary>
		/// Finds text input with identifier provided and returns its DTO
		/// </summary>
		/// <param name="scriptedSelection"></param>
		/// <returns></returns>
		IScriptedSelectionInputDto GetScriptedSelectionInput(int scriptedSelection);


	    IEnumerable<IScriptedSelectionInputDto> GetScriptedSelectionInputs();
            /// <summary>
		/// Modifies the text input in the database
		/// </summary>
		/// <param name="scriptedSelection"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Measure</returns>
		IScriptedSelectionInputDto ModifyScriptedSelectionInput(IScriptedSelectionInputDto scriptedSelection, EntityModification modification);
	}
}