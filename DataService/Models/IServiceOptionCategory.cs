using Common.Dto;
using System.Collections.Generic;

namespace DataService.Models
{
	public interface IServiceOptionCategory : IOffering, IUserCreatedEntity
	{
		Service Service { get; set; }
		ICollection<ServiceOption> ServiceOptions { get; set; }
	}
}