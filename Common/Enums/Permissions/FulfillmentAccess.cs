namespace Common.Enums.Permissions
{
	/// <summary>
	/// Describes the level of access a user has to Fulfill Service Requests
	/// </summary>
	public enum FulfillmentAccess
	{
		/// <summary>
		/// User cannot fulfill Service Requests
		/// </summary>
		NoAccess,

		/// <summary>
		/// User can fulfill any Service Request
		/// </summary>
		CanFulfill
	}
}