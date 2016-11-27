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

        public static ServiceDto MapServiceToDto(IService src)
        {
            if (src == null) return null;

            return new ServiceDto
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

        public static ServiceDocumentDto MapServiceDocumentToDto(IServiceDocumentDto src)
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
