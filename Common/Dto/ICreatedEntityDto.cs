using System;

namespace Common.Dto
{
	public interface ICreatedEntityDto
	{
		DateTime? DateCreated { get; set; }
		DateTime? DateUpdated { get; set; }
	}
}
