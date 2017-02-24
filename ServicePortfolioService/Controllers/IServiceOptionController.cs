using System.Collections.Generic;
using Common.Dto;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface IServiceOptionController
	{
		/// <summary>
		/// Finds option with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceOptionId"></param>
		/// <returns></returns>
		IServiceOptionDto GetServiceOption(int performingUserId, int serviceOptionId);

		/// <summary>
		/// Modifies the option in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceOption"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified SWOT</returns>
		IServiceOptionDto ModifyServiceOption(int performingUserId, IServiceOptionDto serviceOption, EntityModification modification);

		/// <summary>
		/// Gets the required inputs for all supplied service options
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceOptions">Service Options to get the inputs for</param>
		/// <returns></returns>
		IInputGroupDto GetInputsForServiceOptions(int performingUserId, IEnumerable<IServiceOptionDto> serviceOptions);

		/// <summary>
		/// Adds the supplied inputs to the ServiceOption provided
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceOptionId">ID of Service Option to add inputs to</param>
		/// <param name="inputsToAdd">Inputs to add to the Service Option</param>
		/// <returns></returns>
		IServiceOptionDto AddInputsToServiceOption(int performingUserId, int serviceOptionId, IInputGroupDto inputsToAdd);

		/// <summary>
		/// Removes the supplied inputs from the ServiceOption provided
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceOptionId">ID of Service Option to remove inputs from</param>
		/// <param name="inputsToRemove">Inputs to remove from the Service Option</param>
		/// <returns></returns>
		IServiceOptionDto RemoveInputsFromServiceOption(int performingUserId, int serviceOptionId, IInputGroupDto inputsToRemove);
	}
}
