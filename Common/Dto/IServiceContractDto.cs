using System;

namespace Common.Dto
{
	public interface IServiceContractDto : IUserCreatedEntityDto
	{
		string ContractNumber { get; set; }

		/// <summary>
		/// Date the Contract Expires
		/// </summary>
		DateTime ExpiryDate { get; set; }
		int Id { get; set; }
		int ServiceId { get; set; }
		string ServiceProvider { get; set; }

		/// <summary>
		/// Date the Contract goes into effect
		/// </summary>
		DateTime StartDate { get; set; }

		//TODO: ask brooke
		string ContractType { get; set; }
		string Description { get; set; }
	}
}