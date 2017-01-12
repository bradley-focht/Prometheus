using System;
using System.Collections.Generic;
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

		#region Navigation properties
		public ApproveServiceRequest ApproveServiceRequestAccess { get; set; }
		public BusinessCatalog BusinessCatalogAccess { get; set; }
		public SupportCatalog SupportCatalogAccess { get; set; }
		public RolePermissionAdustment RolePermissionAdjustmentAccess { get; set; }
		public ServiceDetails ServiceDetailsAccess { get; set; }
		public ServiceRequestSubmission ServiceRequestSubmissionAccess { get; set; }
		public UserRoleAssignment UserRoleAssignmentAccess { get; set; }
		#endregion
	}
}
