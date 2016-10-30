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

		IServiceBundle ServiceBundle { get; set; }
		ILifecycleStatus LifecycleStatus { get; set; }
		ICollection<IServiceGoal> ServiceGoals { get; set; }
		ICollection<IServiceRequestOption> ServiceRequestOptions { get; set; }
		ICollection<IServiceContract> ServiceContracts { get; set; }
		ICollection<IServiceMeasure> ServiceMeasures { get; set; }
		ICollection<IServiceSwot> ServiceSwots { get; set; }
		ICollection<IServiceWorkUnit> ServiceWorkUnits { get; set; }
	}
}