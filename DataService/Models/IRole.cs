using System.Collections.Generic;
using Common.Enums.Permissions;

namespace DataService.Models
{
	/// <summary>
	/// Role with a set of permission levels applied to it that a User can have
	/// </summary>
	public interface IRole : IUserCreatedEntity
	{
		int Id { get; set; }

		/// <summary>
		/// Name of the Role
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Access Level for Approving Service Requests
		/// </summary>
		ApproveServiceRequest ApproveServiceRequestAccess { get; set; }

		/// <summary>
		/// Access Level for the Business Catalog
		/// </summary>
		BusinessCatalog BusinessCatalogAccess { get; set; }

		/// <summary>
		/// Access Level for the Support Catalog
		/// </summary>
		SupportCatalog SupportCatalogAccess { get; set; }

		/// <summary>
		/// Access Level for adjusting the permissions on Roles
		/// </summary>
		RolePermissionAdjustment RolePermissionAdjustmentAccess { get; set; }

		/// <summary>
		/// Access Level for Service Details
		/// </summary>
		ServiceDetails ServiceDetailsAccess { get; set; }

		/// <summary>
		/// Access Level for submitting Service Requests
		/// </summary>
		ServiceRequestSubmission ServiceRequestSubmissionAccess { get; set; }

		/// <summary>
		/// Access Level for assigning Roles to Users
		/// </summary>
		UserRoleAssignment UserRoleAssignmentAccess { get; set; }

		/// <summary>
		/// Access Level for maintaining the Service Catalog
		/// </summary>
		ServiceCatalogMaintenance ServiceCatalogMaintenanceAccess { get; set; }

		/// <summary>
		/// Access Level for the Service Portfolio
		/// </summary>
		ServicePortfolio ServicePortfolioAccess { get; set; }

		/// <summary>
		/// Access Level for Scripting
		/// </summary>
		ScriptAccess ScriptAccess { get; set; }

		/// <summary>
		/// Access Level for Fulfillment of Service Requests
		/// </summary>
		FulfillmentAccess FulfillmentAccess { get; set; }

		/// <summary>
		/// Access Level for using the REST API
		/// </summary>
		ApiAccess ApiAccess { get; set; }

		/// <summary>
		/// Users with this Role applied to them
		/// </summary>
		ICollection<User> Users { get; set; }
	}
}