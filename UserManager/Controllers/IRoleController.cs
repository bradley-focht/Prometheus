using System.Collections.Generic;
using Common.Dto;
using Common.Enums.Entities;

namespace UserManager.Controllers
{
	public interface IRoleController
	{
		/// <summary>
		/// Modifies the Role in the database
		/// </summary>
		/// <param name="performingUserId">User ID for the user perfomring the modification</param>
		/// <param name="roleDto"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns></returns>
		IRoleDto ModifyRole(int performingUserId, IRoleDto roleDto, EntityModification modification);

		/// <summary>
		/// Adds a role to all provided users
		/// </summary>
		/// <param name="performingUserId">User adding the roles to users</param>
		/// <param name="roleDto">Role to add</param>
		/// <param name="users">Users having the role added to them</param>
		/// <returns>All Users with the Role added</returns>
		IEnumerable<IUserDto> AddRoleToUsers(int performingUserId, IRoleDto roleDto, IEnumerable<IUserDto> users);

		/// <summary>
		/// Removes a role from all provided users if they had the role to begin with
		/// </summary>
		/// <param name="performingUserId">User removing the roles from users</param>
		/// <param name="roleDto">Role to remove</param>
		/// <param name="users">Users having the role removed from them</param>
		/// <returns>All Users with the role removed</returns>
		IEnumerable<IUserDto> RemoveRoleFromUsers(int performingUserId, IRoleDto roleDto, IEnumerable<IUserDto> users);

		/// <summary>
		/// Get all available roles
		/// </summary>
		/// <param name="performingUserId">user requesting the action</param>
		/// <returns></returns>
		IEnumerable<RoleDto> GetRoles(int performingUserId);
		/// <summary>
		/// Retrieve a single role
		/// </summary>
		/// <param name="performingUserId">user making hte request</param>
		/// <param name="roleId">role to retrieve</param>
		/// <returns></returns>
		RoleDto GetRole(int performingUserId, int roleId);

	}
}