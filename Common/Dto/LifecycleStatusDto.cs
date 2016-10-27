using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
	public class LifecycleStatusDto : ILifecycleStatusDto
	{
		//PK
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		//Fields
        //unique name of each status
		[Required(ErrorMessage = "Lifecycle Status: Name required")]
		public string Name { get; set; }

        //optional comment about the intended use of the status
        public string Comment { get; set; }

        //used for the sort order when displaying statuses
        [Required(ErrorMessage = "Lifecycle position is required")]
        public int Position { get; set; }

        //attribute to decide if services with this status will be visible in business/support catalog
		[Display(Name = "Catalog Visible")]
		[Required(ErrorMessage = "Catalog Visibility selection required")]
		public bool CatalogVisible { get; set; }
	}
}
