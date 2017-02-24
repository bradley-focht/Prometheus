using Common.Dto;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface IServiceGoalController
	{
		/// <summary>
		/// Finds service goal with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceGoalId"></param>
		/// <returns></returns>
		IServiceGoalDto GetServiceGoal(int performingUserId, int serviceGoalId);

		/// <summary>
		/// Modifies the service goal in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceGoal"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Goal</returns>
		IServiceGoalDto ModifyServiceGoal(int performingUserId, IServiceGoalDto serviceGoal, EntityModification modification);
	}
}