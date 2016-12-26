using Common.Dto;
using Common.Enums;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface IServiceMeasureController : IUserController
	{
		/// <summary>
		/// Finds service Measure with identifier provided and returns its DTO
		/// </summary>
		/// <param name="serviceMeasureId"></param>
		/// <returns></returns>
		IServiceMeasureDto GetServiceMeasure(int serviceMeasureId);

		/// <summary>
		/// Modifies the service Measure in the database
		/// </summary>
		/// <param name="serviceMeasure"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Measure</returns>
		IServiceMeasureDto ModifyServiceMeasure(IServiceMeasureDto serviceMeasure, EntityModification modification);
	}
}