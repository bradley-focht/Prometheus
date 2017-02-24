using Common.Dto;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface IServiceWorkUnitController
	{
		/// <summary>
		/// Finds service WorkUnit with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceWorkUnitId"></param>
		/// <returns></returns>
		IServiceWorkUnitDto GetServiceWorkUnit(int performingUserId, int serviceWorkUnitId);

		/// <summary>
		/// Modifies the service WorkUnit in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceWorkUnit"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service WorkUnit</returns>
		IServiceWorkUnitDto ModifyServiceWorkUnit(int performingUserId, IServiceWorkUnitDto serviceWorkUnit, EntityModification modification);
	}
}