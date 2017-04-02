namespace Common.Enums
{
	/// <summary>
	/// Possible Actions an SR can perform
	/// </summary>
	public enum ServiceRequestAction
	{
		/// <summary>
		/// Service Request will perform a creation function
		/// </summary>
		New,

		/// <summary>
		/// Service Request will perform an update function
		/// </summary>
		Change,

		/// <summary>
		/// Service Request will perform a deletion function
		/// </summary>
		Remove
	}
}
