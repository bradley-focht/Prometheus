using Ninject.Modules;
using RequestService.Controllers;

namespace DependencyResolver.Modules
{
	public class RequestServiceModule : NinjectModule
	{
		public override void Load()
		{
			Bind<ICatalogController>().To<CatalogController>();
		}
	}
}
