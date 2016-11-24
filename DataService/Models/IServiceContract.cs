using System;

namespace DataService.Models
{
	public interface IServiceContract : IUserCreatedEntity
	{
		string ContractNumber { get; set; }
		DateTime ExpiryDate { get; set; }
		int Id { get; set; }
		int ServiceId { get; set; }
		string ServiceProvider { get; set; }
		DateTime StartDate { get; set; }

		//TODO: ask brooke :: I forgot to ask 
		string ContractType { get; set; }
		string Description { get; set; }

		Service Service { get; set; }
	}
}