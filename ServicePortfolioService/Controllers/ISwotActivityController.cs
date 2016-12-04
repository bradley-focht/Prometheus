using Common.Dto;
using Common.Enums;

namespace ServicePortfolioService.Controllers
{
	public interface ISwotActivityController : IUserController
	{
		/// <summary>
		/// Finds SWOT activity with identifier provided and returns its DTO
		/// </summary>
		/// <param name="swotActivityId"></param>
		/// <returns></returns>
		ISwotActivityDto GetSwotActivity(int swotActivityId);

		/// <summary>
		/// Modifies the SWOT activity in the database
		/// </summary>
		/// <param name="swotActivity"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified SWOT</returns>
		ISwotActivityDto ModifySwotActivity(ISwotActivityDto swotActivity, EntityModification modification);
	}
}
