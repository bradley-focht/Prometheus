using System.Collections.Generic;
using System.Linq;
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

        public static ServiceSwotDto MapServiceSwotToDto(IServiceSwot src)
        {
            if (src == null) return null;

            ServiceSwotDto serviceSwotDto = new ServiceSwotDto
            {
                Id = src.Id,
                Description = src.Description,
                Type = src.Type,
                Item = src.Item,
                ServiceId =  src.ServiceId
            };

            if (src.SwotActivities != null && src.SwotActivities.Any())
            {
                serviceSwotDto.SwotActivities = new List<ISwotActivityDto>();
                foreach (var activity in src.SwotActivities)
                {
                    serviceSwotDto.SwotActivities.Add(MapSwotActivityToDto(activity));
                }
            }
            return serviceSwotDto;           
        }

        public static SwotActivityDto MapSwotActivityToDto(ISwotActivity src)
        {
            if (src == null) { return null; }

            return new SwotActivityDto
            {
                Id = src.Id,
                Description = src.Description,
                Date = src.Date,
                Name = src.Name,
                ServiceSwotId = src.ServiceSwotId
            };
        }

        public static ServiceSwot MapDtoToServiceSwot(IServiceSwotDto src)
        {
            if (src == null) return null;

            ServiceSwot serviceSwot = new ServiceSwot
            {
                Id = src.Id,
                Description = src.Description,
                Type = src.Type,
                Item = src.Item,
                ServiceId = src.ServiceId
            };

            if (src.SwotActivities != null && src.SwotActivities.Any())
            {
                serviceSwot.SwotActivities = new List<SwotActivity>();
                foreach (var activity in src.SwotActivities)
                {
                    serviceSwot.SwotActivities.Add(MapDtoToSwotActivity(activity));
                }
            }
            return serviceSwot;
        }

        public static SwotActivity MapDtoToSwotActivity(ISwotActivityDto src)
        {
            if (src == null) { return null; }

            return new SwotActivity
            {
                Id = src.Id,
                Description = src.Description,
                Date = src.Date,
                Name = src.Name,
                ServiceSwotId = src.ServiceSwotId
            };
        }

        public static ServiceProcessDto MapServiceProcessToDto(IServiceProcess src)
        {
            if (src == null) { return null; }

            return new ServiceProcessDto
            {
                Id = src.Id,
                Description = src.Description,
                ServiceId = src.ServiceId,
                Name = src.Name,
                Improvements = src.Improvements,
                Benefits = src.Benefits
            };
        }

        public static ServiceProcess MapDtoToServiceProcess(IServiceProcessDto src)
        {
            if (src == null) { return null; }

            return new ServiceProcess
            {
                Id = src.Id,
                Description = src.Description,
                ServiceId = src.ServiceId,
                Name = src.Name,
                Improvements = src.Improvements,
                Benefits = src.Benefits
            };
        }


    }
}