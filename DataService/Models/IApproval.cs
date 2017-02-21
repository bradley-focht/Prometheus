using Common.Enums;

namespace DataService.Models
{
	public interface IApproval: IUserCreatedEntity
	{
		int Id { get; set; }
		int ServiceRequestId { get; set; }

		int ApproverId { get; set; }
		int RequestorId { get; set; }
		ApprovalResult Result { get; set; }
		string Comments { get; set; }

		ServiceRequest ServiceRequest { get; set; }
	}
}