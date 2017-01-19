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

		ICollection<Tuple<Guid, string>> SearchUsers(string searchString);
	}
}