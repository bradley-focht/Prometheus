using System.Collections.Generic;
using Common.Enums.Entities;

namespace Common.Dto
{
	/// <summary>
	/// ITIL Service offered by the client
	/// </summary>
	public interface IServiceDto : ICatalogPublishable, IUserCreatedEntityDto
	{
		/// <summary>
		/// Not yet defined
		/// </summary>
		string BusinessOwner { get; set; }

		/// <summary>
		/// Lengthy text description, internal and may be technical
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// Lifecycle Status that Service is in
		/// </summary>
		int LifecycleStatusId { get; set; }

		/// <summary>
		/// Service Bundle in the Service Portfolio
		/// </summary>
		int? ServiceBundleId { get; set; }

		/// <summary>
		/// User who is responsibile for execution of the process
		/// this may be an SSID in the future
		/// </summary>
		string ServiceOwner { get; set; }

		/// <summary>
		/// Indicate if the service is internally provided or outsourced
		/// </summary>
		ServiceTypeProvision ServiceTypeProvision { get; set; }

		/// <summary>
		/// Determines which service catalog (service or supporting) the service belongs in
		/// </summary>
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