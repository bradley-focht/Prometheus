using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace Common.Dto 
{
    public class OptionCategoryDto : IOptionCategoryDto, ICatalogable
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Popularity is required")]
        public int Popularity { get; set; }
        public int ServiceId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public virtual ICollection<int> ServiceOptionIds { get; set; }
    }
}
