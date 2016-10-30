using System.Collections.Generic;

namespace DataService.Models
{
	public interface IServiceBundle : IUserCreatedEntity
	{
		string BusinessValue { get; set; }
		string Description { get; set; }
		int Id { get; set; }
		string Measures { get; set; }
		string Name { get; set; }
		ICollection<IService> Services { get; set; }
	}
}