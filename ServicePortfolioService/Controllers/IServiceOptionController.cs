using Common.Dto;
using Common.Enums.Entities;
using System.Collections.Generic;

namespace ServicePortfolioService.Controllers
{
	public interface IServiceOptionController : IUserController
	{
		/// <summary>
		/// Finds option with identifier provided and returns its DTO
		/// </summary>
		/// <param name="serviceOptionId"></param>
		/// <returns></returns>
		IServiceOptionDto GetServiceOption(int serviceOptionId);

		/// <summary>
		/// Modifies the option in the database
		/// </summary>
		/// <param name="serviceOption"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified SWOT</returns>
		IServiceOptionDto ModifyServiceOption(IServiceOptionDto serviceOption, EntityModification modification);

		/// <summary>
		/// Gets the required inputs for all supplied service options
		/// </summary>
		/// <param name="serviceOptions">Service Options to get the inputs for</param>
		/// <returns></returns>
		IInputGroupDto GetInputsForServiceOptions(IEnumerable<IServiceOptionDto> serviceOptions);

		/// <summary>
		/// Adds the supplied inputs to the ServiceOption provided
		/// </summary>
		/// <param name="serviceOptionId">ID of Service Option to add inputs to</param>
		/// <param name="inputsToAdd">Inputs to add to the Service Option</param>
		/// <returns></returns>
		IServiceOptionDto AddInputsToServiceOption(int serviceOptionId, IInputGroupDto inputsToAdd);

		/// <summary>
		/// Removes the supplied inputs from the ServiceOption provided
		/// </summary>
		/// <param name="serviceOptionId">ID of Service Option to remove inputs from</param>
		/// <param name="inputsToRemove">Inputs to remove from the Service Option</param>
		/// <returns></returns>
		IServiceOptionDto RemoveInputsFromServiceOption(int serviceOptionId, IInputGroupDto inputsToRemove);
	}
}
