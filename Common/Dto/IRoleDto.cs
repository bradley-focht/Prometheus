using Common.Enums.Permissions;

namespace Common.Dto
{
	public interface IRoleDto : IUserCreatedEntityDto
	{
		int Id { get; set; }
		string Name { get; set; }

		ApproveServiceRequest ApproveServiceRequestAccess { get; set; }
		BusinessCatalog BusinessCatalogAccess { get; set; }
		SupportCatalog SupportCatalogAccess { get; set; }
		RolePermissionAdustment RolePermissionAdjustmentAccess { get; set; }
		ServiceDetails ServiceDetailsAccess { get; set; }
		ServiceRequestSubmission ServiceRequestSubmissionAccess { get; set; }
		UserRoleAssignment UserRoleAssignmentAccess { get; set; }
	}
}