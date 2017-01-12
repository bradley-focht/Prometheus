using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
	public class ServiceOptionDto : IServiceOptionDto
	{
		[HiddenInput]
		public int Id { get; set; }

		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

	    //FK
		[HiddenInput]
		public int ServiceId { get; set; }

		#region Fields

		/// <summary>
		/// Unique name
		/// </summary>
		[Required(ErrorMessage = "Name is required")]
		[Display(Order = 1)]
		public string Name { get; set; }

		[Display(Order = 2, Name = "Category")]
		public int? CategoryId { get; set; }
		/// <summary>
		/// These are part of the service design package
		/// </summary>
		[AllowHtml]
		[Display(Order = 3)]
		public string Features { get; set; }
		[AllowHtml]
		[Display(Order = 4)]
		public string Benefits { get; set; }
		[AllowHtml]
		[Display(Order = 5)]
		public string Support { get; set; }
		[Display(Order = 6)]
		public string Usage { get; set; }

		/// <summary>
		/// Cost paid by the company to provide the option
		/// </summary>
		[Display(Order = 7)]
		public double Cost { get; set; }
		
		/// <summary>
		/// profit made
		/// </summary>
		[Display(Name = "Up Front Price", Order = 8)]
		public double PriceUpFront { get; set; }
		[Display(Name = "Monthly Price", Order = 9)]
		public double PriceMonthly { get; set; }


		/// <summary>
		/// Technical or detailed description
		/// </summary>
		[AllowHtml]
		[Display(Order = 10)]
		public string Description { get; set; }
		/// <summary>
		/// higher level, how this offering meets your business needs
		/// </summary>
		[Display(Name = "Business Value", Order = 11)]
		[AllowHtml]
		public string BusinessValue { get; set; }

		/// <summary>
		/// Uploaded picture, only one per option allowed
		/// </summary>
		[Display(Order=12)]
		public Guid? Picture { get; set; }

		/// <summary>
		/// ordering for catalog publishing, not part of service design
		/// </summary>
		public int Popularity { get; set; }
		

		public string PictureMimeType { get; set; }

		#endregion
		#region Navigation properties
		#endregion

		public virtual ICollection<ITextInputDto> TextInputs { get; set; }
		public virtual ICollection<IScriptedSelectionInputDto> ScriptedSelecentionInputs { get; set; }
		public virtual ICollection<ISelectionInputDto> SelectionInputs { get; set; }
	}
}
