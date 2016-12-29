using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Enums.Permissions;

namespace DataService.Models
{
	public class Role : IRole
	{
		//PK
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		#region Fields
		public string Name { get; set; }
		public ApproveServiceRequest ApproveServiceRequestAccess { get; set; }
		public BusinessCatalog BusinessCatalogAccess { get; set; }
		public SupportCatalog SupportCatalogAccess { get; set; }
		public RolePermissionAdustment RolePermissionAdjustmentAccess { get; set; }
		public ServiceDetails ServiceDetailsAccess { get; set; }
		public ServiceRequestSubmission ServiceRequestSubmissionAccess { get; set; }
		public UserRoleAssignment UserRoleAssignmentAccess { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		#endregion
		#region Navigation properties
		public virtual ICollection<User> Users { get; set; }

		#endregion
	}
}
