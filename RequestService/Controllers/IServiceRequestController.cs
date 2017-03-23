using System.Collections.Generic;
using Common.Dto;
using Common.Enums.Entities;

namespace RequestService.Controllers
{
	public interface IServiceRequestController
	{
		/// <summary>
		/// Returns all service requests
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <returns></returns>
		IEnumerable<IServiceRequestDto> GetServiceRequests(int performingUserId);

		/// <summary>
		/// Finds service request with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceRequestId"></param>
		/// <returns></returns>
		IServiceRequestDto GetServiceRequest(int performingUserId, int serviceRequestId);

		/// <summary>
		/// Modifies the service request in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceRequest"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Request</returns>
		IServiceRequestDto ModifyServiceRequest(int performingUserId, IServiceRequestDto serviceRequest, EntityModification modification);

		/// <summary>
		/// Retrieves the service requests that the user provided is the requestor for
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="requestorUserId"></param>
		/// <returns></returns>
		IEnumerable<IServiceRequestDto> GetServiceRequestsForRequestorId(int performingUserId, int requestorUserId);

		/// <summary>
		/// Retrieves the service requests for an approver based on their department
		/// </summary>
		/// <param name="approverId">ID of approver</param>
		/// <returns></returns>
		IEnumerable<IServiceRequestDto> GetServiceRequestsForApproverId(int approverId);
	}
}