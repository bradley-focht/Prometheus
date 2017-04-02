using System.Collections.Generic;

namespace Common.Dto
{
	/// <summary>
	/// Service Request Queue Associated with users
	/// </summary>
	public interface IDepartmentDto : IUserCreatedEntityDto
	{
		int Id { get; set; }

		/// <summary>
		/// Queue name to match script result
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Users that belong to the Department
		/// </summary>
		ICollection<IUserDto> Users { get; set; }
	}
}