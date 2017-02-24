using Common.Dto;
using Prometheus.WebUI.Models.ServiceRequestMaintenance;

namespace Prometheus.WebUI.Helpers
{
	public class AbbreviatedEntityUpdate
	{
		/// <summary>
		/// preserve catalogable attributes
		/// </summary>
		/// <param name="src"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		public static IServiceDto UpdateService(ServiceAbbreviatedModel src, IServiceDto target)
		{
			target.BusinessValue = src.BusinessValue;
			target.Popularity = src.Popularity;

			return target;
		}

		/// <summary>
		/// preserve catalogable attributes
		/// </summary>
		/// <param name="src"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		public static IServiceDto UpdateService(IServiceDto src, IServiceDto target)
		{
			target.BusinessValue = src.BusinessValue;
			target.Popularity = src.Popularity;

			return target;
		}

		/// <summary>
		/// preserve catalogable attributes
		/// </summary>
		/// <param name="src"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		public static IServiceOptionCategoryDto UpdateServiceCategory(ServiceCategoryAbbreviatedModel src, IServiceOptionCategoryDto target)
		{
			target.BusinessValue = src.BusinessValue;
			target.Popularity = src.Popularity;
			target.Quantifiable = src.Quantifiable;

			return target;
		}

		/// <summary>
		/// preserve catalogable attributes
		/// </summary>
		/// <param name="src"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		public static IServiceOptionCategoryDto UpdateServiceCategory(IServiceOptionCategoryDto src, IServiceOptionCategoryDto target)
		{
			target.BusinessValue = src.BusinessValue;
			target.Popularity = src.Popularity;
			target.Quantifiable = src.Quantifiable;

			return target;
		}

		/// <summary>
		/// transfer new data onto an existing entity
		/// </summary>
		/// <param name="src"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		public static IServiceOptionDto UpdateServiceOption(ServiceOptionAbbreviatedModel src, IServiceOptionDto target)
		{
			target.BusinessValue = src.BusinessValue;
			target.Popularity = src.Popularity;
			target.PictureMimeType = src.PictureMimeType;
			target.Picture = src.Picture;
			target.Details = src.Details;

			return target;
		}

		/// <summary>
		/// transfer new data onto an existing entity
		/// </summary>
		/// <param name="src"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		public static IServiceOptionDto UpdateServiceOption(IServiceOptionDto src, IServiceOptionDto target)
		{
			target.BusinessValue = src.BusinessValue;
			target.Popularity = src.Popularity;
			target.PictureMimeType = src.PictureMimeType;
			target.Picture = src.Picture;
			target.Details = src.Details;

			return target;
		}
	}
}