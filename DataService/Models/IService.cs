using System;
using System.Collections.Generic;

namespace DataService.Models
{
	public interface IService
	{
		string BusinessOwner { get; set; }
		Guid CreatedByUserId { get; set; }
		DateTime? DateCreated { get; set; }
		DateTime? DateUpdated { get; set; }
		string Description { get; set; }
		int Id { get; set; }
		Guid LifecycleStatus { get; set; }
		string Name { get; set; }
		ServiceBundle ServiceBundle { get; set; }
		Guid ServiceBundleId { get; set; }
		string ServiceOwner { get; set; }
		ICollection<ServiceRequestOption> ServiceRequestOptions { get; set; }
        IEnumerable<IServiceGoal> ServiceGoals { get; set; }
        int ServiceTypeProvision { get; set; }
        int ServiceTypeRole { get; set; }
		Guid UpdatedByUserId { get; set; }
	    IEnumerable<IServiceWorkUnit> ServiceWorkUnits { get; set; }
        IEnumerable<IServiceContract> ServiceContracts { get; set; }
        IEnumerable<IServiceMeasure> ServiceMeasures { get; set; }
	}
}