using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	/// <summary>
	/// Service Request Queue Associated with users
	/// </summary>
	public class Department : IDepartment
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }


		/// <summary>
		/// Queue name to match script result
		/// </summary>
		public string Name { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }


		/// <summary>
		/// Users that belong to the Department
		/// </summary>
		public virtual ICollection<User> Users { get; set; }
		public virtual ICollection<ServiceRequest> ServiceRequests { get; set; }
	}
}
