using System;
using System.Collections.Generic;
using System.Configuration;
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
						var id = int.Parse(ConfigurationManager.AppSettings["GetDepartmentScriptId"]);
						var scriptGuid = _departmentController.GetDepartmentScriptFromId(id);
						string departmentName = _scriptExecutor.GetUserDepartment(newUser.AdGuid, scriptGuid);

						if (string.IsNullOrEmpty(departmentName))
						{
							throw new Exception("Login failure: no department available for this account");
						}

						try
						{
							newUser.DepartmentId = (from d in _departmentController.GetDepartments(newUser.Id)
													where d.Name == departmentName
													select d.Id).FirstOrDefault();
							if (newUser.DepartmentId < 1)   //somewhere invalid departments are not getting thrown...
							{
								throw new Exception("Login failure: no department configured for this account");
							}
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
						newUser.Department = new DepartmentDto { Name = departmentName, Id = newUser.DepartmentId }; //attach the department
						newUser.Name = GetDisplayName(newUser.AdGuid);      //Name resolution
						return newUser;
					}
				}
			}

			//failed login if there is no AD Authentication
			return new UserDto { Name = "failed" };
		}

		/// <summary>
		/// Gets the AD display name of a User directly from an AD identifier
		/// </summary>
		/// <param name="userGuid"></param>
		/// <returns></returns>
		public string GetDisplayName(Guid userGuid)
		{
			IAdSearch userSearch = new AdSearch();
			return userSearch.GetUserDisplayName(userGuid);
		}

		/// <summary>
		/// Gets a specific user from their AD identifier
		/// </summary>
		/// <param name="userGuid"></param>
		/// <returns></returns>
		public IUserDto GetUser(Guid userGuid)
		{
			return _userController.GetUser(userGuid);

		}

		/// <summary>
		/// Search AD for a list of users
		/// </summary>
		/// <param name="queryString">Display Name Query, do not append '*'</param>
		/// <returns>List of Users by their AD guids and names</returns>
		public ICollection<Tuple<Guid, string>> SearchUsers(string searchString)
		{
			IAdSearch userSearch = new AdSearch();
			return userSearch.SearchDirectoryUsers(searchString);
		}

		/// <summary>
		/// Determines whether or not the supplied userID corresponds with a user that has access to the permission provided.
		/// 
		/// example use:
		//*		bool canViewServiceDetails = UserHasPermission<ServiceDetails>(userId, ServiceDetails.CanViewServiceDetails);
		/// </summary>
		/// <typeparam name="T">Permission type</typeparam>
		/// <param name="userId">ID for user to check</param>
		/// <param name="permission">Permission level to ensure user has</param>
		/// <returns></returns>
		public bool UserHasPermission<T>(int userId, T permission)
		{
			return _permissionController.UserHasPermission(userId, permission);
		}

		/// <summary>
		/// Adds the roles to the specified user if permission allows it
		/// </summary>
		/// <param name="performingUserId">User performing the role addition</param>
		/// <param name="adjustedUserId">User having roles added</param>
		/// <param name="rolesToAdd">Roles to be added to the user</param>
		/// <returns></returns>
		public IEnumerable<IRoleDto> AddRolesToUser(int performingUserId, int adjustedUserId, IEnumerable<IRoleDto> rolesToAdd)
		{
			return _userController.AddRolesToUser(performingUserId, adjustedUserId, rolesToAdd);
		}

		/// <summary>
		/// Modifies the User in the database
		/// </summary>
		/// <param name="performingUserId">User ID for the user perfomring the modification</param>
		/// <param name="userDto"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns></returns>
		public IUserDto ModifyUser(int performingUserId, IUserDto userDto, EntityModification modification)
		{
			return _userController.ModifyUser(performingUserId, userDto, modification);
		}

		/// <summary>
		/// Get a list of all users
		/// </summary>
		/// <param name="performingUserId">user id of requestor</param>
		/// <returns></returns>
		public IEnumerable<IUserDto> GetUsers(int performingUserId)
		{
			return _userController.GetUsers(performingUserId);
		}

		public IUserDto GetUser(int performingUserId, int userId)
		{
			return _userController.GetUser(performingUserId, userId);
		}

		/// <summary>
		/// ID of the Guest User of the system
		/// </summary>
		public int GuestId { get { return _userController.GuestId; } }

		/// <summary>
		/// ID of the defaulted Administrator of the system
		/// </summary>
		public int AdministratorId { get { return _userController.AdministratorId; } }

		/// <summary>
		/// Modifies the Role in the database
		/// </summary>
		/// <param name="performingUserId">User ID for the user perfomring the modification</param>
		/// <param name="roleDto"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns></returns>
		public IRoleDto ModifyRole(int performingUserId, IRoleDto roleDto, EntityModification modification)
		{
			return _roleController.ModifyRole(performingUserId, roleDto, modification);
		}

		/// <summary>
		/// Adds a role to all provided users
		/// </summary>
		/// <param name="performingUserId">User adding the roles to users</param>
		/// <param name="roleDto">Role to add</param>
		/// <param name="users">Users having the role added to them</param>
		/// <returns>All Users with the Role added</returns>
		public IEnumerable<IUserDto> AddRoleToUsers(int performingUserId, IRoleDto roleDto, IEnumerable<IUserDto> users)
		{
			return _roleController.AddRoleToUsers(performingUserId, roleDto, users);
		}

		/// <summary>
		/// Removes a role from all provided users if they had the role to begin with
		/// </summary>
		/// <param name="performingUserId">User removing the roles from users</param>
		/// <param name="roleDto">Role to remove</param>
		/// <param name="users">Users having the role removed from them</param>
		/// <returns>All Users with the role removed</returns>
		public IEnumerable<IUserDto> RemoveRoleFromUsers(int performingUserId, IRoleDto roleDto, IEnumerable<IUserDto> users)
		{
			return _roleController.RemoveRoleFromUsers(performingUserId, roleDto, users);
		}

		/// <summary>
		/// Get all available roles
		/// </summary>
		/// <param name="performingUserId">user requesting the action</param>
		/// <returns></returns>
		public IEnumerable<IRoleDto> GetRoles(int performingUserId)
		{
			return _roleController.GetRoles(performingUserId);
		}

		/// <summary>
		/// Retrieve a single role
		/// </summary>
		/// <param name="performingUserId">user making hte request</param>
		/// <param name="roleId">role to retrieve</param>
		/// <returns></returns>
		public IRoleDto GetRole(int performingUserId, int roleId)
		{
			return _roleController.GetRole(performingUserId, roleId);
		}
	}
}
