using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Common.Dto
{
	public class ServiceContractDto : IServiceContractDto
	{
		
		public int Id { get; set; }
		public int ServiceId { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
        [Required(ErrorMessage = "Contract Number is Required")]
        [Display(Name="Contract Number")]
		public string ContractNumber { get; set; }
        [Required(ErrorMessage = "Service Provider is Required")]
        [Display(Name="Service Provider")]
		public string ServiceProvider { get; set; }
		
        //I don't really know what this is supposed to be....
		public string ContractType { get; set; }
        [AllowHtml]
		public string Description { get; set; }
        [Display(Name="Start Date")]
        [Required(ErrorMessage = "Start Date is required")]
		public DateTime StartDate { get; set; }
        [Display(Name="Expiry Date")]
        [Required(ErrorMessage = "Expiry Date is required")]
		public DateTime ExpiryDate { get; set; }
		#endregion

	}
}
