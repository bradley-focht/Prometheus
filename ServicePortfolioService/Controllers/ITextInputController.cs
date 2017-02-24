using System.Collections.Generic;
using Common.Dto;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface ITextInputController
	{
		/// <summary>
		/// Finds text input with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="textInput"></param>
		/// <returns></returns>
		ITextInputDto GetTextInput(int performingUserId, int textInput);

		/// <summary>
		/// Retrieve all
		/// </summary>
		/// <returns></returns>
		IEnumerable<ITextInputDto> GetTextInputs(int performingUserId);

		/// <summary>
		/// Modifies the text input in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="textInput"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Measure</returns>
		ITextInputDto ModifyTextInput(int performingUserId, ITextInputDto textInput, EntityModification modification);
	}
}