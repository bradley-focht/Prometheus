using System;
using System.Collections.Generic;

namespace Common.Dto
{
	public interface IUserDto : IUserCreatedEntityDto
	{
		int Id { get; set; }
		int DepartmentId { get; set; }
		string Name { get; set; }
		Guid AdGuid { get; set; }

		IDepartmentDto Department { get; set; }
		ICollection<IRoleDto> Roles { get; set; }
	}
}