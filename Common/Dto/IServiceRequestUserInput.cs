using Common.Enums;

namespace Common.Dto
{
	public interface IServiceRequestUserInputDto
	{
		int Id { get; set; }
		UserInputTypes UserInputType { get; set; }
		string Name { get; set; } 
		string Value { get; set; }
	}
}
