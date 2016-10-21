using ServicePortfolio.Controllers;

namespace ServicePortfolio
{
	public interface IServicePortfolio : IServiceBundleController, IServiceController, ILifecycleStatusController
	{
	}
}