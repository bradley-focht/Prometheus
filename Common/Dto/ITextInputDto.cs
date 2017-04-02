namespace Common.Dto
{
	/// <summary>
	/// Text Input for Service Requests
	/// </summary>
	public interface ITextInputDto : IUserInput
	{
		/// <summary>
		/// false for Textbox, true for Textarea
		/// </summary>
		bool MultiLine { get; set; }
	}
}
