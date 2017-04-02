using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	/// <summary>
	/// User in the Prometheus system
	/// </summary>
	public class User : IUser
	{
		//PK
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		//FK
		/// <summary>
		/// ID for the department the User belongs to
		/// </summary>
		public int DepartmentId { get; set; }

		#region Fields
		/// <summary>
		/// AD Name of User on creation
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Identifier for the User in Active Directory
		/// </summary>
		public Guid AdGuid { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		#endregion

		#region Navigation properties
		public virtual Department Department { get; set; }
		public virtual ICollection<Role> Roles { get; set; }
		#endregion
	}
}
