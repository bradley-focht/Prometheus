using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace Common.Dto
{
	public class ServiceOptionCategoryDto : IServiceOptionCategoryDto
	{
        /// <summary>
        /// PK
        /// </summary>
		[HiddenInput]
		public int Id { get; set; }

        /// <summary>
        /// FK
        /// </summary>
        [HiddenInput]
		public int ServiceId { get; set; }

		/// <summary>
		/// Used for sorting in service catalog
		/// Inherited from ICatalogable
		/// </summary>
		public int Popularity { get; set; }
		/// <summary>
		/// These attributes come from the Service Design Package
		/// </summary>
		[Display(Order = 3)]
		[AllowHtml]
		public string Features { get; set; }

        /// <summary>
        /// product or service benefits to the customer
        /// </summary>
		[Display(Order = 4)]
		[AllowHtml]
		public string Benefits { get; set; }

        /// <summary>
        /// how is this product or service supported
        /// </summary>
		[Display(Order = 5)]
		[AllowHtml]
		public string Support { get; set; }

        /// <summary>
        /// product or service over view, not serice catalog visible
        /// </summary>
		[Display(Order = 2)]
		[AllowHtml]
		public string Description { get; set; }

        /// <summary>
        /// Unique name
        /// </summary>
		[Required(ErrorMessage = "Name is required")]
		[Display(Order = 1)]
		public string Name { get; set; }

        /// <summary>
        /// Catalog visible overview of product or service
        /// </summary>
		[AllowHtml]
		[Display(Order = 6, Name = "Business Value")]
		public string BusinessValue { get; set; }

        #region Nagivation
        public virtual IServiceDto Service { get; set; }

        [Display(Order = 7, Name = "Service Options")]
        public virtual ICollection<IServiceOptionDto> ServiceOptions { get; set; }
        #endregion
    }
}
