using Common.Dto;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface IServiceOptionCategoryController : IUserController
	{
		/// <summary>
		/// Finds option category with identifier provided and returns its DTO
		/// </summary>
		/// <param name="optionCategoryId"></param>
		/// <returns></returns>
		IServiceOptionCategoryDto GetServiceOptionCategory(int optionCategoryId);

		/// <summary>
		/// Modifies the option category in the database NEW COMMIT
		/// </summary>
		/// <param name="optionCategory"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service WorkUnit</returns>
		IServiceOptionCategoryDto ModifyServiceOptionCategory(IServiceOptionCategoryDto optionCategory, EntityModification modification);
	}
}