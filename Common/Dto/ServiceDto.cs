using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Common.Enums;

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
		[Display(Name = "Lifecycle Status", Order=7)]
		public int LifecycleStatusId { get; set; }

		//Fields
		public DateTime? DateCreated { get; set; }

		public DateTime? DateUpdated { get; set; }

		public int CreatedByUserId { get; set; }

	    public ServiceTypeRole Role { get; set; }
	    public int UpdatedByUserId { get; set; }

        //unique name to identify each service
		[Required(ErrorMessage = "*required")]
        [Display(Order=1)]
		public string Name { get; set; }

        //lengthy text description
		[DataType(DataType.MultilineText)]
        [AllowHtml]
        [Display(Order = 2)]
        public string Description { get; set; }

        //personal responsible for the business aspects of the service
        // this may be changed to an ssid in the future
		[Display(Name = "Business Owner", Order = 3)]
        public string BusinessOwner { get; set; }

        //user who is responsibile for execution of the process
        // this may be an ssid in the future
		[Display(Name = "Service Owner", Order=4)]
		public string ServiceOwner { get; set; }

		//Determines which service catalog (service or supporting) the service belongs in
		[Display(Name = "Service Type Role", Order=5)]
		public ServiceTypeRole ServiceTypeRole { get; set; }
        //indicate if the service is internally provided or outsourced
		[Display(Name = "Service Type Provision", Order=6)]
		public ServiceTypeProvision ServiceTypeProvision { get; set; }

		//Navigation Properties
        //service bundle indicates to which service portfolio service (i.e. the term service bundle is used)
        // this is the Gartner Service Portfolio, which is described differently than the ITIL service portfolio
		public virtual IServiceBundleDto ServiceBundle { get; set; }
	    public ServiceTypeProvision Provision { get; set; }
	    //the entire lifecycle object to which the service belongs
		public virtual ILifecycleStatusDto LifecycleStatusDto { get; set; }
       
        //what you can get when you order this service
        public virtual ICollection<IServiceRequestOptionDto> ServiceRequestOptions { get; set; }

        //all collections below are part of the service package that goes with each service

        public virtual ICollection<IServiceGoalDto> ServiceGoals { get; set; }

        public virtual ICollection<IServiceSwotDto> ServiceSwot { get; set; }
	    public ICollection<IServiceContractDto> ServiceContracts { get; set; }
	    public ICollection<IServiceWorkUnitDto> ServiceWorkUnits { get; set; }
	    public ICollection<IServiceMeasureDto> ServiceMeasures { get; set; }
	}
}
