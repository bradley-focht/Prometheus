using Common.Dto;
using Common.Enums.Entities;
using System.Collections.Generic;

namespace ServicePortfolioService.Controllers
{
	public interface IServiceRequestController : IUserController
	{
		/// <summary>
		/// Finds service request with identifier provided and returns its DTO
		/// </summary>
		/// <param name="serviceRequestId"></param>
		/// <returns></returns>
		IServiceRequestDto GetServiceRequest(int serviceRequestId);

		/// <summary>
		/// Modifies the service request in the database
		/// </summary>
		/// <param name="serviceRequest"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Request</returns>
		IServiceRequestDto ModifyServiceRequest(IServiceRequestDto serviceRequest, EntityModification modification);

		/// <summary>
		/// Retrieves the service requests that the user provided is the requestor for
		/// </summary>
		/// <param name="requestorUserId"></param>
		/// <returns></returns>
		IEnumerable<IServiceRequestDto> GetServiceRequestsForRequestorId(int requestorUserId);
	}
}