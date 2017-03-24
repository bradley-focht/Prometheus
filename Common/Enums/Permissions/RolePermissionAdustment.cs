namespace Common.Enums.Permissions
{
	/// <summary>
	/// Describes the level of access a user has to change the Permission levels that Roles have
	/// </summary>
	public enum RolePermissionAdjustment
	{
		/// <summary>
		/// User cannot access Role Permissions
		/// </summary>
		NoAccess,

		/// <summary>
		/// User can view the Permission levels that Roles have
		/// </summary>
		CanViewRolePermissions,

		/// <summary>
		/// User can change the Permission levels that Roles have
		/// </summary>
		CanAdjustRolePermissions
	}
}
