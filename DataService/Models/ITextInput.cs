using System.Collections.Generic;
using Common.Dto;

namespace DataService.Models
{
	public interface ITextInput : IUserInput
	{
		bool MultiLine { get; set; }

		ICollection<ServiceOption> ServiceOptions { get; set; }
	}
}
