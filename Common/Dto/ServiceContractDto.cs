using System;

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
		public string ContractNumber { get; set; }
		public string ServiceProvider { get; set; }
		//TODO: enum?
		public string Type { get; set; }
		public string Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime ExpiryDate { get; set; }
		#endregion

	}
}
