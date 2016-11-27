using Common.Dto;
using System;
using System.Collections.Generic;

namespace ServicePortfolioService.Controllers
{
	public interface IServiceController : IUserController
	{
		/// <summary>
		/// Finds service with identifier provided and returns its DTO
		/// </summary>
		/// <param name="serviceId"></param>
		/// <returns></returns>
		IServiceDto GetService(int serviceId);

		/// <summary>
		/// Finds the service bundle from identifier then uses its services
		/// KVP of all services IDs and names in ascending order by name
		/// </summary>
		/// <returns></returns>
		IEnumerable<Tuple<int, string>> GetServiceNamesForServiceBundle(int serviceBundleId);

		/// <summary>
		/// Finds the service bundle from identifier then returns all of its services
		/// </summary>
		/// <param name="serviceBundleId"></param>
		/// <returns></returns>
		IEnumerable<IServiceDto> GetServicesForServiceBundle(int serviceBundleId);

		/// <summary>
		/// Saves the service to the database
		/// </summary>
		/// <param name="service"></param>
		/// <returns>Saved entity DTO</returns>
		IServiceDto SaveService(IServiceDto service);

		/// <summary>
		/// Deletes the service from the database
		/// </summary>
		/// <param name="serviceId"></param>
		/// <returns>True if successful</returns>
		bool DeleteService(int serviceId);

        /// <summary>
        /// Gets a list of services and names for making lists
        /// </summary>
        /// <returns></returns>
        IEnumerable<Tuple<int, string>> GetServiceNames();

        /// <summary>
        /// Get a full list of services 
        /// </summary>
        /// <returns></returns>
	    IEnumerable<IServiceDto> GetServices();
	}
}