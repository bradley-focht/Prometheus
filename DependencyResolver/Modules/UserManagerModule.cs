using Ninject.Modules;
using UserManager;
using UserManager.AdService;
using UserManager.Controllers;

namespace DependencyResolver.Modules
{
	public class UserManagerModule : NinjectModule
	{
		public override void Load()
		{
			LoadAdService();
			LoadControllers();
			LoadService();
		}

		private void LoadService()
		{
			Bind<IUserManager>().To<UserManagerService>();
		}

		private void LoadControllers()
		{
			Bind<IPermissionController>().To<PermissionController>();
			Bind<IRoleController>().To<RoleController>();
			Bind<IUserController>().To<UserController>();
			Bind<IDepartmentController>().To<DepartmentController>();
		}

		private void LoadAdService()
		{
			Bind<IAdSearch>().To<AdSearch>();
			Bind<IDirectoryUser>().To<AdUser>();
		}
	}
}
