using System;

namespace Common.Dto
{
	public class UserDto : IUserDto
	{
		//PK
		public int Id { get; set; }

		#region Fields
		public string Name { get; set; }
		public Guid AdGuid { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		#endregion

		#region Navigation properties

		#endregion
	}
}
