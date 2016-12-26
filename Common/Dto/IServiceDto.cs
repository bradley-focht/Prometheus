using Common.Enums;
using System.Collections.Generic;

namespace Common.Dto
{
	public interface IServiceDto : IUserCreatedEntityDto
	{
		string BusinessOwner { get; set; }
		string Description { get; set; }
		int Id { get; set; }
		int LifecycleStatusId { get; set; }
		string Name { get; set; }
		int ServiceBundleId { get; set; }
		string ServiceOwner { get; set; }
		ServiceTypeProvision ServiceTypeProvision { get; set; }
		ServiceTypeRole ServiceTypeRole { get; set; }
		IServiceBundleDto ServiceBundle { get; set; }
		ILifecycleStatusDto LifecycleStatusDto { get; set; }
		ICollection<IServiceOptionDto> ServiceOptions { get; set; }
		ICollection<IServiceSwotDto> ServiceSwots { get; set; }
		ICollection<IServiceWorkUnitDto> ServiceWorkUnits { get; set; }
		ICollection<IServiceContractDto> ServiceContracts { get; set; }
		ICollection<IServiceGoalDto> ServiceGoals { get; set; }
		ICollection<IServiceMeasureDto> ServiceMeasures { get; set; }
		ICollection<IServiceDocumentDto> ServiceDocuments { get; set; }
		ICollection<IServiceProcessDto> ServiceProcesses { get; set; }
        ICollection<IServiceDto> Dpendents { get; set;  }
	}
}