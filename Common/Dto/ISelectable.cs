
namespace Common.Dto
{
	/// <summary>
	/// Inputs where selections can be made
	/// </summary>
	public interface ISelectable : IUserInput
	{
		/// <summary>
		/// Maximum number of items that can be selected
		/// </summary>
		int NumberToSelect { get; set; }
	}
}