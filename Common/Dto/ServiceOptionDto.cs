using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Common.Enums;
using Common.Enums.Entities;

namespace Common.Dto
{
	public class ServiceOptionDto : IServiceOptionDto, ICatalogable, IRequestable
	{
		//PK
		public int Id { get; set; }

		//FK
		public int ServiceId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [AllowHtml]
	    public string Description { get; set; }
        [Display(Name = "Business Value")]
        [AllowHtml]
	    public string BusinessValue { get; set; }
	    public string Picture { get; set; }
	    public ICollection<Tuple<PriceType, double>> Prices { get; set; }
	    public double Cost { get; set; }

	    #region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		#endregion
		#region Navigation properties
		#endregion
	}
}
