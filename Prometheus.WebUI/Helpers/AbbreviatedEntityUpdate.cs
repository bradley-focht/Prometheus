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
        public static ServiceDto UpdateService(ServiceAbbreviatedModel src, IServiceDto target)
        {
            target.BusinessValue = src.BusinessValue;
            target.Popularity = src.Popularity;

            return (ServiceDto)target;
        }

        /// <summary>
        /// preserve catalogable attributes
        /// </summary>
        /// <param name="src"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static ServiceDto UpdateService(IServiceDto src, IServiceDto target)
        {
            target.BusinessValue = src.BusinessValue;
            target.Popularity = src.Popularity;

            return (ServiceDto)target;
        }

        /// <summary>
        /// preserve catalogable attributes
        /// </summary>
        /// <param name="src"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static ServiceOptionCategoryDto UpdateServiceCategory(ServiceCategoryAbbreviatedModel src, IServiceOptionCategoryDto target)
        {
            target.BusinessValue = src.BusinessValue;
            target.Popularity = src.Popularity;
            target.Quantifiable = src.Quantifiable;

            return (ServiceOptionCategoryDto)target;
        }

        /// <summary>
        /// preserve catalogable attributes
        /// </summary>
        /// <param name="src"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static ServiceOptionCategoryDto UpdateServiceCategory(IServiceOptionCategoryDto src, IServiceOptionCategoryDto target)
        {
            target.BusinessValue = src.BusinessValue;
            target.Popularity = src.Popularity;
            target.Quantifiable = src.Quantifiable;

            return (ServiceOptionCategoryDto)target;
        }

        /// <summary>
        /// transfer new data onto an existing entity
        /// </summary>
        /// <param name="src"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static ServiceOptionDto UpdateServiceOption(ServiceOptionAbbreviatedModel src, IServiceOptionDto target)
        {
            target.BusinessValue = src.BusinessValue;
            target.Popularity = src.Popularity;
            target.PictureMimeType = src.PictureMimeType;
            target.Picture = src.Picture;
            target.Details = src.Details;

            return (ServiceOptionDto)target;
        }

        /// <summary>
        /// transfer new data onto an existing entity
        /// </summary>
        /// <param name="src"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static ServiceOptionDto UpdateServiceOption(IServiceOptionDto src, IServiceOptionDto target)
        {
            target.BusinessValue = src.BusinessValue;
            target.Popularity = src.Popularity;
            target.PictureMimeType = src.PictureMimeType;
            target.Picture = src.Picture;
            target.Details = src.Details;

            return (ServiceOptionDto)target;
        }
    }
}