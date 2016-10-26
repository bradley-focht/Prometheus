using System.Collections.Generic;

namespace DataService.Models
{
	public interface IService : IUserCreatedEntity
	{
		string BusinessOwner { get; set; }
		string Description { get; set; }
		int Id { get; set; }
		int LifecycleStatusId { get; set; }
		string Name { get; set; }
		int ServiceBundleId { get; set; }
		string ServiceOwner { get; set; }
		ICollection<ServiceRequestOption> ServiceRequestOptions { get; set; }
		ServiceBundle ServiceBundle { get; set; }
		//TODO: make enums Brad
		//Guid ServiceTypeProvision { get; set; }
		//Guid ServiceTypeRole { get; set; }
		ICollection<ServiceGoal> ServiceGoals { get; set; }
	}
}