namespace Common.Dto
{
	public interface IUserInput
	{
		int Id { get; set; }

		/// <summary>
		/// User friendly name displayed
		/// </summary>
		string DisplayName { get; set; }

		/// <summary>
		/// Helpful tool tip
		/// </summary>
		string HelpToolTip { get; set; }

		/// <summary>
		/// If the Input is to be used on ServiceRequestAction.New
		/// </summary>
		bool AvailableOnAdd { get; set; }

		/// <summary>
		/// If the Input is to be used on ServiceRequestAction.Change
		/// </summary>
		bool AvailableOnChange { get; set; }

		/// <summary>
		/// If the Input is to be used on ServiceRequestAction.Remove
		/// </summary>
		bool AvailableOnRemove { get; set; }
	}
}