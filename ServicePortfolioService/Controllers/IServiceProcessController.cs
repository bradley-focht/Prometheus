using Common.Dto;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface IServiceProcessController
	{
		/// <summary>
		/// Finds service process with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceProcessId"></param>
		/// <returns></returns>
		IServiceProcessDto GetServiceProcess(int performingUserId, int serviceProcessId);

		/// <summary>
		/// Modifies the service process in the database
		/// </summary>
		/// <param name="performingUserId">ID of user performing modification</param>
		/// <param name="serviceProcess"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Process</returns>
		IServiceProcessDto ModifyServiceProcess(int performingUserId, IServiceProcessDto serviceProcess, EntityModification modification);
	}
}