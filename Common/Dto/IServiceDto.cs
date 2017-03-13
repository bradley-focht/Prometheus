using System.Collections.Generic;
using Common.Enums.Entities;

namespace Common.Dto
{
	public interface IServiceDto : ICatalogPublishable, IUserCreatedEntityDto
	{
		string BusinessOwner { get; set; }
		string Description { get; set; }
		int LifecycleStatusId { get; set; }
		int? ServiceBundleId { get; set; }
		string ServiceOwner { get; set; }
		ServiceTypeProvision ServiceTypeProvision { get; set; }
		ServiceTypeRole ServiceTypeRole { get; set; }
		ILifecycleStatusDto LifecycleStatusDto { get; set; }
		ICollection<IServiceSwotDto> ServiceSwots { get; set; }
		ICollection<IServiceWorkUnitDto> ServiceWorkUnits { get; set; }
		ICollection<IServiceContractDto> ServiceContracts { get; set; }
		ICollection<IServiceGoalDto> ServiceGoals { get; set; }
		ICollection<IServiceMeasureDto> ServiceMeasures { get; set; }
		ICollection<IServiceDocumentDto> ServiceDocuments { get; set; }
		ICollection<IServiceProcessDto> ServiceProcesses { get; set; }
		ICollection<IServiceOptionDto> ServiceOptions { get; set; }
		ICollection<IServiceOptionCategoryDto> ServiceOptionCategories { get; set; }
		ICollection<IServiceDto> Dependencies { get; set; }
	}
}