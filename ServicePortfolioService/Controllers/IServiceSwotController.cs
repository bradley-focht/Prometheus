using Common.Dto;
using Common.Enums;

namespace ServicePortfolioService.Controllers
{
	public interface IServiceSwotController : IUserController
	{
		/// <summary>
		/// Finds service SWOT with identifier provided and returns its DTO
		/// </summary>
		/// <param name="serviceSwotId"></param>
		/// <returns></returns>
		IServiceSwotDto GetServiceSwot(int serviceSwotId);

		/// <summary>
		/// Modifies the service SWOT in the database
		/// </summary>
		/// <param name="serviceSwot"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified SWOT</returns>
		IServiceSwotDto ModifyServiceSwot(IServiceSwotDto serviceSwot, EntityModification modification);
	}
}
