
using System.Collections.Generic;

namespace Common.Dto
{
	public interface IServiceOptionCategoryDto : ICatalogPublishable
	{
		IServiceDto Service { get; set; }
		ICollection<IServiceOptionDto> ServiceOptions { get; set; }
        int ServiceId { get; set; }
        string Features { get; set; }
        string Benefits { get; set; }
        string Support { get; set; }
        string Description { get; set; }
    }
}