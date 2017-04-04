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

		/// <summary>
		/// Contract or Invoice number
		/// </summary>
		[Required(ErrorMessage = "Contract Number is Required")]
		[Display(Name = "Contract Number")]
		public string ContractNumber { get; set; }

		/// <summary>
		/// Service Vendor or provider
		/// </summary>
		[Required(ErrorMessage = "Service Provider is Required")]
		[Display(Name = "Service Provider")]
		public string ServiceProvider { get; set; }

		/// <summary>
		/// Contract type? unknown
		/// </summary>
		[Display(Name="Contract Type")]
		public string ContractType { get; set; }

		/// <summary>
		/// Other details of the contract
		/// </summary>
		[AllowHtml]
		public string Description { get; set; }

		/// <summary>
		/// Date the Contract goes into effect
		/// </summary>
		[Display(Name = "Start Date")]
		[Required(ErrorMessage = "Start Date is required")]
		public DateTime StartDate { get; set; }

		/// <summary>
		/// Date the Contract Expires
		/// </summary>
		[Display(Name = "Expiry Date")]
		[Required(ErrorMessage = "Expiry Date is required")]
		public DateTime ExpiryDate { get; set; }
		#endregion

	}
}
