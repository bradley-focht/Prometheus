using Common.Enums;

namespace Common.Dto
{
	public interface IApprovalDto:IUserCreatedEntityDto
	{
		int ServiceRequestId { get; set; }
		int ApproverId { get; set; }
		int RequestorId { get; set; }
		ApprovalResult Result { get; set; }
		string Comments { get; set; }
	}
}