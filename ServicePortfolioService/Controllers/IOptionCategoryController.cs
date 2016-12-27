using Common.Dto;
using Common.Enums;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface IOptionCategoryController : IUserController
	{
        /// <summary>
        /// Finds option category with identifier provided and returns its DTO
        /// </summary>
        /// <param name="optionCategoryId"></param>
        /// <returns></returns>
        IOptionCategoryDto GetOptionCategory(int optionCategoryId);

        /// <summary>
        /// Modifies the option category in the database
        /// </summary>
        /// <param name="optionCategory"></param>
        /// <param name="modification">Type of modification to make</param>
        /// <returns>Modified Service WorkUnit</returns>
        IOptionCategoryDto ModifyOptionCategory(IOptionCategoryDto optionCategory, EntityModification modification);
	}
}