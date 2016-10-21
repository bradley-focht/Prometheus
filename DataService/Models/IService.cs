using System;
using System.Collections.Generic;

namespace DataService.Models
{
	public interface IService
	{
		string BusinessOwner { get; set; }
		int CreatedByUserId { get; set; }
		DateTime? DateCreated { get; set; }
		DateTime? DateUpdated { get; set; }
		string Description { get; set; }
		int Id { get; set; }
		int LifecycleStatusId { get; set; }
		string Name { get; set; }
		int ServiceBundleId { get; set; }
		string ServiceOwner { get; set; }
		ICollection<ServiceRequestOption> ServiceRequestOptions { get; set; }
		ServiceBundle ServiceBundle { get; set; }
		//Guid ServiceTypeProvision { get; set; }
		//Guid ServiceTypeRole { get; set; }
		int UpdatedByUserId { get; set; }
	}
}