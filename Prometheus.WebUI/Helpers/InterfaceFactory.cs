using ServicePortfolioService;
using ServicePortfolioService.Controllers;

namespace Prometheus.WebUI.Helpers
{
    public class InterfaceFactory
    {
        public static PortfolioService CreatePortfolioService(int userId)
        {
            return new PortfolioService(userId, 
                new ServiceBundleController(),
                new ServiceController(),
                new LifecycleStatusController(), 
                new ServiceSwotController(), 
                new SwotActivityController());
        }
    }
}