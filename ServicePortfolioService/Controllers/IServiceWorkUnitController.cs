using Common.Dto;
using Common.Enums;

namespace ServicePortfolioService.Controllers
{
	public interface IServiceWorkUnitController : IUserController
	{
		/// <summary>
		/// Finds service WorkUnit with identifier provided and returns its DTO
		/// </summary>
		/// <param name="serviceWorkUnitId"></param>
		/// <returns></returns>
		IServiceWorkUnitDto GetServiceWorkUnit(int serviceWorkUnitId);

		/// <summary>
		/// Modifies the service WorkUnit in the database
		/// </summary>
		/// <param name="serviceWorkUnit"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service WorkUnit</returns>
		IServiceWorkUnitDto ModifyServiceWorkUnit(IServiceWorkUnitDto serviceWorkUnit, EntityModification modification);
	}
}