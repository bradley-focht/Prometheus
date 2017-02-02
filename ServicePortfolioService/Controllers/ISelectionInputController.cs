using System.Collections.Generic;
using Common.Dto;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface ISelectionInputController : IUserController
	{
		/// <summary>
		/// Finds text input with identifier provided and returns its DTO
		/// </summary>
		/// <param name="textInput"></param>
		/// <returns></returns>
		ISelectionInputDto GetSelectionInput(int textInput);

        /// <summary>
        /// Retrieve all. 
        /// </summary>
        /// <returns></returns>
	    IEnumerable<ISelectionInputDto> GetSelectionInputs();

            /// <summary>
		/// Modifies the text input in the database
		/// </summary>
		/// <param name="selectionInput"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Measure</returns>
		ISelectionInputDto ModifySelectionInput(ISelectionInputDto selectionInput, EntityModification modification);
	}
}