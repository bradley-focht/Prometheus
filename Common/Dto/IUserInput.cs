namespace Common.Dto
{
	public interface IUserInput
	{
		int Id { get; set; }
		int ServiceOptionId { get; set; }
		string DisplayName { get; set; }
		string HelpToolTip { get; set; }
	}
}