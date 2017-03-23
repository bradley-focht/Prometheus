namespace Common.Enums.Permissions
{
	/// <summary>
	/// Describes the level of access a user has to use the API calls.
	/// </summary>
	public enum ApiAccess
	{
		/// <summary>
		/// User cannot perform any API call
		/// </summary>
		NoAccess,

		/// <summary>
		/// User can only perform API calls on Service Requests the user is either
		///	the requestor or approver for.
		/// </summary>
		OnlyUsersRequests,

		/// <summary>
		/// User can perform all API calls without restriction
		/// </summary>
		FullApiAccess
	}
}