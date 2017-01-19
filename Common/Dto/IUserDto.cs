using System;

namespace Common.Dto
{
	public interface IUserDto : IUserCreatedEntityDto
	{
		int Id { get; set; }
		string Name { get; set; }
		Guid AdGuid { get; set; }
	}
}