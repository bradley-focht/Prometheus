using System;
using System.ComponentModel.DataAnnotations;
using Common.Enums.Permissions;

namespace Common.Dto
{
	public class RoleDto : IRoleDto
	{
		//PK
		public int Id { get; set; }

		#region Fields
		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		#endregion

		[Display(Order = 1)]
		public ApiAccess ApiAccess { get; set; }
		[Display(Order = 2)]
		public ApproveServiceRequest ApproveServiceRequestAccess { get; set; }
		[Display(Order = 3)]
		public BusinessCatalog BusinessCatalogAccess { get; set; }
		[Display(Order = 4)]
		public FulfillmentAccess FulfillmentAccess { get; set; }
		[Display(Order = 4)]
		public RolePermissionAdjustment RolePermissionAdjustmentAccess { get; set; }
		[Display(Order = 5)]
		public ScriptAccess ScriptAccess { get; set; }
		[Display(Order = 6)]
		public ServiceCatalogMaintenance ServiceCatalogMaintenanceAccess { get; set; }
		[Display(Order = 7)]
		public ServiceDetails ServiceDetailsAccess { get; set; }
		[Display(Order = 8)]
		public ServicePortfolio ServicePortfolioAccess { get; set; }
		[Display(Order = 9)]
		public ServiceRequestSubmission ServiceRequestSubmissionAccess { get; set; }
		[Display(Order = 10)]
		public SupportCatalog SupportCatalogAccess { get; set; }
		[Display(Order = 11)]
		public UserRoleAssignment UserRoleAssignmentAccess { get; set; }







	}
}
