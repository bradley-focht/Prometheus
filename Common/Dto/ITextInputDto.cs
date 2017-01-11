namespace Common.Dto
{
	public interface ITextInputDto : IUserInput
	{
		bool MultiLine { get; set; }
	}
}
