using Common.Dto;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface ITextInputController : IUserController
	{
		/// <summary>
		/// Finds text input with identifier provided and returns its DTO
		/// </summary>
		/// <param name="textInput"></param>
		/// <returns></returns>
		ITextInputDto GetTextInput(int textInput);

		/// <summary>
		/// Modifies the text input in the database
		/// </summary>
		/// <param name="textInput"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Measure</returns>
		ITextInputDto ModifyTextInput(ITextInputDto textInput, EntityModification modification);
	}
}