using System;

namespace DataService.Models
{
	public interface ISwotActivity : IUserCreatedEntity
	{
		int Id { get; set; }
		int ServiceSwotId { get; set; }

		DateTime? Date { get; set; }
		string Description { get; set; }
		string Name { get; set; }

		ServiceSwot ServiceSwot { get; set; }
	}
}