using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
	public class ServiceBundleDto : IServiceBundleDto
	{
		//PK
		public int Id { get; set; }

		//Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }

        //unique name must be provided
		[Required(ErrorMessage = "Service Bundle: Name is required")]
		public string Name { get; set; }

        //free text field
		[AllowHtml]
		[DataType(DataType.MultilineText)]
		public string Description { get; set; }

        //extra text for those who just enjoy reading that much
		[AllowHtml]
		[DataType(DataType.MultilineText)]
		[Display(Name = "Business Value")]
		public string BusinessValue { get; set; }

        //list of measures, should be comma separated, but won't be enforced
		public string Measures { get; set; }

		//Navigation properties
		public virtual ICollection<IServiceDto> Services { get; set; }
	}
}
