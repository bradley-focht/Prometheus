using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Enums.Entities;

namespace DataService.Models
{
	/// <summary>
	/// ITIL Service offered by the client
	/// </summary>
	public class Service : IService
	{
		//PK
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		//FK
		/// <summary>
		/// Lifecycle Status that Service is in
		/// </summary>
		public int LifecycleStatusId { get; set; }


		/// <summary>
		/// Service Bundle in the Service Portfolio
		/// </summary>
		public int? ServiceBundleId { get; set; }
		#region Fields
		public DateTime? DateCreated { get; set; }

		public DateTime? DateUpdated { get; set; }

		public int CreatedByUserId { get; set; }

		public int UpdatedByUserId { get; set; }

		/// <summary>
		/// Unique name to identify each service
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Value offered to Customers, inherited from ICatalogable
		/// </summary>
		public string BusinessValue { get; set; }

		/// <summary>
		/// Lengthy text description, internal and may be technical
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Not yet defined
		/// </summary>
		public string BusinessOwner { get; set; }

		/// <summary>
		/// Used for sorting in the Service Catalog. Larger is more popular
		/// </summary>
		public int Popularity { get; set; }

		/// <summary>
		/// User who is responsibile for execution of the process
		/// this may be an SSID in the future
		/// </summary>
		public string ServiceOwner { get; set; }

		/// <summary>
		/// Determines which service catalog (service or supporting) the service belongs in
		/// </summary>
		public ServiceTypeRole ServiceTypeRole { get; set; }

		/// <summary>
		/// Indicate if the service is internally provided or outsourced
		/// </summary>
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
