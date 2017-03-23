namespace Common.Enums.Permissions
{
	/// <summary>
	/// Describes the level of access a user has to assign Roles to Users
	/// </summary>
	public enum UserRoleAssignment
	{
		/// <summary>
		/// User cannot access Roles
		/// </summary>
		NoAccess,

		/// <summary>
		/// User can view the Roles assigned to them
		/// </summary>
		CanViewRoles,

		/// <summary>
		/// User can assign Roles to Users
		/// </summary>
		CanAssignRoles
	}
}