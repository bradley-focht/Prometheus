using System;
using System.Collections.Generic;

namespace Common.Dto
{
	public class RoleDto : IRoleDto
	{
		//PK
		public int Id { get; set; }

		#region Fields
		public string Name { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		#endregion

		#region Navigation properties
		public virtual ICollection<IUserDto> Users { get; set; }
		#endregion
	}
}
