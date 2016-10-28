using System;
using System.Web.Mvc;

namespace Common.Dto
{
	public class SwotActivityDto : ISwotActivityDto
	{
        [HiddenInput]
		public int Id { get; set; }
        //this is a title for the item
        public string Name { get; set; }

        //optional extra text
        [AllowHtml]
		public string Description { get; set; }

        //date the activity took place on, or start date for multi-day events
        public DateTime Date { get; set; }
	}
}
