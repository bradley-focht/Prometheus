
using System.Collections.Generic;

namespace Common.Dto
{
	public interface IServiceOptionCategoryDto : IOffering
	{
		IServiceDto Service { get; set; }
		ICollection<IServiceOptionDto> ServiceOptions { get; set; }
	}
}