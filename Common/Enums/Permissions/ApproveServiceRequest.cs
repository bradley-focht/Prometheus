namespace Common.Enums.Permissions
{
	/// <summary>
	/// Describes the level of access a user has to Approve Service Requests
	/// </summary>
	public enum ApproveServiceRequest
	{
		/// <summary>
		/// User cannot Approve any Service Requests
		/// </summary>
		NoAccess,

		/// <summary>
		/// User can Approve Service Requests that have the Basic property set to true for all Service Options
		///	on the Service Request
		/// </summary>
		ApproveBasicRequests,

		/// <summary>
		/// User can Approve any Service Request made under their Department
		/// </summary>
		ApproveDepartmentRequests,

		/// <summary>
		/// User can Approve any Service Request
		/// </summary>
		ApproveAnyRequests
	}
}