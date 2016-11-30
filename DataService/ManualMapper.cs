using System.Collections.Generic;
using Common.Dto;
using DataService.Models;

namespace DataService
{
    /// <summary>
    /// Temporary replacement for Automapper. As you can see, it is of limited use. But hey, it works.
    /// </summary>
    public class ManualMapper
    {
        public static LifecycleStatusDto MapLifecycleStatusToDto(ILifecycleStatus src)
        {
            if (src == null) return null;

            return new LifecycleStatusDto
            {
                Id = src.Id,
                Name = src.Name,
                CatalogVisible = src.CatalogVisible,
                Position = src.Position
            };
        }

        public static LifecycleStatus MapDtoToLifecycleStatus(ILifecycleStatusDto src)
        {
            if (src == null) return null;

            return new LifecycleStatus
            {
                Id = src.Id,
                Name = src.Name,
                CatalogVisible = src.CatalogVisible,
                Position = src.Position
            };
        }

        public static Service MapDtoToService(IServiceDto src)
        {
            if (src == null) return null;

            return new Service
            {
                Id = src.Id,
                Name = src.Name,
                Description = src.Description,
                BusinessOwner = src.BusinessOwner,
                ServiceOwner = src.ServiceOwner,
                LifecycleStatusId = src.LifecycleStatusId,
                ServiceTypeProvision = src.ServiceTypeProvision,
                ServiceTypeRole = src.ServiceTypeRole,
                ServiceBundleId = src.ServiceBundleId
            };
        }

        /// <summary>
        /// Contains all information about the service, all sections
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static ServiceDto MapServiceToDto(IService src)
        {
            if (src == null) return null;

            var serviceDto = new ServiceDto
            {
                Id = src.Id,
                Name = src.Name,
                Description = src.Description,
                BusinessOwner = src.BusinessOwner,
                ServiceOwner = src.ServiceOwner,
                LifecycleStatusId = src.LifecycleStatusId,
                ServiceTypeProvision = src.ServiceTypeProvision,
                ServiceTypeRole = src.ServiceTypeRole,
                ServiceBundleId = src.ServiceBundleId,

                LifecycleStatusDto = MapLifecycleStatusToDto(src.LifecycleStatus)
            };

            if (src.ServiceDocuments != null)
            {
                serviceDto.ServiceDocuments = new List<IServiceDocumentDto>();
                foreach (var doc in src.ServiceDocuments)
                {
                    serviceDto.ServiceDocuments.Add(MapServiceDocumentToDto(doc));
                }
            }

            serviceDto.LifecycleStatusDto = MapLifecycleStatusToDto(src.LifecycleStatus);

            return serviceDto;
        }

        public static ServiceBundleDto MapServiceBundleToDto(IServiceBundle src)
        {
            if (src == null) return null;

            return new ServiceBundleDto
            {
                Id = src.Id,
                Name = src.Name,
                Description = src.Description,
                BusinessValue = src.BusinessValue,
                Measures = src.Measures,

            };
        }


        public static ServiceBundle MapDtoToServiceBundle(IServiceBundleDto src)
        {
            if (src == null) return null;

            return new ServiceBundle
            {
                Id = src.Id,
                Name = src.Name,
                Description = src.Description,
                BusinessValue = src.BusinessValue,
                Measures = src.Measures,

            };


        }


        public static ServiceDocument MapDtoToServiceDocument(IServiceDocumentDto src)
        {
            if (src == null) return null;

            return new ServiceDocument
            {
                Id = src.Id,
                ServiceId = src.ServiceId,
                StorageNameGuid = src.StorageNameGuid,
                Filename = src.Filename,
                FileExtension = src.FileExtension

            };
        }

        public static ServiceDocumentDto MapServiceDocumentToDto(IServiceDocument src)
        {
            if (src == null) return null;

            return new ServiceDocumentDto
            {
                Id = src.Id,
                ServiceId = src.ServiceId,
                StorageNameGuid = src.StorageNameGuid,
                Filename = src.Filename,
                FileExtension = src.FileExtension

            };
        }

    }
}
