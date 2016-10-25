using System;

namespace Common.Dto
{
	public class UserDto : IUserDto
	{
		//PK
		public int Id { get; set; }

		//FK
		public int RoleId { get; set; }

		//Fields
		public string Name { get; set; }
		public string Password { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		//Navigation properties
		public virtual IRoleDto Role { get; set; }
	}
}
