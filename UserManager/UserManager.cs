using System;
using System.Collections.Generic;
using Common.Dto;
using Common.Enums.Entities;
using UserManager.AdService;
using UserManager.Controllers;

namespace UserManager
{
	public class UserManagerService : IUserManager
	{
		private readonly IPermissionController _permissionController;
		private IUserController _userController;
		private IRoleController _roleController;

		public UserManagerService(IPermissionController permissionController, IUserController userController, IRoleController roleController)
		{
			_permissionController = permissionController;
			_userController = userController;
			_roleController = roleController;
		}


		//TODO: Sean implement login
		public UserDto Login(string username, string password)
		{
			// <hack>I'm in</hack>
			//return null;      //perhaps not

			IDirectoryUser user = new AdUser();
			if (user.AuthenticateUser(username, password))
			{
				return new UserDto
				{
					Name = user.DisplayName,
					//Id = user.UserGuid.ToInt(), //this doesn't seem to work... hmmm
					Id = 0,
					Password = "bubba lou", //maybe not a field that is needed...
					Role = new RoleDto { Name = "God Mode" }
				};
			}
			return null;
		}


		public string GetDisplayName(Guid userGuid)
		{
			IAdSearch userSearch = new AdSearch();
			return userSearch.GetUserDisplayName(userGuid);
		}

		public ICollection<Tuple<Guid, string>> SearchUsers(string searchString)
		{
			IAdSearch userSearch = new AdSearch();
			return userSearch.SearchDirectoryUsers(searchString);
		}

		public bool UserHasPermission<T>(int userId, T permission)
		{
			return _permissionController.UserHasPermission(userId, permission);
		}

		public IEnumerable<IRoleDto> AddRolesToUser(int performingUserId, int adjustedUserId, IEnumerable<IRoleDto> rolesToAdd)
		{
			return _userController.AddRolesToUser(performingUserId, adjustedUserId, rolesToAdd);
		}

		public IUserDto ModifyUser(int performingUserId, IUserDto userDto, EntityModification modification)
		{
			return _userController.ModifyUser(performingUserId, userDto, modification);
		}

		public IEnumerable<UserDto> GetUsers(int performingUserId)
		{
			return _userController.GetUsers(performingUserId);
		}

		public UserDto GetUser(int performingUserId, int userId)
		{
			return _userController.GetUser(performingUserId, userId);
		}

		public IRoleDto ModifyRole(int performingUserId, IRoleDto roleDto, EntityModification modification)
		{
			return _roleController.ModifyRole(performingUserId, roleDto, modification);
		}

		public IEnumerable<IUserDto> AddRoleToUsers(int performingUserId, IRoleDto roleDto, IEnumerable<IUserDto> users)
		{
			return _roleController.AddRoleToUsers(performingUserId, roleDto, users);
		}

		public IEnumerable<IUserDto> RemoveRoleFromUsers(int performingUserId, IRoleDto roleDto, IEnumerable<IUserDto> users)
		{
			return _roleController.RemoveRoleFromUsers(performingUserId, roleDto, users);
		}

		public IEnumerable<RoleDto> GetRoles(int performingUserId)
		{
			return _roleController.GetRoles(performingUserId);
		}

		public RoleDto GetRole(int performingUserId, int roleId)
		{
			return _roleController.GetRole(performingUserId, roleId);
		}
	}
}
