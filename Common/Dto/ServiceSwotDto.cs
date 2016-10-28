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
        
        //this is a title of the item
        [Display(Order=1)]
        public string Item { get; set; }

        //either strength, weakness, opportunity, threat
        [Display(Order=2)]
        public ServiceSwotType Type { get; set; }

        //details of items
        [AllowHtml]
        [Display(Order=3)]
		public string Description { get; set; }

        //activities for continuous performance
        [Display(Name="Activities", Order=4)]
		public ICollection<ISwotActivityDto> SwotActivities { get; set; }
	}
}
