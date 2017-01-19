using ServicePortfolioService.Controllers;

namespace ServicePortfolioService
{
	public interface IPortfolioService : IServiceController, IServiceBundleController, ILifecycleStatusController,
		IServiceSwotController, ISwotActivityController, IServiceDocumentController, IServiceGoalController,
		IServiceContractController, IServiceWorkUnitController, IServiceMeasureController, IServiceOptionController,
        IServiceProcessController, IOptionCategoryController, ITextInputController, ISelectionInputController, IScriptedSelectionController
	{
	}
}