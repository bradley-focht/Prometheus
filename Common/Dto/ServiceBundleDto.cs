using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServicePortfolio.Dto
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

		[Required(ErrorMessage = "Service Bundle: Name is required")]
		public string Name { get; set; }

		[DataType(DataType.MultilineText)]
		public string Description { get; set; }

		[DataType(DataType.MultilineText)]
		public string BusinessValue { get; set; }
		public string Measures { get; set; }

		//Navigation properties
		public virtual ICollection<IServiceDto> Services { get; set; }
	}
}
