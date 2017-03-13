using System;
using System.Collections.Generic;
using Common.Dto;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
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
		/// Modifies the service bundle in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceBundle"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Bundle</returns>
		IServiceBundleDto ModifyServiceBundle(int performingUserId, IServiceBundleDto serviceBundle, EntityModification modification);

	}
}