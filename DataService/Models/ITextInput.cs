using Common.Dto;

namespace DataService.Models
{
	public interface ITextInput : IUserInput
	{
		bool MultiLine { get; set; }
	}
}
