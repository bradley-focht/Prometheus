using Common.Dto;
using Common.Enums.Entities;
using System.Collections.Generic;

namespace ServicePortfolioService.Controllers
{
	public interface IServiceRequestPackageController : IUserController
	{
		/// <summary>
		/// Finds service package with identifier provided and returns its DTO
		/// </summary>
		/// <param name="servicePackageId"></param>
		/// <returns></returns>
		IServiceRequestPackageDto GetServiceRequestPackage(int servicePackageId);

		/// <summary>
		/// Modifies the service Package in the database
		/// </summary>
		/// <param name="servicePackage"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service WorkUnit</returns>
		IServiceRequestPackageDto ModifyServiceRequestPackage(IServiceRequestPackageDto servicePackage, EntityModification modification);

		/// <summary>
		/// All of the Service Request Packages in the database
		/// </summary>
		IEnumerable<IServiceRequestPackageDto> AllServiceRequestPackages { get; }

		/// <summary>
		/// Retrieves the service package that the service option id exists in
		/// </summary>
		/// <param name="serviceOptionId"></param>
		/// <returns></returns>
		IServiceRequestPackageDto GetServiceRequestPackageForServiceOption(int serviceOptionId);
	}
}