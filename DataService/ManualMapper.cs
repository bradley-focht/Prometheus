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
    }
}
