using Common.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
	public class ServiceSwotDto : IServiceSwotDto
	{
        [HiddenInput]
		public int Id { get; set; }
        
        //either strength, weakness, opportunity, threat
        [Display(Order=1)]
        public ServiceSwotType Type { get; set; }

        //details of items
        [AllowHtml]
        [Display(Order=2)]
		public string Description { get; set; }

        //activities for continuous performance
        [Display(Name="Activities", Order=3)]
		public ICollection<ISwotActivityDto> SwotActivities { get; set; }
	}
}
