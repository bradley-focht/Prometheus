using System;
using System.Collections.Generic;

namespace Common.Dto
{
	/// <summary>
	/// User in the Prometheus system
	/// </summary>
	public interface IUserDto : IUserCreatedEntityDto
	{
		int Id { get; set; }

		/// <summary>
		/// ID for the department the User belongs to
		/// </summary>
		int DepartmentId { get; set; }

		/// <summary>
		/// AD Name of User on creation
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Identifier for the User in Active Directory
		/// </summary>
		Guid AdGuid { get; set; }

		IDepartmentDto Department { get; set; }
		ICollection<IRoleDto> Roles { get; set; }
	}
}