using System;
using System.Collections.Generic;

namespace Common.Dto
{
	public interface IServiceDto
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
		ICollection<IServiceRequestOptionDto> ServiceRequestOptions { get; set; }
		IServiceBundleDto ServiceBundle { get; set; }
		//Guid ServiceTypeProvision { get; set; }
		//Guid ServiceTypeRole { get; set; }
		int UpdatedByUserId { get; set; }

		ICollection<IServiceGoalDto> ServiceGoals { get; set; }
        ICollection<IServiceSwotDto> ServiceSwot { get; set; }
	}
}