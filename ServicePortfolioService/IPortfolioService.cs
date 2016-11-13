using ServicePortfolioService.Controllers;

namespace ServicePortfolioService
{
	public interface IPortfolioService : IServiceController, IServiceBundleController, ILifecycleStatusController
	{
	}
}