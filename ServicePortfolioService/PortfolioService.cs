using Common.Dto;
using Common.Enums.Entities;
using ServicePortfolioService.Controllers;
using System;
using System.Collections.Generic;

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
		private readonly IServiceProcessController _serviceProcessController;
		private readonly IServiceOptionCategoryController _optionCategoryController;
		private readonly ITextInputController _textInputController;
		private readonly ISelectionInputController _selectionInputController;
		private readonly IScriptedSelectionController _scriptedSelectionController;
		private readonly IServiceRequestPackageController _serviceRequestPackageController;
		private readonly IServiceRequestController _serviceRequestController;



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
			IServiceMeasureController serviceMeasureController, IServiceOptionController serviceOptionController,
			IServiceOptionCategoryController optionCategoryController, IServiceProcessController serviceProcessController,
			ITextInputController textInputController, ISelectionInputController selectionInputController,
			IScriptedSelectionController scriptedSelectionController, IServiceRequestPackageController serviceRequestPackageController,
			IServiceRequestController serviceRequestController)
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
			_optionCategoryController = optionCategoryController;
			_serviceProcessController = serviceProcessController;
			_textInputController = textInputController;
			_selectionInputController = selectionInputController;
			_scriptedSelectionController = scriptedSelectionController;
			_serviceRequestPackageController = serviceRequestPackageController;
			_serviceRequestController = serviceRequestController;

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
			_serviceRequestPackageController.UserId = userId;
			_serviceRequestController.UserId = userId;
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

		public IServiceDocumentDto GetServiceDocument(int documentGuid)
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

		public IInputGroupDto GetInputsForServiceOptions(IEnumerable<IServiceOptionDto> serviceOptions)
		{
			return _serviceOptionController.GetInputsForServiceOptions(serviceOptions);
		}

		public IServiceOptionDto AddInputsToServiceOption(int serviceOptionId, IInputGroupDto inputsToAdd)
		{
			return _serviceOptionController.AddInputsToServiceOption(serviceOptionId, inputsToAdd);
		}

		public IServiceOptionDto RemoveInputsFromServiceOption(int serviceOptionId, IInputGroupDto inputsToRemove)
		{
			return _serviceOptionController.RemoveInputsFromServiceOption(serviceOptionId, inputsToRemove);
		}

		public IServiceProcessDto GetServiceProcess(int serviceProcessId)
		{
			return _serviceProcessController.GetServiceProcess(serviceProcessId);
		}

		public IServiceProcessDto ModifyServiceProcess(IServiceProcessDto serviceProcess, EntityModification modification)
		{
			return _serviceProcessController.ModifyServiceProcess(serviceProcess, modification);
		}

		public IServiceOptionCategoryDto GetServiceOptionCategory(int optionCategoryId)
		{
			return _optionCategoryController.GetServiceOptionCategory(optionCategoryId);
		}

		public IServiceOptionCategoryDto ModifyServiceOptionCategory(IServiceOptionCategoryDto optionCategory, EntityModification modification)
		{
			return _optionCategoryController.ModifyServiceOptionCategory(optionCategory, modification);
		}

		public ITextInputDto GetTextInput(int textInputId)
		{
			return _textInputController.GetTextInput(textInputId);
		}

		public IEnumerable<ITextInputDto> GetTextInputs()
		{
			return _textInputController.GetTextInputs();
		}

		public ITextInputDto ModifyTextInput(ITextInputDto textInput, EntityModification modification)
		{
			return _textInputController.ModifyTextInput(textInput, modification);
		}

		public ISelectionInputDto GetSelectionInput(int selectionInputId)
		{
			return _selectionInputController.GetSelectionInput(selectionInputId);
		}

		public IEnumerable<ISelectionInputDto> GetSelectionInputs()
		{
			return _selectionInputController.GetSelectionInputs();
		}

		public ISelectionInputDto ModifySelectionInput(ISelectionInputDto selectionInput, EntityModification modification)
		{
			return _selectionInputController.ModifySelectionInput(selectionInput, modification);
		}

		public IScriptedSelectionInputDto GetScriptedSelectionInput(int scriptedSelection)
		{
			return _scriptedSelectionController.GetScriptedSelectionInput(scriptedSelection);
		}

		public IEnumerable<IScriptedSelectionInputDto> GetScriptedSelectionInputs()
		{
			return _scriptedSelectionController.GetScriptedSelectionInputs();
		}

		public IScriptedSelectionInputDto ModifyScriptedSelectionInput(IScriptedSelectionInputDto scriptedSelection,
			EntityModification modification)
		{
			return _scriptedSelectionController.ModifyScriptedSelectionInput(scriptedSelection, modification);
		}

		public IServiceRequestPackageDto GetServiceRequestPackage(int servicePackageId)
		{
			return _serviceRequestPackageController.GetServiceRequestPackage(servicePackageId);
		}

		public IServiceRequestPackageDto ModifyServiceRequestPackage(IServiceRequestPackageDto servicePackage,
			EntityModification modification)
		{
			return _serviceRequestPackageController.ModifyServiceRequestPackage(servicePackage, modification);
		}

		public IEnumerable<IServiceRequestPackageDto> AllServiceRequestPackages
		{
			get { return _serviceRequestPackageController.AllServiceRequestPackages; }
		}

		public IEnumerable<IServiceRequestPackageDto> GetServiceRequestPackagesForServiceOption(int serviceOptionId)
		{
			return _serviceRequestPackageController.GetServiceRequestPackagesForServiceOption(serviceOptionId);
		}

		public IServiceRequestDto GetServiceRequest(int serviceRequestId)
		{
			return _serviceRequestController.GetServiceRequest(serviceRequestId);
		}

		public IServiceRequestDto ModifyServiceRequest(IServiceRequestDto serviceRequest, EntityModification modification)
		{
			return _serviceRequestController.ModifyServiceRequest(serviceRequest, modification);
		}

		public IEnumerable<IServiceRequestDto> GetServiceRequestsForRequestorId(int requestorUserId)
		{
			return _serviceRequestController.GetServiceRequestsForRequestorId(requestorUserId);
		}
	}
}
