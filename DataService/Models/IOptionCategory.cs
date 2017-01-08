using System.Collections.Generic;
using Common.Dto;

namespace DataService.Models
{
    public interface IOptionCategory: IOffering, IUserCreatedEntity
    {
        ICollection<ServiceOption> ServiceOptions { get; set; }
    }
}