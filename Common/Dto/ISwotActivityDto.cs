using System;

namespace Common.Dto
{
	public interface ISwotActivityDto : IUserCreatedEntityDto
	{
		int Id { get; set; }
		int ServiceSwotId { get; set; }

		DateTime Date { get; set; }
		string Description { get; set; }
		string Name { get; set; }
	}
}