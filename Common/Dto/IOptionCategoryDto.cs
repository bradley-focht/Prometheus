
using System.Collections.Generic;

namespace Common.Dto
{
    public interface IOptionCategoryDto : IOffering
    {
        ICollection<ServiceOptionDto> ServiceOptions { get; set; }
    }
}