using System;

namespace DataService.Models
{
	public interface IServiceContract
	{
		string ContractNumber { get; set; }
		DateTime ExpiryDate { get; set; }
		int Id { get; set; }
		int ServiceId { get; set; }
		string ServiceProvider { get; set; }
		DateTime StartDate { get; set; }

		//TODO: ask brooke
		string Type { get; set; }
		string Description { get; set; }
	}
}