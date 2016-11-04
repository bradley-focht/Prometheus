using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
	public class ServiceDto : IServiceDto
	{
		//PK
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		//FK
		public int ServiceBundleId { get; set; }

		//Id to service status to reduce calls to db
		[Display(Name = "Lifecycle Status", Order = 7)]
		public int LifecycleStatusId { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }

		public DateTime? DateUpdated { get; set; }

		public int CreatedByUserId { get; set; }

		public int UpdatedByUserId { get; set; }

		/// <summary>
		/// Unique name to identify each service
		/// </summary>
		[Required(ErrorMessage = "*required")]
		[Display(Order = 1)]
		public string Name { get; set; }

		/// <summary>
		/// Lengthy text description
		/// </summary>
		[DataType(DataType.MultilineText)]
		[AllowHtml]
		[Display(Order = 2)]
		public string Description { get; set; }

		/// <summary>
		/// Personel responsible for the business aspects of the service
		/// this may be changed to an SSID in the future
		/// </summary>
		[Display(Name = "Business Owner", Order = 3)]
		public string BusinessOwner { get; set; }

		/// <summary>
		/// User who is responsibile for execution of the process
		/// this may be an SSID in the future
		/// </summary>
		[Display(Name = "Service Owner", Order = 4)]
		public string ServiceOwner { get; set; }

		/// <summary>
		/// Determines which service catalog (service or supporting) the service belongs in
		/// </summary>
		[Display(Name = "Service Type Role", Order = 5)]
		public ServiceTypeRole ServiceTypeRole { get; set; }

		/// <summary>
		/// Indicate if the service is internally provided or outsourced
		/// </summary>
		[Display(Name = "Service Type Provision", Order = 6)]
		public ServiceTypeProvision ServiceTypeProvision { get; set; }
		#endregion

		#region Navigation Properties
		/// <summary>
		/// Service bundle indicates to which service portfolio service (i.e. the term service bundle is used)
		/// this is the Gartner Service Portfolio, which is described differently than the ITIL service portfolio
		/// </summary>
		public virtual IServiceBundleDto ServiceBundle { get; set; }

		/// <summary>
		/// The entire lifecycle object to which the service belongs
		/// </summary>
		public virtual ILifecycleStatusDto LifecycleStatusDto { get; set; }

		/// <summary>
		/// What you can get when you order this service
		/// </summary>
		public virtual ICollection<IServiceRequestOptionDto> ServiceRequestOptions { get; set; }

		/// <summary>
		/// All collections below are part of the service package that goes with each service
		/// </summary>
		public virtual ICollection<IServiceGoalDto> ServiceGoals { get; set; }
		public virtual ICollection<IServiceSwotDto> ServiceSwots { get; set; }
		public virtual ICollection<IServiceContractDto> ServiceContracts { get; set; }
		public virtual ICollection<IServiceWorkUnitDto> ServiceWorkUnits { get; set; }
		public virtual ICollection<IServiceMeasureDto> ServiceMeasures { get; set; }
	    public ICollection<IServiceDocumentDto> ServiceDocuments { get; set; }
	    #endregion
	}
}
