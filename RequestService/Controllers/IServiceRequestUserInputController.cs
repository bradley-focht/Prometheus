using Common.Dto;
using Common.Enums.Entities;

namespace RequestService.Controllers
{
	public interface IServiceRequestUserInputController
	{
		/// <summary>
		/// Finds service request user input with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="userInputId"></param>
		/// <returns></returns>
		IServiceRequestUserInputDto GetServiceRequestUserInput(int performingUserId, int userInputId);

		/// <summary>
		/// Modifies the service request input in the database
		/// </summary>
		/// <param name="performingUserId">ID of user performing the modification</param>
		/// <param name="userInput"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Request User Input</returns>
		IServiceRequestUserInputDto ModifyServiceRequestUserInput(int performingUserId, IServiceRequestUserInputDto userInput, EntityModification modification);
	}
}
