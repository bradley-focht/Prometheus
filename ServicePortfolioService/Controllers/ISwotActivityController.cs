using Common.Dto;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface ISwotActivityController
	{
		/// <summary>
		/// Finds SWOT activity with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="swotActivityId"></param>
		/// <returns></returns>
		ISwotActivityDto GetSwotActivity(int performingUserId, int swotActivityId);

		/// <summary>
		/// Modifies the SWOT activity in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="swotActivity"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified SWOT activity</returns>
		ISwotActivityDto ModifySwotActivity(int performingUserId, ISwotActivityDto swotActivity, EntityModification modification);
	}
}
