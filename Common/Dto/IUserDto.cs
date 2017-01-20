using System;
using System.Collections.Generic;

namespace Common.Dto
{
	public interface IUserDto : IUserCreatedEntityDto
	{
		int Id { get; set; }
		string Name { get; set; }
		Guid AdGuid { get; set; }
        ICollection<RoleDto> Roles { get; set; }
    }
}