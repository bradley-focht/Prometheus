using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ServicePortfolio.Dto
{
	public class ServiceDto : IServiceDto
	{
		//PK
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		//FK
		public int ServiceBundleId { get; set; }

		[Display(Name = "Lifecycle Status")]
		public int LifecycleStatusId { get; set; }

		//Fields
		public DateTime? DateCreated { get; set; }

		public DateTime? DateUpdated { get; set; }

		public int CreatedByUserId { get; set; }

		public int UpdatedByUserId { get; set; }

		[Required(ErrorMessage = "Service: Name required")]
		public string Name { get; set; }

		[DataType(DataType.MultilineText)]
		public string Description { get; set; }

		[Display(Name = "Business Owner")]
		public string BusinessOwner { get; set; }

		[Display(Name = "Service Owner")]
		public string ServiceOwner { get; set; }


		//TODO: Brad fill Sean in on these
		//[Display(Name = "Service Type Role")]
		//public Guid ServiceTypeRole { get; set; }
		//[Display(Name = "Service Type Provision")]
		//public Guid ServiceTypeProvision { get; set; }

		//Navigation Properties
		public virtual IServiceBundleDto ServiceBundle { get; set; }
		public virtual ILifecycleStatusDto LifecycleStatusDto { get; set; }
		public virtual ICollection<IServiceRequestOptionDto> ServiceRequestOptions { get; set; }
	}
}
