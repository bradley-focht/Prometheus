using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DataService.Models
{
	public class LifecycleStatus : ILifecycleStatus
	{
		[Key]
		[HiddenInput(DisplayValue = false)]
		public Guid? Id { get; set; }

		[Required(ErrorMessage ="name required")]
		public string Name { get; set; }
		public string Comment { get; set; }
		public int Position { get; set; }
		
		[Display(Name="Catalog Visible")]
		[Required(ErrorMessage = "Selection required")]
		public bool CatalogVisible { get; set; }
	}
}
