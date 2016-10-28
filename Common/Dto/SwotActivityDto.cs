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
        [AllowHtml]
		public string Description { get; set; }
		public DateTime Date { get; set; }
	}
}
