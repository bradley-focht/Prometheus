using Common.Dto;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface IServiceProcessController : IUserController
	{
		/// <summary>
		/// Finds service process with identifier provided and returns its DTO
		/// </summary>
		/// <param name="serviceProcessId"></param>
		/// <returns></returns>
		IServiceProcessDto GetServiceProcess(int serviceProcessId);

		/// <summary>
		/// Modifies the service process in the database
		/// </summary>
		/// <param name="serviceProcess"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service WorkUnit</returns>
		IServiceProcessDto ModifyServiceProcess(IServiceProcessDto serviceProcess, EntityModification modification);
	}
}