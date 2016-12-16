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
			Bind<IServiceContractController>().To<ServiceContractController>();
			Bind<IServiceDocumentController>().To<ServiceDocumentController>();
			Bind<IServiceGoalController>().To<ServiceGoalController>();
			Bind<IServiceMeasureController>().To<ServiceMeasureController>();
			Bind<IServiceSwotController>().To<ServiceSwotController>();
			Bind<IServiceWorkUnitController>().To<ServiceWorkUnitController>();
			Bind<ISwotActivityController>().To<SwotActivityController>();
		}
	}
}
