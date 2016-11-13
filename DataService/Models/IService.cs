using Common.Enums;
using System.Collections.Generic;

namespace DataService.Models
{
	public interface IService : IUserCreatedEntity
	{
		int Id { get; set; }
		int ServiceBundleId { get; set; }

		string BusinessOwner { get; set; }
		string Description { get; set; }
		int LifecycleStatusId { get; set; }
		string Name { get; set; }
		string ServiceOwner { get; set; }
		ServiceTypeRole ServiceTypeRole { get; set; }
		ServiceTypeProvision ServiceTypeProvision { get; set; }

		ServiceBundle ServiceBundle { get; set; }
		LifecycleStatus LifecycleStatus { get; set; }
		ICollection<ServiceGoal> ServiceGoals { get; set; }
		ICollection<ServiceRequestOption> ServiceRequestOptions { get; set; }
		ICollection<ServiceContract> ServiceContracts { get; set; }
		ICollection<ServiceMeasure> ServiceMeasures { get; set; }
		ICollection<ServiceSwot> ServiceSwots { get; set; }
		ICollection<ServiceWorkUnit> ServiceWorkUnits { get; set; }
	}
}