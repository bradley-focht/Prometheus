using System;
using System.Collections.Generic;
using Common.Dto;
using Common.Enums.Entities;
using ServicePortfolioService.Controllers;

namespace ServicePortfolioService
{
	public class PortfolioService : IPortfolioService
	{
		//TODO: Figure out guest account
		public const int GuestUserId = -1;

		private readonly IServiceBundleController _serviceBundleController;
		private readonly IServiceController _serviceController;
		private readonly ILifecycleStatusController _lifecycleStatusController;
		private readonly IServiceSwotController _serviceSwotController;
		private readonly ISwotActivityController _swotActivityController;
		private readonly IServiceDocumentController _serviceDocumentController;
		private readonly IServiceGoalController _serviceGoalController;
		private readonly IServiceContractController _serviceContractController;
		private readonly IServiceWorkUnitController _serviceWorkUnitController;
		private readonly IServiceMeasureController _serviceMeasureController;
		private readonly IServiceOptionController _serviceOptionController;


		//TODO: Add check for valid user being set
		private int _userId;

		public int UserId
		{
			get { return _userId; }
			set
			{
				_userId = value;
				SetControllerUsers(_userId);
			}
		}

		//Lol I'll make a factory for constructing this
		public PortfolioService(int userId, IServiceBundleController serviceBundleController,
			IServiceController serviceController, ILifecycleStatusController lifecycleStatusController,
			IServiceSwotController serviceSwotController, ISwotActivityController swotActivityController,
			IServiceDocumentController serviceDocumentController, IServiceGoalController serviceGoalController,
			IServiceContractController serviceContractController, IServiceWorkUnitController serviceWorkUnitController,
			IServiceMeasureController serviceMeasureController, IServiceOptionController serviceOptionController)
		{
			_serviceBundleController = serviceBundleController;
			_serviceController = serviceController;
			_lifecycleStatusController = lifecycleStatusController;
			_serviceSwotController = serviceSwotController;
			_swotActivityController = swotActivityController;
			_serviceDocumentController = serviceDocumentController;
			_serviceGoalController = serviceGoalController;
			_serviceContractController = serviceContractController;
			_serviceWorkUnitController = serviceWorkUnitController;
			_serviceMeasureController = serviceMeasureController;
			_serviceOptionController = serviceOptionController;

			UserId = userId;
		}

		private void SetControllerUsers(int userId)
		{
			_serviceBundleController.UserId = userId;
			_serviceController.UserId = userId;
			_lifecycleStatusController.UserId = userId;
			_serviceSwotController.UserId = userId;
			_swotActivityController.UserId = userId;
			_serviceDocumentController.UserId = userId;
			_serviceGoalController.UserId = userId;
			_serviceContractController.UserId = userId;
			_serviceWorkUnitController.UserId = userId;
			_serviceMeasureController.UserId = userId;
			_serviceOptionController.UserId = userId;
		}

		public IEnumerable<IServiceBundleDto> GetServiceBundles()
		{
			return _serviceBundleController.GetServiceBundles();
		}

		public IServiceBundleDto GetServiceBundle(int serviceBundleId)
		{
			return _serviceBundleController.GetServiceBundle(serviceBundleId);
		}

		public IEnumerable<Tuple<int, string>> GetServiceBundleNames()
		{
			return _serviceBundleController.GetServiceBundleNames();
		}

		public IServiceBundleDto SaveServiceBundle(IServiceBundleDto serviceBundle)
		{
			return _serviceBundleController.SaveServiceBundle(serviceBundle);
		}

		public bool DeleteServiceBundle(int serviceBundleId)
		{
			return _serviceBundleController.DeleteServiceBundle(serviceBundleId);
		}

		public IEnumerable<Tuple<int, string>> GetLifecycleStatusNames()
		{
			return _lifecycleStatusController.GetLifecycleStatusNames();
		}

		public ILifecycleStatusDto GetLifecycleStatus(int lifecycleStatusId)
		{
			return _lifecycleStatusController.GetLifecycleStatus(lifecycleStatusId);
		}

		public ILifecycleStatusDto SaveLifecycleStatus(ILifecycleStatusDto lifecycleStatus)
		{
			return _lifecycleStatusController.SaveLifecycleStatus(lifecycleStatus);
		}

		public bool DeleteLifecycleStatus(int lifecycleStatusId)
		{
			return _lifecycleStatusController.DeleteLifecycleStatus(lifecycleStatusId);
		}

		public int CountLifecycleStatuses()
		{
			return _lifecycleStatusController.CountLifecycleStatuses();
		}


		public IEnumerable<IServiceDto> GetServicesForServiceBundle(int serviceBundleId)
		{
			return _serviceController.GetServicesForServiceBundle(serviceBundleId);
		}

		public IServiceDto ModifyService(IServiceDto service, EntityModification modification)
		{
			return _serviceController.ModifyService(service, modification);
		}

		public IServiceDto GetService(int serviceId)
		{
			return _serviceController.GetService(serviceId);
		}

		public IEnumerable<Tuple<int, string>> GetServiceNamesForServiceBundle(int serviceBundleId)
		{
			return _serviceController.GetServiceNamesForServiceBundle(serviceBundleId);
		}

		public IEnumerable<Tuple<int, string>> GetServiceNames()
		{
			return _serviceController.GetServiceNames();
		}

		public IEnumerable<IServiceDto> GetServices()
		{
			return _serviceController.GetServices();
		}

		public IEnumerable<IServiceDocumentDto> GetServiceDocuments(int serviceId)
		{
			return _serviceController.GetServiceDocuments(serviceId);
		}

		public IServiceDocumentDto ModifyServiceDocument(IServiceDocumentDto document, EntityModification modification)
		{
			return _serviceDocumentController.ModifyServiceDocument(document, modification);
		}

		public IServiceDocumentDto GetServiceDocument(Guid documentGuid)
		{
			return _serviceDocumentController.GetServiceDocument(documentGuid);
		}

		public IServiceBundleDto UpdateServiceBundle(IServiceBundleDto serviceBundle)
		{
			return _serviceBundleController.UpdateServiceBundle(serviceBundle);
		}

		public IServiceSwotDto GetServiceSwot(int serviceSwotId)
		{
			return _serviceSwotController.GetServiceSwot(serviceSwotId);
		}

		public IServiceSwotDto ModifyServiceSwot(IServiceSwotDto serviceSwot, EntityModification modification)
		{
			return _serviceSwotController.ModifyServiceSwot(serviceSwot, modification);
		}

		public ISwotActivityDto GetSwotActivity(int swotActivityId)
		{
			return _swotActivityController.GetSwotActivity(swotActivityId);
		}

		public ISwotActivityDto ModifySwotActivity(ISwotActivityDto swotActivity, EntityModification modification)
		{
			return _swotActivityController.ModifySwotActivity(swotActivity, modification);
		}

		public IServiceGoalDto GetServiceGoal(int serviceGoalId)
		{
			return _serviceGoalController.GetServiceGoal(serviceGoalId);
		}

		public IServiceGoalDto ModifyServiceGoal(IServiceGoalDto serviceGoal, EntityModification modification)
		{
			return _serviceGoalController.ModifyServiceGoal(serviceGoal, modification);
		}

		public IServiceContractDto GetServiceContract(int serviceContractId)
		{
			return _serviceContractController.GetServiceContract(serviceContractId);
		}

		public IServiceContractDto ModifyServiceContract(IServiceContractDto serviceContract, EntityModification modification)
		{
			return _serviceContractController.ModifyServiceContract(serviceContract, modification);
		}

		public IServiceWorkUnitDto GetServiceWorkUnit(int serviceWorkUnitId)
		{
			return _serviceWorkUnitController.GetServiceWorkUnit(serviceWorkUnitId);
		}

		public IServiceWorkUnitDto ModifyServiceWorkUnit(IServiceWorkUnitDto serviceWorkUnit, EntityModification modification)
		{
			return _serviceWorkUnitController.ModifyServiceWorkUnit(serviceWorkUnit, modification);
		}

		public IServiceMeasureDto GetServiceMeasure(int serviceMeasureId)
		{
			return _serviceMeasureController.GetServiceMeasure(serviceMeasureId);
		}

		public IServiceMeasureDto ModifyServiceMeasure(IServiceMeasureDto serviceMeasure, EntityModification modification)
		{
			return _serviceMeasureController.ModifyServiceMeasure(serviceMeasure, modification);
		}

		public IServiceOptionDto GetServiceOption(int serviceOptionId)
		{
			return _serviceOptionController.GetServiceOption(serviceOptionId);
		}

		public IServiceOptionDto ModifyServiceOption(IServiceOptionDto serviceOption, EntityModification modification)
		{
			return _serviceOptionController.ModifyServiceOption(serviceOption, modification);
		}
	}
}
