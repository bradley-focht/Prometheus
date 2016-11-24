using System;

namespace Common.Dto
{
	public interface IServiceContractDto : IUserCreatedEntityDto
	{
		string ContractNumber { get; set; }
		DateTime ExpiryDate { get; set; }
		int Id { get; set; }
		int ServiceId { get; set; }
		string ServiceProvider { get; set; }
		DateTime StartDate { get; set; }

		//TODO: ask brooke
		string ContractType { get; set; }
		string Description { get; set; }
	}
}