using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
	public class LifecycleStatusDto : ILifecycleStatusDto
	{
		//PK
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		//FK
		public int ServiceId { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		/// <summary>
		/// Unique name of each status
		/// </summary>
		[Required(ErrorMessage = "Lifecycle Status: Name required")]
		public string Name { get; set; }

		/// <summary>
		/// Optional comment about the intended use of the status
		/// </summary>
		public string Comment { get; set; }

		/// <summary>
		/// Used for the sort order when displaying statuses
		/// </summary>
		[Required(ErrorMessage = "Lifecycle position is required")]
		public int Position { get; set; }

		/// <summary>
		/// Attribute to decide if services with this status will be 
		/// visible in business / support catalog
		/// </summary>
		[Display(Name = "Catalog Visible")]
		[Required(ErrorMessage = "Catalog Visibility selection required")]
		public bool CatalogVisible { get; set; }
		#endregion

		#region Navigation Properties
		#endregion

	}
}
