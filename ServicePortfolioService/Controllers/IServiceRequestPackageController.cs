using System.Collections.Generic;
using Common.Dto;
using Common.Enums;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface IServiceRequestPackageController
	{
		/// <summary>
		/// Finds service package with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="servicePackageId"></param>
		/// <returns></returns>
		IServiceRequestPackageDto GetServiceRequestPackage(int performingUserId, int servicePackageId);

		/// <summary>
		/// Modifies the service Package in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="servicePackage"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service WorkUnit</returns>
		IServiceRequestPackageDto ModifyServiceRequestPackage(int performingUserId, IServiceRequestPackageDto servicePackage, EntityModification modification);

		/// <summary>
		/// All of the Service Request Packages in the database
		/// </summary>
		IEnumerable<IServiceRequestPackageDto> AllServiceRequestPackages { get; }

		/// <summary>
		/// Retrieves the service packages that the service option id exists in
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceOptionId"></param>
		/// <returns></returns>
		IEnumerable<IServiceRequestPackageDto> GetServiceRequestPackagesForServiceOption(int performingUserId, int serviceOptionId, ServiceRequestAction action);
	}
}