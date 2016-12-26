using Common.Dto;
using Common.Enums;
using System;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface IServiceDocumentController : IUserController
	{
		/// <summary>
		/// Finds service document with identifier provided and returns its DTO
		/// </summary>
		/// <param name="serviceDocumentId"></param>
		/// <returns></returns>
		IServiceDocumentDto GetServiceDocument(Guid serviceDocumentId);

		/// <summary>
		/// Modifies the service document in the database
		/// </summary>
		/// <param name="serviceDocument"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Document</returns>
		IServiceDocumentDto ModifyServiceDocument(IServiceDocumentDto serviceDocument, EntityModification modification);
	}
}
