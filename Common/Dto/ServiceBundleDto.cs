using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
	public class ServiceBundleDto : IServiceBundleDto
	{
		//PK
		public int Id { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

		/// <summary>
		/// Unique name must be provided
		/// </summary>
		[Required(ErrorMessage = "name is required")]
		public string Name { get; set; }

		/// <summary>
		/// Free text field
		/// </summary>
		[AllowHtml]
		[DataType(DataType.MultilineText)]
		public string Description { get; set; }

		/// <summary>
		/// Extra text for those who just enjoy reading that much
		/// </summary>
		[AllowHtml]
		[DataType(DataType.MultilineText)]
		[Display(Name = "Business Value")]
		public string BusinessValue { get; set; }

		/// <summary>
		/// List of measures, should be comma separated, but won't be enforced
		/// </summary>
		public string Measures { get; set; }
		#endregion

		#region Navigation properties
		#endregion
	}
}
