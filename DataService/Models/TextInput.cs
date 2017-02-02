namespace DataService.Models
{
	public class TextInput : ITextInput
	{
		public int Id { get; set; }
		public string DisplayName { get; set; }
		public string HelpToolTip { get; set; }
		public bool MultiLine { get; set; }
	}
}