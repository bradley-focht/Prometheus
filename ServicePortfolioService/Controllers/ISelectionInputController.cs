using System.Collections.Generic;
using Common.Dto;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface ISelectionInputController
	{
		/// <summary>
		/// Finds text input with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="textInput"></param>
		/// <returns></returns>
		ISelectionInputDto GetSelectionInput(int performingUserId, int textInput);

		/// <summary>
		/// Retrieve all. 
		/// </summary>
		/// <returns></returns>
		IEnumerable<ISelectionInputDto> GetSelectionInputs(int performingUserId);

		/// <summary>
		/// Modifies the text input in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="selectionInput"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Measure</returns>
		ISelectionInputDto ModifySelectionInput(int performingUserId, ISelectionInputDto selectionInput, EntityModification modification);
	}
}