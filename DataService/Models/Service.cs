using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DataService.Models
{
	public class Service : IService
	{
		//PK
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

		public ICollection<ServiceGoal> ServiceGoals { get; set; }

		//TODO: Brad fill Sean in on these
		//[Display(Name = "Service Type Role")]
		//public Guid ServiceTypeRole { get; set; }
		//[Display(Name = "Service Type Provision")]
		//public Guid ServiceTypeProvision { get; set; }

		//Navigation Properties
		public virtual ServiceBundle ServiceBundle { get; set; }
		public virtual LifecycleStatus LifecycleStatus { get; set; }
		public virtual ICollection<ServiceRequestOption> ServiceRequestOptions { get; set; }
	}
}
