using System.Collections.Generic;
using Common.Dto;

namespace DataService.Models
{
	public interface ITextInput : IUserInput
	{
		/// <summary>
		/// Allow a single line or multi-line textbox
		/// </summary>
		bool MultiLine { get; set; }

		/// <summary>
		/// To which service options this input applies
		/// </summary>
		ICollection<ServiceOption> ServiceOptions { get; set; }
	}
}
