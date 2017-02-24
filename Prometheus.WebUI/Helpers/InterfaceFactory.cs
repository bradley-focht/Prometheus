﻿using RequestService;
using RequestService.Controllers;
using ServicePortfolioService;
using ServicePortfolioService.Controllers;
using UserManager;
using UserManager.Controllers;

namespace Prometheus.WebUI.Helpers
{
	public class InterfaceFactory
	{
		/// <summary>
		/// Creat the Portfolio Service Interface
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public static PortfolioService CreatePortfolioService()
		{
			return new PortfolioService(
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
				new ServiceOptionCategoryController(),
				new ServiceProcessController(),
				new TextInputController(),
				new SelectionInputController(),
				new ScriptedSelectionInputController(),
				new ServiceRequestPackageController(),
				new ServiceRequestController());
		}

		/// <summary>
		/// Create a Catalog Controller, no interface
		/// </summary>
		/// <param name="dummyId"></param>
		/// <returns></returns>
		public static ICatalogController CreateCatalogController()
		{
			return new CatalogController(CreateUserManagerService());
		}

		/// <summary>
		/// Create the User Manager Interface
		/// </summary>
		/// <returns></returns>
		public static UserManagerService CreateUserManagerService()
		{
			return new UserManagerService(
				new PermissionController(),
				new UserController(),
				new RoleController(new PermissionController())
				);
		}

		/// <summary>
		/// Create a new Service Request Interface/Controller
		/// </summary>
		/// <returns></returns>
		public static ServiceRequestOptionController CreateServiceRequestOptionController()
		{
			return new ServiceRequestOptionController();
		}

		/// <summary>
		/// Service Request Management interface
		/// </summary>
		/// <returns></returns>
		public static RequestManager CreateRequestManager()
		{
			return new RequestManager();
		}
	}
}