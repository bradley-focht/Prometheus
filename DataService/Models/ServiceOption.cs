using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	/// <summary>
	/// A requestable item that a Service provides
	/// </summary>
	public class ServiceOption : IServiceOption
	{
		//PK
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		//FK
		/// <summary>
		/// Service Request Category
		/// </summary>
		public int ServiceOptionCategoryId { get; set; }

		public int Popularity { get; set; }

		/// <summary>
		/// Unique name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Service Catalog display information
		/// </summary>
		public string BusinessValue { get; set; }

		/// <summary>
		/// Visible in Catalog
		/// </summary>
		public bool Published { get; set; }

		/// <summary>
		/// Uploaded picture, only one per option allowed
		/// </summary>
		public Guid? Picture { get; set; }

		/// <summary>
		/// Media type of the Picture
		/// </summary>
		public string PictureMimeType { get; set; }

		/// <summary>
		/// profit made
		/// </summary>
		public double PriceUpFront { get; set; }

		/// <summary>
		/// profit made
		/// </summary>
		public double PriceMonthly { get; set; }

		/// <summary>
		/// Cost paid by the company to provide the option
		/// </summary>
		public double Cost { get; set; }

		/// <summary>
		/// Utilization of the option by clients
		/// </summary>
		public string Utilization { get; set; }

		/// <summary>
		/// Included in the price
		/// </summary>
		public string Included { get; set; }

		/// <summary>
		/// method of procuring
		/// </summary>
		public string Procurement { get; set; }

		/// <summary>
		/// service design package description
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// technical details
		/// </summary>
		public string Details { get; set; }

		/// <summary>
		/// Option "risk" level. Basic Requests can be Approved by users with 
		/// the ApproveServiceRequest.ApproveBasicRequests permission
		/// </summary>
		public bool BasicRequest { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
		#endregion
		#region Navigation properties
		public virtual ServiceOptionCategory ServiceOptionCategory { get; set; }


		public virtual ICollection<TextInput> TextInputs { get; set; }
		public virtual ICollection<ScriptedSelectionInput> ScriptedSelectionInputs { get; set; }
		public virtual ICollection<SelectionInput> SelectionInputs { get; set; }
		#endregion
	}
}
