using System;

namespace DataService.Models
{
	public interface ISwotActivity : IUserCreatedEntity
	{
		int Id { get; set; }
		int ServiceId { get; set; }
		int ServiceSwotId { get; set; }

		DateTime Date { get; set; }
		string Description { get; set; }
		string Name { get; set; }

		IService Service { get; set; }
		IServiceSwot ServiceSwot { get; set; }
	}
}