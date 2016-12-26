using Common.Dto;
using Common.Enums;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface IServiceContractController : IUserController
	{
		/// <summary>
		/// Finds service Contract with identifier provided and returns its DTO
		/// </summary>
		/// <param name="serviceContractId"></param>
		/// <returns></returns>
		IServiceContractDto GetServiceContract(int serviceContractId);

		/// <summary>
		/// Modifies the service Contract in the database
		/// </summary>
		/// <param name="serviceContract"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Contract</returns>
		IServiceContractDto ModifyServiceContract(IServiceContractDto serviceContract, EntityModification modification);
	}
}