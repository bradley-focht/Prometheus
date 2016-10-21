using System;

namespace ServicePortfolio.Dto
{
	public interface ICreatedEntityDto
	{
		DateTime? DateCreated { get; set; }
		DateTime? DateUpdated { get; set; }
	}
}
