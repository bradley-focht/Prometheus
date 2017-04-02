using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Enums.Permissions;

namespace DataService.Models
{
	/// <summary>
	/// Role with a set of permission levels applied to it that a User can have
	/// </summary>
	public class Role : IRole
	{
		//PK
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		#region Fields
		/// <summary>
		/// Name of the Role
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Access Level for Approving Service Requests
		/// </summary>
		public ApproveServiceRequest ApproveServiceRequestAccess { get; set; }

		/// <summary>
		/// Access Level for the Business Catalog
		/// </summary>
		public BusinessCatalog BusinessCatalogAccess { get; set; }

		/// <summary>
		/// Access Level for the Support Catalog
		/// </summary>
		public SupportCatalog SupportCatalogAccess { get; set; }

		/// <summary>
		/// Access Level for adjusting the permissions on Roles
		/// </summary>
		public RolePermissionAdjustment RolePermissionAdjustmentAccess { get; set; }

		/// <summary>
		/// Access Level for Service Details
		/// </summary>
		public ServiceDetails ServiceDetailsAccess { get; set; }

		/// <summary>
		/// Access Level for submitting Service Requests
		/// </summary>
		public ServiceRequestSubmission ServiceRequestSubmissionAccess { get; set; }

		/// <summary>
		/// Access Level for assigning Roles to Users
		/// </summary>
		public UserRoleAssignment UserRoleAssignmentAccess { get; set; }

		/// <summary>
		/// Access Level for maintaining the Service Catalog
		/// </summary>
		public ServiceCatalogMaintenance ServiceCatalogMaintenanceAccess { get; set; }

		/// <summary>
		/// Access Level for the Service Portfolio
		/// </summary>
		public ServicePortfolio ServicePortfolioAccess { get; set; }

		/// <summary>
		/// Access Level for Scripting
		/// </summary>
		public ScriptAccess ScriptAccess { get; set; }

		/// <summary>
		/// Access Level for Fulfillment of Service Requests
		/// </summary>
		public FulfillmentAccess FulfillmentAccess { get; set; }

		/// <summary>
		/// Access Level for using the REST API
		/// </summary>
		public ApiAccess ApiAccess { get; set; }

		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		#endregion
		#region Navigation properties
		/// <summary>
		/// Users with this Role applied to them
		/// </summary>
		public virtual ICollection<User> Users { get; set; }

		#endregion
	}
}
