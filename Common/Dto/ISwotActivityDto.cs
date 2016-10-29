using System;

namespace Common.Dto
{
	public interface ISwotActivityDto : IUserCreatedEntityDto
	{
		DateTime Date { get; set; }
		string Description { get; set; }
		int Id { get; set; }
		string Name { get; set; }
	}
}