using Ninject.Modules;
using UserManager;
using UserManager.Controllers;
using IUserController = UserManager.Controllers.IUserController;

namespace DependencyResolver.Modules
{
	public class UserManagerModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IPermissionController>().To<PermissionController>();
			Bind<IRoleController>().To<RoleController>();
			Bind<IUserController>().To<UserController>();
			Bind<IUserManager>().To<UserManagerService>();
		}
	}
}
