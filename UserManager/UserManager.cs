using System;
using System.Collections.Generic;
using System.Linq;
using Common.Dto;
using Common.Enums.Entities;
using Common.Utilities;
using DataService;
using DataService.DataAccessLayer;
using DataService.Models;
using UserManager.AdService;
using UserManager.Controllers;

namespace UserManager
{
	public class UserManagerService : IUserManager
	{
		private readonly IPermissionController _permissionController;
		private readonly IUserController _userController;
		private readonly IRoleController _roleController;
		private const string AuthorizedUserRoleName = "Authorized User";

		private readonly IScriptExecutor _scriptExecutor;        // is this a place we should be executing scripts from?
		private readonly IDepartmentController _departmentController;

		public UserManagerService(IPermissionController permissionController, IUserController userController, IRoleController roleController, IScriptExecutor scriptExecutor, IDepartmentController departmentController)
		{
			_permissionController = permissionController;
			_userController = userController;
			_roleController = roleController;
			_scriptExecutor = scriptExecutor;
			_departmentController = departmentController;
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
					IUserDto user = null;
					try
					{
						user = GetUser(adUser.UserGuid);
					}
					catch (Exception) { /* user does not exist */ }

					if (user != null)
					{
						//If they existed retrun them
						user.Name = GetDisplayName(user.AdGuid);
						return user;
					}
					else
					{
						//Otherwise add them with the authenticated role
						var newUser = new UserDto { AdGuid = adUser.UserGuid };

						//Get the role that is to be added to the user
						var authenticatedRole = context.Roles.FirstOrDefault(x => x.Name == AuthorizedUserRoleName);

						//get the user's department

						string departmentName = _scriptExecutor.GetUserDepartment(newUser.AdGuid);

						if (string.IsNullOrEmpty(departmentName))
						{
							throw new Exception("Login failure: no department available for this account");
						}

						try
						{
							newUser.DepartmentId = (from d in _departmentController.GetDepartments(newUser.Id)
								where d.Name == departmentName
								select d.Id).FirstOrDefault();
						}
						catch (Exception)
						{
							throw new Exception("Login failure: no department configured for this account");
						}
						//Add them and their role to the database
						var savedUser = context.Users.Add(ManualMapper.MapDtoToUser(newUser));
						savedUser.Roles = new List<Role> { authenticatedRole };
						context.SaveChanges();
						newUser = (UserDto)ManualMapper.MapUserToDto(savedUser);
						newUser.Name = GetDisplayName(newUser.AdGuid);      //Name resolution
						return newUser;
					}
				}
			}
			return new UserDto { Name = "failed" }; //failed login
		}


		public string GetDisplayName(Guid userGuid)
		{
			IAdSearch userSearch = new AdSearch();
			return userSearch.GetUserDisplayName(userGuid);
		}

		public IUserDto GetUser(Guid userGuid)
		{
			return _userController.GetUser(userGuid);

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

		public IEnumerable<IUserDto> GetUsers(int performingUserId)
		{
			return _userController.GetUsers(performingUserId);
		}

		public IUserDto GetUser(int performingUserId, int userId)
		{
			return _userController.GetUser(performingUserId, userId);
		}

		public int GuestId { get { return _userController.GuestId; } }
		public int AdministratorId { get { return _userController.AdministratorId; } }

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

		public IEnumerable<IRoleDto> GetRoles(int performingUserId)
		{
			return _roleController.GetRoles(performingUserId);
		}

		public IRoleDto GetRole(int performingUserId, int roleId)
		{
			return _roleController.GetRole(performingUserId, roleId);
		}
	}
}
