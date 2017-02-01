using Common.Dto;
using System.Collections.Generic;

namespace DataService.Models
{
	public interface IServiceOptionCategory : ICatalogPublishable, IUserCreatedEntity
	{
		Service Service { get; set; }
		ICollection<ServiceOption> ServiceOptions { get; set; }
        int ServiceId { get; set; }
        string Features { get; set; }
        string Benefits { get; set; }
        string Support { get; set; }
        string Description { get; set; }
    }
}