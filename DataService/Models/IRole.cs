using System.Collections.Generic;
using Common.Enums.Permissions;

namespace DataService.Models
{
	public interface IRole : IUserCreatedEntity
	{
		int Id { get; set; }
		string Name { get; set; }
		ApproveServiceRequest ApproveServiceRequestAccess { get; set; }
		BusinessCatalog BusinessCatalogAccess { get; set; }
		SupportCatalog SupportCatalogAccess { get; set; }
		RolePermissionAdjustment RolePermissionAdjustmentAccess { get; set; }
		ServiceDetails ServiceDetailsAccess { get; set; }
		ServiceRequestSubmission ServiceRequestSubmissionAccess { get; set; }
		UserRoleAssignment UserRoleAssignmentAccess { get; set; }
		ServiceCatalogMaintenance ServiceCatalogMaintenanceAccess { get; set; }
		ServicePortfolio ServicePortfolioAccess { get; set; }
		ScriptAccess ScriptAccess { get; set; }
		FulfillmentAccess FulfillmentAccess { get; set; }
		ApiAccess ApiAccess { get; set; }

		ICollection<User> Users { get; set; }
	}
}