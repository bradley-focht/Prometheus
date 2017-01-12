using System.Collections.Generic;
using Common.Dto;

namespace RequestService.Controllers
{
	public interface ICatalogController
	{
		IEnumerable<IServiceDto> RequestBusinessCatalog(int requestingUserId);
		IEnumerable<IServiceDto> RequestSupportCatalog(int requestingUserId);
	}
}