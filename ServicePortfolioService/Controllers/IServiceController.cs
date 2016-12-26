using Common.Dto;
using Common.Enums;
using System;
using System.Collections.Generic;
using Common.Enums.Entities;

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
		/// Modifies the service in the database
		/// </summary>
		/// <param name="service"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified entity DTO</returns>
		IServiceDto ModifyService(IServiceDto service, EntityModification modification);

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