using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Enums;

namespace DataService.Models
{
	public class Approval : IApproval
	{
		//PK
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		//FK
		public int ServiceRequestId { get; set; }

		#region Fields
		public int ApproverId { get; set; }
		public int RequestorId { get; set; }
		public ApprovalResult Result { get; set; }
		public string Comments { get; set; }

		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		#endregion

		#region Navigation properties
		public virtual ServiceRequest ServiceRequest{ get; set; }
		#endregion
	}
}
