using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
	public class ServiceOptionDto : IServiceOptionDto, ICatalogable, IRequestable
	{
		[HiddenInput]
		public int Id { get; set; }

		public int? CategoryId { get; set; }
		[Display(Order = 2)]
		public int Popularity { get; set; }
		//FK
		[HiddenInput]
		public int ServiceId { get; set; }
		[Required(ErrorMessage = "Name is required")]
		[Display(Order = 1)]
		public string Name { get; set; }
		[AllowHtml]
		[Display(Order = 3)]
		public string Description { get; set; }
		[Display(Name = "Business Value", Order = 4)]
		[AllowHtml]
		public string BusinessValue { get; set; }
		[Display(Order = 5)]
		public string Picture { get; set; }
		[Display(Order = 6)]
		public double Cost { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		#endregion
		#region Navigation properties
		[Display(Order = 7)]
		public ICollection<IPriceDto> Prices { get; set; }
		#endregion
	}
}
