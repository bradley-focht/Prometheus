using ServicePortfolioService.Controllers;

namespace ServicePortfolioService
{
	public interface IPortfolioService : IServiceBundleController, IServiceController, ILifecycleStatusController
	{
	}
}