using System;
using System.Collections.Generic;
using Common.Dto;
using UserManager.Controllers;

namespace UserManager
{
	public interface IUserManager : IPermissionController, IUserController, IRoleController
	{
		/// <summary>
		/// Authenticates the user based on the credentials provided
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		IUserDto Login(string username, string password);

		/// <summary>
		/// Search AD for a list of users
		/// </summary>
		/// <param name="queryString">Display Name Query, do not append '*'</param>
		/// <returns>List of Users by their AD guids and names</returns>
		ICollection<Tuple<Guid, string>> SearchUsers(string searchString);

		/// <summary>
		/// Gets the AD display name of a User directly from an AD identifier
		/// </summary>
		/// <param name="userGuid"></param>
		/// <returns></returns>
		string GetDisplayName(Guid userGuid);
	}
}