namespace Common.Dto
{
	/// <summary>
	/// Combobox selection style input
	/// </summary>
	public interface ISelectionInputDto : ISelectable
	{
		/// <summary>
		/// List of items selected separated by the delimiter
		/// </summary>
		string SelectItems { get; set; }

		/// <summary>
		/// Delimiter for separation of selected items
		/// </summary>
		string Delimiter { get; set; }
	}
}