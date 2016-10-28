using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Common.Enums;


namespace Common.Dto
{

	public class ServiceGoalDto : IServiceGoalDto
	{
        [HiddenInput]
		public int Id { get; set; }
        //Uniquie descriptive name
        [Display(Order=1)]
        public string Name { get; set; }

        //extra text for those who like to talk
        [AllowHtml]
        public string Description { get; set; }

        //identify if short term or long term
        public ServiceGoalType Type { get; set; }

        [Display(Name="Start Date")]
        public DateTime? StartDate { get; set; }

        [Display(Name="End Date")]
        public DateTime? EndDate { get; set; }
	}
}
