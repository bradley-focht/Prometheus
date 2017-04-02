using System.Collections.Generic;
using Common.Enums.Entities;

namespace DataService.Models
{
	/// <summary>
	/// ITIL Service offered by the client
	/// </summary>
	public interface IService : IUserCreatedEntity
	{
		int Id { get; set; }

		/// <summary>
		/// Service Bundle in the Service Portfolio
		/// </summary>
		int? ServiceBundleId { get; set; }

		/// <summary>
		/// Not yet defined
		/// </summary>
		string BusinessOwner { get; set; }

		/// <summary>
		/// Lengthy text description, internal and may be technical
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// Value offered to Customers, inherited from ICatalogable
		/// </summary>
		string BusinessValue { get; set; }

		/// <summary>
		/// Lifecycle Status that Service is in
		/// </summary>
		int LifecycleStatusId { get; set; }

		/// <summary>
		/// Unique name to identify each service
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// User who is responsibile for execution of the process
		/// this may be an SSID in the future
		/// </summary>
		string ServiceOwner { get; set; }

		/// <summary>
		/// Used for sorting in the Service Catalog. Larger is more popular
		/// </summary>
		int Popularity { get; set; }

		/// <summary>
		/// Determines which service catalog (service or supporting) the service belongs in
		/// </summary>
		ServiceTypeRole ServiceTypeRole { get; set; }

		/// <summary>
		/// Indicate if the service is internally provided or outsourced
		/// </summary>
		ServiceTypeProvision ServiceTypeProvision { get; set; }

		LifecycleStatus LifecycleStatus { get; set; }
		ICollection<ServiceGoal> ServiceGoals { get; set; }
		ICollection<ServiceContract> ServiceContracts { get; set; }
		ICollection<ServiceMeasure> ServiceMeasures { get; set; }
		ICollection<ServiceSwot> ServiceSwots { get; set; }
		ICollection<ServiceWorkUnit> ServiceWorkUnits { get; set; }
		ICollection<ServiceDocument> ServiceDocuments { get; set; }
		ICollection<ServiceProcess> ServiceProcesses { get; set; }
		ICollection<ServiceOptionCategory> ServiceOptionCategories { get; set; }
		ICollection<Service> DependentServices { get; set; }
	}
}