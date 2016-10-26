using Common.Dto;
using System;
using System.Collections.Generic;

namespace ServicePortfolio.Controllers
{
	public interface IServiceBundleController
	{
		/// <summary>
		/// Finds service bundle with identifier provided and returns its DTO
		/// </summary>
		/// <param name="serviceBundleId"></param>
		/// <returns></returns>
		IServiceBundleDto GetServiceBundle(int serviceBundleId);

		/// <summary>
		/// KVP of all service bundle IDs and names in ascending order by name
		/// </summary>
		/// <returns></returns>
		IEnumerable<Tuple<int, string>> GetServiceBundleNames();

		/// <summary>
		/// Returns all service bundles
		/// </summary>
		/// <returns></returns>
		IEnumerable<IServiceBundleDto> GetServiceBundles();

		/// <summary>
		/// Saves the service bundle to the database
		/// </summary>
		/// <param name="serviceBundle"></param>
		/// <returns>Saved entity DTO</returns>
		IServiceBundleDto SaveServiceBundle(IServiceBundleDto serviceBundle);

		/// <summary>
		/// Deletes the service bundle from the database
		/// </summary>
		/// <param name="serviceBundleId"></param>
		/// <returns>True if successful</returns>
		bool DeleteServiceBundle(int serviceBundleId);
	}
}