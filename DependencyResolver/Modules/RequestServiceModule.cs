using Ninject.Modules;
using RequestService;
using RequestService.Controllers;

namespace DependencyResolver.Modules
{
	public class RequestServiceModule : NinjectModule
	{
		public override void Load()
		{
			LoadControllers();
			LoadManager();
		}

		private void LoadManager()
		{
			Bind<IRequestManager>().To<RequestManager>();
		}

		private void LoadControllers()
		{
			Bind<ICatalogController>().To<CatalogController>();
			Bind<IScriptFileController>().To<ScriptFileController>();
			Bind<IServiceRequestController>().To<ServiceRequestController>();
			Bind<IServiceRequestOptionController>().To<ServiceRequestOptionController>();
			Bind<IServiceRequestUserInputController>().To<ServiceRequestUserInputController>();
		}
	}
}
