using Common.Dto;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface IServiceOptionCategoryController
	{
		/// <summary>
		/// Finds option category with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="optionCategoryId"></param>
		/// <returns></returns>
		IServiceOptionCategoryDto GetServiceOptionCategory(int performingUserId, int optionCategoryId);

		/// <summary>
		/// Modifies the option category in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="optionCategory"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Option Category</returns>
		IServiceOptionCategoryDto ModifyServiceOptionCategory(int performingUserId, IServiceOptionCategoryDto optionCategory, EntityModification modification);
	}
}