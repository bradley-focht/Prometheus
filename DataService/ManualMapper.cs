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
            return new Service
            {
                Id = src.Id,
                Name = src.Name,
                Description = src.Description,
                BusinessOwner = src.BusinessOwner,
                ServiceOwner = src.ServiceOwner,
                LifecycleStatusId = src.LifecycleStatusId,
                ServiceTypeProvision = src.ServiceTypeProvision,
                ServiceTypeRole = src.ServiceTypeRole
            };
        }

        public static ServiceDto MapServiceToDto(IService src)
        {
            return new ServiceDto
            {
                Id = src.Id,
                Name = src.Name,
                Description = src.Description,
                BusinessOwner = src.BusinessOwner,
                ServiceOwner = src.ServiceOwner,
                LifecycleStatusId = src.LifecycleStatusId,
                ServiceTypeProvision = src.ServiceTypeProvision,
                ServiceTypeRole = src.ServiceTypeRole
            };
        }
    }
}
