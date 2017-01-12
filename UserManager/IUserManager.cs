using System;
using System.Collections.Generic;
using Common.Dto;
using UserManager.Controllers;

namespace UserManager
{
	public interface IUserManager : IPermissionController, IUserController, IRoleController
	{
		IUserDto Login(string username, string password);
		ICollection<Tuple<Guid, string>> SearchUsers(string searchString);
	}
}