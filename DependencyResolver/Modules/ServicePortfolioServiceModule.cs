using Ninject.Modules;
using ServicePortfolioService;
using ServicePortfolioService.Controllers;

namespace DependencyResolver.Modules
{
	public class ServicePortfolioServiceModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IPortfolioService>().To<PortfolioService>();
			Bind<IServiceController>().To<ServiceController>();
			Bind<IServiceBundleController>().To<ServiceBundleController>();
			Bind<ILifecycleStatusController>().To<LifecycleStatusController>();
		}
	}
}
