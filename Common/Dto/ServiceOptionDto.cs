using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
	/// <summary>
	/// A requestable item that a Service provides
	/// </summary>
	public class ServiceOptionDto : IServiceOptionDto, ICatalogPublishable
	{
		[HiddenInput]
		public int Id { get; set; }

		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		#region Fields

		/// <summary>
		/// Unique name
		/// </summary>
		[Required(ErrorMessage = "Name is required")]
		[Display(Order = 1)]
		public string Name { get; set; }

		/// <summary>
		/// service design package description
		/// </summary>
		[Display(Order = 2)]
		[AllowHtml]
		public string Description { get; set; }

		/// <summary>
		/// technical details
		/// </summary>
		[Display(Order = 3)]
		[AllowHtml]
		public string Details { get; set; }

		/// <summary>
		/// Service Catalog display information
		/// </summary>
		[AllowHtml]
		[Display(Order = 2, Name = "Business Value")]
		public string BusinessValue { get; set; }

		/// <summary>
		/// Visible in Catalog
		/// </summary>
		public bool Published { get; set; }

		/// <summary>
		/// Service Request Category
		/// </summary>
		[Required(ErrorMessage = "Category is required")]
		[Display(Order = 8, Name = "Category")]
		public int ServiceOptionCategoryId { get; set; }

		/// <summary>
		/// Utilization of the option by clients
		/// </summary>
		[Display(Order = 8)]
		public string Utilization { get; set; }

		/// <summary>
		/// Included in the price
		/// </summary>
		[AllowHtml]
		[Display(Order = 3)]
		public string Included { get; set; }

		/// <summary>
		/// method of procuring
		/// </summary>
		[AllowHtml]
		[Display(Order = 4)]
		public string Procurement { get; set; }

		/// <summary>
		/// Cost paid by the company to provide the option
		/// </summary>
		[Display(Order = 5)]
		public double Cost { get; set; }

		/// <summary>
		/// profit made
		/// </summary>
		[Display(Name = "Up Front Price", Order = 6)]
		public double PriceUpFront { get; set; }

		/// <summary>
		/// profit made
		/// </summary>
		[Display(Name = "Monthly Price", Order = 7)]
		public double PriceMonthly { get; set; }

		/// <summary>
		/// Option "risk" level. Basic Requests can be Approved by users with 
		/// the ApproveServiceRequest.ApproveBasicRequests permission
		/// </summary>
		[Display(Name = "Approval Level")]
		public bool BasicRequest { get; set; }

		/// <summary>
		/// Uploaded picture, only one per option allowed
		/// </summary>
		public Guid? Picture { get; set; }

		/// <summary>
		/// ordering for catalog publishing, not part of service design
		/// </summary>
		public int Popularity { get; set; }

		/// <summary>
		/// Media type of the Picture
		/// </summary>
		public string PictureMimeType { get; set; }

		#endregion
		#region Navigation properties
		#endregion

		public virtual ICollection<ITextInputDto> TextInputs { get; set; }
		public virtual ICollection<IScriptedSelectionInputDto> ScriptedSelectionInputs { get; set; }
		public virtual ICollection<ISelectionInputDto> SelectionInputs { get; set; }
	}
}
