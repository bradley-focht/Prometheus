using Common.Dto;
using Common.Enums.Entities;

namespace RequestService.Controllers
{
	public interface IServiceRequestOptionController
	{
		/// <summary>
		/// Finds service request option with identifier provided and returns its DTO
		/// </summary>
		/// <param name="optionId"></param>
		/// <returns></returns>
		IServiceRequestOptionDto GetServiceRequestOption(int optionId);

		/// <summary>
		/// Modifies the service request option in the database
		/// </summary>
		/// <param name="requestOption"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Request Option</returns>
		IServiceRequestOptionDto ModifyServiceRequestOption(IServiceRequestOptionDto requestOption, EntityModification modification);
	}
}
