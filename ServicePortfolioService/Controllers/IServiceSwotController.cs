using Common.Dto;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface IServiceSwotController
	{
		/// <summary>
		/// Finds service SWOT with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceSwotId"></param>
		/// <returns></returns>
		IServiceSwotDto GetServiceSwot(int performingUserId, int serviceSwotId);

		/// <summary>
		/// Modifies the service SWOT in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceSwot"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified SWOT</returns>
		IServiceSwotDto ModifyServiceSwot(int performingUserId, IServiceSwotDto serviceSwot, EntityModification modification);
	}
}
