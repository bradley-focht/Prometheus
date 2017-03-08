using Ninject.Modules;
using ServicePortfolioService;
using ServicePortfolioService.Controllers;

namespace DependencyResolver.Modules
{
	public class ServicePortfolioServiceModule : NinjectModule
	{
		public override void Load()
		{
			LoadControllers();
			LoadService();
		}

		private void LoadService()
		{
			Bind<IPortfolioService>().To<PortfolioService>();
		}

		private void LoadControllers()
		{
			Bind<ILifecycleStatusController>().To<LifecycleStatusController>();
			Bind<IScriptedSelectionInputController>().To<ScriptedSelectionInputController>();
			Bind<ISelectionInputController>().To<SelectionInputController>();
			Bind<IServiceBundleController>().To<ServiceBundleController>();
			Bind<IServiceContractController>().To<ServiceContractController>();
			Bind<IServiceController>().To<ServiceController>();
			Bind<IServiceDocumentController>().To<ServiceDocumentController>();
			Bind<IServiceGoalController>().To<ServiceGoalController>();
			Bind<IServiceMeasureController>().To<ServiceMeasureController>();
			Bind<IServiceOptionCategoryController>().To<ServiceOptionCategoryController>();
			Bind<IServiceOptionController>().To<ServiceOptionController>();
			Bind<IServiceProcessController>().To<ServiceProcessController>();
			Bind<IServiceRequestPackageController>().To<ServiceRequestPackageController>();
			Bind<IServiceSwotController>().To<ServiceSwotController>();
			Bind<IServiceWorkUnitController>().To<ServiceWorkUnitController>();
			Bind<ISwotActivityController>().To<SwotActivityController>();
			Bind<ITextInputController>().To<TextInputController>();
		}
	}
}
