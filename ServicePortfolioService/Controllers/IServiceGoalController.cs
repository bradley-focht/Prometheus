using Common.Dto;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface IServiceGoalController : IUserController
	{
		/// <summary>
		/// Finds service goal with identifier provided and returns its DTO
		/// </summary>
		/// <param name="serviceGoalId"></param>
		/// <returns></returns>
		IServiceGoalDto GetServiceGoal(int serviceGoalId);

		/// <summary>
		/// Modifies the service goal in the database
		/// </summary>
		/// <param name="serviceGoal"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Goal</returns>
		IServiceGoalDto ModifyServiceGoal(IServiceGoalDto serviceGoal, EntityModification modification);
	}
}