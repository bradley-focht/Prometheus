using Common.Enums;
using System.Collections.Generic;

namespace Common.Dto
{
	public class ServiceSwotDto : IServiceSwotDto
	{
		public int Id { get; set; }
		//this is either Strength, Weakness, Opportunity, or Threat
		// might be best as an enum?
		public ServiceSwotType Type { get; set; }

		public string Description { get; set; }

		public ICollection<ISwotActivityDto> SwotActivities { get; set; }
	}
}
