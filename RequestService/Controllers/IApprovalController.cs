using Common.Dto;
using Common.Enums.Entities;

namespace RequestService.Controllers
{
	public interface IApprovalController
	{
		/// <summary>
		/// Finds approval with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="approvalId"></param>
		/// <returns></returns>
		IApprovalDto GetApproval(int performingUserId, int approvalId);

		/// <summary>
		/// Modifies the approval in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="approval"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Approval</returns>
		IApprovalDto ModifyApproval(int performingUserId, IApprovalDto approval, EntityModification modification);
	}
}
