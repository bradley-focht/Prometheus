using System.Collections.Generic;
using Common.Dto;

namespace RequestService.Controllers
{
	public interface ICatalogController
	{
		/// <summary>
		/// Retrieves the list of Services that are valid for viewing in the Business Catalog
		/// provided the user has permission to view the catalog.
		/// 
		/// Throws permission exception in event that user does not have access to this catalog
		/// </summary>
		/// <param name="requestingUserId">User requesting the Catalog</param>
		/// <returns></returns>
		IEnumerable<IServiceDto> RequestBusinessCatalog(int requestingUserId);

		/// <summary>
		/// Retrieves the list of Services that are valid for viewing in the Support Catalog
		/// provided the user has permission to view the catalog.
		/// 
		/// Throws permission exception in event that user does not have access to this catalog
		/// </summary>
		/// <param name="requestingUserId">User requesting the Catalog</param>
		/// <returns></returns>
		IEnumerable<IServiceDto> RequestSupportCatalog(int requestingUserId);
	}
}