using Common.Dto;
using System.Collections.Generic;

namespace DataService.Models
{
	public interface IServiceOptionCategory : ICatalogPublishable, IUserCreatedEntity
	{
		Service Service { get; set; }
		int ServiceId { get; set; }
		string Features { get; set; }
		string Code { get; set; }
		string Benefits { get; set; }
		string Support { get; set; }
		string Description { get; set; }
        bool Quantifiable { get; set; }
        ICollection<ServiceRequestPackage> ServiceRequestPackages { get; set; }
		ICollection<ServiceOption> ServiceOptions { get; set; }
	}
}