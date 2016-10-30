using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Models
{
	public class LifecycleStatus : ILifecycleStatus
	{
		//PK
		[Key]
		public int Id { get; set; }

		//FK
		public int ServiceId { get; set; }

		//TODO: Brad document what the fields in this entity do. Ideally we should have comments in the Model interfaces for ALL fields
		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		[Required(ErrorMessage = "Lifecycle Status: Name required")]
		public string Name { get; set; }
		public string Comment { get; set; }
		public int Position { get; set; }

		[Display(Name = "Catalog Visible")]
		[Required(ErrorMessage = "Catalog Visible: Selection required")]
		public bool CatalogVisible { get; set; }
		#endregion
		#region Navigation Properties
		public virtual IService Service { get; set; }
		#endregion
	}
}
