using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
	public class LifecycleStatusDto : ILifecycleStatusDto
	{
		//PK
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		//TODO: Brad document what the fields in this entity do. Ideally we should have comments in the Model interfaces for ALL fields
		//Fields
		[Required(ErrorMessage = "Lifecycle Status: Name required")]
		public string Name { get; set; }
		public string Comment { get; set; }
		public int Position { get; set; }

		[Display(Name = "Catalog Visible")]
		[Required(ErrorMessage = "Catalog Visible: Selection required")]
		public bool CatalogVisible { get; set; }
	}
}
