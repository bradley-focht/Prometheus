
namespace Common.Dto
{
	public interface ISelectable : IUserInput
	{
		/// <summary>
		/// Maximum number of items that can be selected
		/// </summary>
		int NumberToSelect { get; set; }
	}
}