
using System.Collections.Generic;

namespace Common.Dto
{
	public interface IServiceOptionCategoryDto : ICatalogPublishable
	{
		IServiceDto Service { get; set; }
		int ServiceId { get; set; }
		string Features { get; set; }
		string Benefits { get; set; }
		string Support { get; set; }
		string Description { get; set; }
        bool Quantifiable { get; set; }
        ICollection<IServiceOptionDto> ServiceOptions { get; set; }
		ICollection<IServiceRequestPackageDto> ServiceRequestPackages { get; set; }
	}
}