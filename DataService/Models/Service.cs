using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Enums.Entities;

namespace DataService.Models
{
	public class Service : IService
	{
		//PK
		//[Key, ForeignKey("LifecycleStatus")]
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		//FK
		public int? ServiceBundleId { get; set; }

		public int LifecycleStatusId { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }

		public DateTime? DateUpdated { get; set; }

		public int CreatedByUserId { get; set; }

		public int UpdatedByUserId { get; set; }

		public string Name { get; set; }
		public string BusinessValue { get; set; }
		public string Description { get; set; }
		public string BusinessOwner { get; set; }

		public int Popularity { get; set; }

		public string ServiceOwner { get; set; }
		public ServiceTypeRole ServiceTypeRole { get; set; }
		public ServiceTypeProvision ServiceTypeProvision { get; set; }
		#endregion
		#region Navigation Properties
		public virtual LifecycleStatus LifecycleStatus { get; set; }
		public virtual ICollection<ServiceContract> ServiceContracts { get; set; }
		public virtual ICollection<ServiceMeasure> ServiceMeasures { get; set; }
		public virtual ICollection<ServiceGoal> ServiceGoals { get; set; }
		public virtual ICollection<ServiceSwot> ServiceSwots { get; set; }
		public virtual ICollection<ServiceWorkUnit> ServiceWorkUnits { get; set; }
		public virtual ICollection<ServiceDocument> ServiceDocuments { get; set; }
		public virtual ICollection<ServiceProcess> ServiceProcesses { get; set; }
		public virtual ICollection<ServiceOptionCategory> ServiceOptionCategories { get; set; }
		public virtual ICollection<Service> DependentServices { get; set; }
		#endregion
	}
}
