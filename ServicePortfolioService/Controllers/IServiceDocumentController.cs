using Common.Dto;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface IServiceDocumentController
	{
		/// <summary>
		/// Finds service document with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceDocumentId"></param>
		/// <returns></returns>
		IServiceDocumentDto GetServiceDocument(int performingUserId, int serviceDocumentId);

		/// <summary>
		/// Modifies the service document in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceDocument"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Document</returns>
		IServiceDocumentDto ModifyServiceDocument(int performingUserId, IServiceDocumentDto serviceDocument, EntityModification modification);
	}
}
