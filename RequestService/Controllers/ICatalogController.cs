using Common.Dto;
using System.Collections.Generic;

namespace RequestService
{
	public interface ICatalogController
	{
		/// <summary>
		/// List of all publicly viewable services
		/// </summary>
		IEnumerable<IServiceDto> BusinessCatalog { get; }

		/// <summary>
		/// List of all viewable Supporting services
		/// </summary>
		IEnumerable<IServiceDto> SupportCatalog { get; }
	}
}