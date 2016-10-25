using Common.Dto;
using System;
using System.Collections.Generic;

namespace ServicePortfolio.Controllers
{
	public interface IServiceController
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
		/// <returns>True if successful</returns>
		bool SaveService(IServiceDto service);

		/// <summary>
		/// Deletes the service from the database
		/// </summary>
		/// <param name="serviceId"></param>
		/// <returns>True if successful</returns>
		bool DeleteService(int serviceId);
	}
}