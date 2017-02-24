using Common.Dto;
using Common.Enums.Entities;

namespace ServicePortfolioService.Controllers
{
	public interface IServiceMeasureController
	{
		/// <summary>
		/// Finds service Measure with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceMeasureId"></param>
		/// <returns></returns>
		IServiceMeasureDto GetServiceMeasure(int performingUserId, int serviceMeasureId);

		/// <summary>
		/// Modifies the service Measure in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceMeasure"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Measure</returns>
		IServiceMeasureDto ModifyServiceMeasure(int performingUserId, IServiceMeasureDto serviceMeasure, EntityModification modification);
	}
}