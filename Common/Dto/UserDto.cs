using System;
using System.Collections.Generic;

namespace Common.Dto
{
	/// <summary>
	/// User in the Prometheus system
	/// </summary>
	public class UserDto : IUserDto
	{
		//PK
		public int Id { get; set; }

		#region Fields
		/// <summary>
		/// ID for the department the User belongs to
		/// </summary>
		public int DepartmentId { get; set; }

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
		public IDepartmentDto Department { get; set; }
		public ICollection<IRoleDto> Roles { get; set; }
		#endregion
	}
}
