using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace Common.Dto 
{
    public class OptionCategoryDto : IOptionCategoryDto
    {
		[HiddenInput]
        public int Id { get; set; }
        [Required(ErrorMessage = "Popularity is required")]
        public int Popularity { get; set; }
        public int ServiceId { get; set; }
		[Display(Order = 2)]
		[AllowHtml]
		public string Features { get; set; }
		[Display(Order = 3)]
		[AllowHtml]
		public string Benefits { get; set; }
		[Display(Order = 4)]
		[AllowHtml]
		public string Support { get; set; }

		[Display(Order = 6)]
		[AllowHtml]
	    public string Description { get; set; }

	    [Required(ErrorMessage = "Name is required")]
		[Display(Order = 1)]
        public string Name { get; set; }
        [AllowHtml]
		[Display(Order = 5, Name = "Business Value")]
		public string BusinessValue { get; set; }
		[Display(Order=7)]
        public virtual ICollection<ServiceOptionDto> ServiceOptions { get; set; }
    }
}
