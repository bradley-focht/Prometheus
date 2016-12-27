using Common.Dto;
using Common.Enums;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface IServiceOptionController : IUserController
	{
        /// <summary>
        /// Finds option with identifier provided and returns its DTO
        /// </summary>
        /// <param name="serviceOptionId"></param>
        /// <returns></returns>
        IServiceOptionDto GetServiceOption(int serviceOptionId);

        /// <summary>
        /// Modifies the option in the database
        /// </summary>
        /// <param name="serviceOption"></param>
        /// <param name="modification">Type of modification to make</param>
        /// <returns>Modified SWOT</returns>
        IServiceOptionDto ModifyServiceOption(IServiceOptionDto serviceOption, EntityModification modification);
	}
}
