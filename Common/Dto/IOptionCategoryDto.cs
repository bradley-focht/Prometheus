
using System.Collections.Generic;

namespace Common.Dto
{
    public interface IOptionCategoryDto
    {
        int Id { get; set; }
        int Popularity { get; set; }
        int ServiceId { get; set; }
        string Name { get; set; }
        string BusinessValue { get; set; }
        ICollection<ServiceOptionDto> ServiceOptions { get; set; }
    }
}