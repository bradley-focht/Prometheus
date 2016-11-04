using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Models
{
	public class Service : IService
	{
		//PK
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		//FK
		public int ServiceBundleId { get; set; }

		[Display(Name = "Lifecycle Status")]
		public int LifecycleStatusId { get; set; }

		#region Fields
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
		public ServiceTypeRole ServiceTypeRole { get; set; }
		public ServiceTypeProvision ServiceTypeProvision { get; set; }
		#endregion
		#region Navigation Properties
		public virtual IServiceBundle ServiceBundle { get; set; }
		public virtual ILifecycleStatus LifecycleStatus { get; set; }

		public virtual ICollection<IServiceRequestOption> ServiceRequestOptions { get; set; }
		public virtual ICollection<IServiceContract> ServiceContracts { get; set; }
		public virtual ICollection<IServiceMeasure> ServiceMeasures { get; set; }
		public virtual ICollection<IServiceGoal> ServiceGoals { get; set; }
		public virtual ICollection<IServiceSwot> ServiceSwots { get; set; }
		public virtual ICollection<IServiceWorkUnit> ServiceWorkUnits { get; set; }
        public virtual ICollection<IServiceDocument> ServiceDocuments { get; set; }
		#endregion
	}
}
