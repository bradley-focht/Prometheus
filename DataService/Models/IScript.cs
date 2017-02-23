using System.IO;

namespace DataService.Models
{
	public interface IScript : IUserCreatedEntity
	{
		int Id { get; set; }
	    string Name { get; set; }
	    string Description { get; set; }
	    string Version { get; set; }
	}
}