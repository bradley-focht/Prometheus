using System;
using System.Collections.Generic;
using Common.Dto;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
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
		/// Applies a service bundle ID to multiple services.
		/// 
		/// NOTE: null can be applied as service bundle ID to remove the services from their bundle
		/// </summary>
		/// <param name="performingUserId">ID for user performing adjustment</param>
		/// <param name="serviceBundleId">ID to add to the services. Can be null</param>
		/// <param name="services">Services to set the Service Bundle on</param>
		/// <returns></returns>
		IEnumerable<IServiceDto> SetServiceBundleForServices(int performingUserId, int? serviceBundleId, IEnumerable<IServiceDto> services);

		/// <summary>
		/// Modifies the service in the database
		/// </summary>
		/// <param name="performingUserId">ID for user doing modification</param>
		/// <param name="service"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified entity DTO</returns>
		IServiceDto ModifyService(int performingUserId, IServiceDto service, EntityModification modification);

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

		/// <summary>
		/// Get all documents associated with a service
		/// </summary>
		/// <param name="serviceId"></param>
		/// <returns></returns>
		IEnumerable<IServiceDocumentDto> GetServiceDocuments(int serviceId);
	}
}