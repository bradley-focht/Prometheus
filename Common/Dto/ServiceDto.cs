using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Common.Enums.Entities;

namespace Common.Dto
{
	public class ServiceDto : IServiceDto
	{
		//PK
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		//FK
		public int? ServiceBundleId { get; set; }

		public int LifecycleStatusId { get; set; }


		public DateTime? DateCreated { get; set; }

		public DateTime? DateUpdated { get; set; }

		public int CreatedByUserId { get; set; }

		public int UpdatedByUserId { get; set; }


		#region Fields

		/// <summary>
		/// Unique name to identify each service
		/// </summary>
		[Required(ErrorMessage = "Name is required")]
		[Display(Order = 1)]
		public string Name { get; set; }

		/// <summary>
		/// Value offered to Customers, inherited from ICatalogable
		/// </summary>
		[AllowHtml]
		[Display(Order = 2, Name = "Business Value")]
		public string BusinessValue { get; set; }

		/// <summary>
		/// Enabled in searches
		/// </summary>
		public bool Published { get; set; }

		/// <summary>
		/// Lengthy text description, internal and may be technical
		/// </summary>
		[AllowHtml]
		[Display(Order = 3)]
		public string Description { get; set; }


		/// <summary>
		/// User who is responsibile for execution of the process
		/// this may be an SSID in the future
		/// </summary>
		[Display(Name = "Service Owner", Order = 4)]
		public string ServiceOwner { get; set; }

		/// <summary>
		/// Not yet defined
		/// </summary>
		[Display(Name = "Business Owner", Order = 5)]
		public string BusinessOwner { get; set; }

		/// <summary>
		/// Determines which service catalog (service or supporting) the service belongs in
		/// </summary>
		[Display(Name = "Service Type Role", Order = 6)]
		public ServiceTypeRole ServiceTypeRole { get; set; }

		/// <summary>
		/// Indicate if the service is internally provided or outsourced
		/// </summary>
		[Display(Name = "Service Type Provision", Order = 7)]
		public ServiceTypeProvision ServiceTypeProvision { get; set; }

		/// <summary>
		/// Used for sorting in the Service Catalog, inherited from ICatalogable
		/// </summary>
		public int Popularity { get; set; }



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
		/// All collections below are part of the service package that goes with each service
		/// </summary>
		public virtual ICollection<IServiceGoalDto> ServiceGoals { get; set; }
		public virtual ICollection<IServiceSwotDto> ServiceSwots { get; set; }
		public virtual ICollection<IServiceContractDto> ServiceContracts { get; set; }
		public virtual ICollection<IServiceWorkUnitDto> ServiceWorkUnits { get; set; }
		public virtual ICollection<IServiceMeasureDto> ServiceMeasures { get; set; }
		/// <summary>
		/// Used for basic document management
		/// </summary>
		public ICollection<IServiceDocumentDto> ServiceDocuments { get; set; }
		/// <summary>
		/// Term is ambiguous, may be Gartner or ITIL by definition
		/// </summary>
		public ICollection<IServiceProcessDto> ServiceProcesses { get; set; }

		/// <summary>
		/// What you can get when you order this service
		/// </summary>
		public virtual ICollection<IServiceOptionDto> ServiceOptions
		{
			get
			{
				ICollection<IServiceOptionDto> options = new List<IServiceOptionDto>();
				foreach (var cat in ServiceOptionCategories)
				{
					foreach (var opt in cat.ServiceOptions)
					{
						options.Add(opt);
					}
				}
				return options;
			}

			set { }
		}

		/// <summary>
		/// Other services that this service depends on
		/// </summary>
		public virtual ICollection<IServiceDto> Dependencies { get; set; }

		/// <summary>
		/// Option categories
		/// </summary>
		public virtual ICollection<IServiceOptionCategoryDto> ServiceOptionCategories { get; set; }
	}

	#endregion
}
