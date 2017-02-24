using Common.Dto;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface IServiceContractController
	{
		/// <summary>
		/// Finds service Contract with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceContractId"></param>
		/// <returns></returns>
		IServiceContractDto GetServiceContract(int performingUserId, int serviceContractId);

		/// <summary>
		/// Modifies the service Contract in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceContract"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Contract</returns>
		IServiceContractDto ModifyServiceContract(int performingUserId, IServiceContractDto serviceContract, EntityModification modification);
	}
}