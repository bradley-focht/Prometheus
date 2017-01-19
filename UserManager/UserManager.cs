using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using Common.Dto;
using Common.Enums.Entities;
using DataService;
using DataService.DataAccessLayer;
using UserManager.AdService;
using UserManager.Controllers;

namespace UserManager
{
	public class UserManagerService : IUserManager
	{
		private readonly IPermissionController _permissionController;
		private IUserController _userController;
		private IRoleController _roleController;
		private const string AuthorizedUserRoleName = "Authorized User";

		public UserManagerService(IPermissionController permissionController, IUserController userController, IRoleController roleController)
		{
			_permissionController = permissionController;
			_userController = userController;
			_roleController = roleController;
		}

		/// <summary>
		/// Attempts authentication through AD and then adds the user to the DB if they do not already exist with the 
		/// "Authorized User" role added as a default.
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public IUserDto Login(string username, string password)
		{
			AdUser adUser = new AdUser();
			if (adUser.AuthenticateUser(username, password))
			{
				using (var context = new PrometheusContext())
				{
					//See if the user exists already
					var user = context.Users.FirstOrDefault(x => x.AdGuid == adUser.UserGuid);
					if (user != null)
					{
						//If they existed retrun them
						return ManualMapper.MapUserToDto(user);
					}
					else
					{
						//Otherwise add them with the authenticated role
						var userDto = new UserDto()
						{
							AdGuid = adUser.UserGuid,
							Name = adUser.DisplayName
						};

						//Get the role that is to be added to the user
						var authenticatedRole = context.Roles.FirstOrDefault(x => x.Name == AuthorizedUserRoleName);

						//Add them and their role to the database
						var savedUser = context.Users.Add(ManualMapper.MapDtoToUser(userDto));
						savedUser.Roles.Add(authenticatedRole);
						context.SaveChanges();
						return ManualMapper.MapUserToDto(savedUser);
					}
				}
			}
			throw new AuthenticationException("Username and password could not authenticate with Active Directory");
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
