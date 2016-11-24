using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Dto
{
	public class ServiceContractDto : IServiceContractDto
	{
		//TODO: Brad needs commenting
		public int Id { get; set; }
		public int ServiceId { get; set; }

		#region Fields
		public DateTime? DateCreated { get; set; }
		public DateTime? DateUpdated { get; set; }
		public int CreatedByUserId { get; set; }
		public int UpdatedByUserId { get; set; }
        [Display(Name="Contract Number")]
		public string ContractNumber { get; set; }
        [Display(Name="Service Provider")]
		public string ServiceProvider { get; set; }
		
        //I don't really know what this is supposed to be....
		public string ContractType { get; set; }
		public string Description { get; set; }
        [Display(Name="Start Date")]
		public DateTime StartDate { get; set; }
        [Display(Name="Expiry Date")]
		public DateTime ExpiryDate { get; set; }
		#endregion

	}
}
