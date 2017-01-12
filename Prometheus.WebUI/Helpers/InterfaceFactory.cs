using RequestService;
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
		        new SwotActivityController(),
		        new ServiceDocumentController(),
		        new ServiceGoalController(),
		        new ServiceContractController(),
		        new ServiceWorkUnitController(),
		        new ServiceMeasureController(),
		        new ServiceOptionController(),
		        new OptionCategoryController(),
		        new ServiceProcessController(),
		        new TextInputController());
        }

	    public static ICatalogController CreateCatalogController(int dummyId)
	    {
			return new CatalogController(dummyId);
		} 
	}
}