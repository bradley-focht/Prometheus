using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DataService.Models
{
	public class Service : IService
	{
		//PK
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
        [HiddenInput(DisplayValue = false)]
		public int Id { get; set; }
		//FK
		public Guid ServiceBundleId { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public Guid CreatedByUserId { get; set; }
		public Guid UpdatedByUserId { get; set; }

        [Required(ErrorMessage="name required")]
		public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        
        public string Description { get; set; }

        [Display(Name="Business Owner")]
        public string BusinessOwner { get; set; }
        [Display(Name="Service Owner")]
		public string ServiceOwner { get; set; }
        [Display(Name="Lifecycle Status")]
		public Guid LifecycleStatus { get; set; }

	    [Display(Name = "Service Type Role")]
		public Guid ServiceTypeRole { get; set; }
        [Display(Name = "Service Type Provision")]
        public Guid ServiceTypeProvision { get; set; }
        public IEnumerable<IServiceGoal> ServiceGoals { get; set; }
	    public IEnumerable<IServiceWorkUnit> ServiceWorkUnits { get; set; }
        public IEnumerable<IServiceContract> ServiceContracts { get; set; }
	    public IEnumerable<IServiceMeasure> ServiceMeasures { get; set; }


        //Navigation Properties
        public virtual ServiceBundle ServiceBundle { get; set; }
		public virtual ICollection<ServiceRequestOption> ServiceRequestOptions { get; set; }

	    

        int IService.ServiceTypeProvision
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        int IService.ServiceTypeRole
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
