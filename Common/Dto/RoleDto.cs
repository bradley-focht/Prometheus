using System;
using System.ComponentModel.DataAnnotations;
using Common.Enums.Permissions;

namespace Common.Dto
{
	/// <summary>
	/// Role with a set of permission levels applied to it that a User can have
	/// </summary>
	public class RoleDto : IRoleDto
	{
		//PK
		public int Id { get; set; }

		#region Fields
		/// <summary>
		/// Name of the Role
		/// </summary>
		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		#endregion


		/// <summary>
		/// Access Level for using the REST API
		/// </summary>
		[Display(Order = 1)]
		public ApiAccess ApiAccess { get; set; }

		/// <summary>
		/// Access Level for Approving Service Requests
		/// </summary>
		[Display(Order = 2)]
		public ApproveServiceRequest ApproveServiceRequestAccess { get; set; }

		/// <summary>
		/// Access Level for the Business Catalog
		/// </summary>
		[Display(Order = 3)]
		public BusinessCatalog BusinessCatalogAccess { get; set; }

		/// <summary>
		/// Access Level for Fulfillment of Service Requests
		/// </summary>
		[Display(Order = 4)]
		public FulfillmentAccess FulfillmentAccess { get; set; }

		/// <summary>
		/// Access Level for adjusting the permissions on Roles
		/// </summary>
		[Display(Order = 4)]
		public RolePermissionAdjustment RolePermissionAdjustmentAccess { get; set; }

		/// <summary>
		/// Access Level for Scripting
		/// </summary>
		[Display(Order = 5)]
		public ScriptAccess ScriptAccess { get; set; }

		/// <summary>
		/// Access Level for maintaining the Service Catalog
		/// </summary>
		[Display(Order = 6)]
		public ServiceCatalogMaintenance ServiceCatalogMaintenanceAccess { get; set; }

		/// <summary>
		/// Access Level for Service Details
		/// </summary>
		[Display(Order = 7)]
		public ServiceDetails ServiceDetailsAccess { get; set; }

		/// <summary>
		/// Access Level for the Service Portfolio
		/// </summary>
		[Display(Order = 8)]
		public ServicePortfolio ServicePortfolioAccess { get; set; }

		/// <summary>
		/// Access Level for submitting Service Requests
		/// </summary>
		[Display(Order = 9)]
		public ServiceRequestSubmission ServiceRequestSubmissionAccess { get; set; }

		/// <summary>
		/// Access Level for the Support Catalog
		/// </summary>
		[Display(Order = 10)]
		public SupportCatalog SupportCatalogAccess { get; set; }

		/// <summary>
		/// Access Level for assigning Roles to Users
		/// </summary>
		[Display(Order = 11)]
		public UserRoleAssignment UserRoleAssignmentAccess { get; set; }
	}
}
