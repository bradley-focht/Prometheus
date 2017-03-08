using System;
using System.Collections.Generic;
using Common.Dto;
using Common.Enums;
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
		private readonly IServiceProcessController _serviceProcessController;
		private readonly IServiceOptionCategoryController _optionCategoryController;
		private readonly ITextInputController _textInputController;
		private readonly ISelectionInputController _selectionInputController;
		private readonly IScriptedSelectionInputController _scriptedSelectionController;
		private readonly IServiceRequestPackageController _serviceRequestPackageController;

		//Lol I'll make a factory for constructing this
		public PortfolioService(IServiceBundleController serviceBundleController,
			IServiceController serviceController, ILifecycleStatusController lifecycleStatusController,
			IServiceSwotController serviceSwotController, ISwotActivityController swotActivityController,
			IServiceDocumentController serviceDocumentController, IServiceGoalController serviceGoalController,
			IServiceContractController serviceContractController, IServiceWorkUnitController serviceWorkUnitController,
			IServiceMeasureController serviceMeasureController, IServiceOptionController serviceOptionController,
			IServiceOptionCategoryController optionCategoryController, IServiceProcessController serviceProcessController,
			ITextInputController textInputController, ISelectionInputController selectionInputController,
			IScriptedSelectionInputController scriptedSelectionController, IServiceRequestPackageController serviceRequestPackageController)
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

		public IServiceDocumentDto ModifyServiceDocument(int performingUserId, IServiceDocumentDto document, EntityModification modification)
		{
			return _serviceDocumentController.ModifyServiceDocument(performingUserId, document, modification);
		}

		public IServiceDocumentDto GetServiceDocument(int performingUserId, int documentGuid)
		{
			return _serviceDocumentController.GetServiceDocument(performingUserId, documentGuid);
		}

		public IServiceBundleDto UpdateServiceBundle(IServiceBundleDto serviceBundle)
		{
			return _serviceBundleController.UpdateServiceBundle(serviceBundle);
		}

		public IServiceSwotDto GetServiceSwot(int performingUserId, int serviceSwotId)
		{
			return _serviceSwotController.GetServiceSwot(performingUserId, serviceSwotId);
		}

		public IServiceSwotDto ModifyServiceSwot(int performingUserId, IServiceSwotDto serviceSwot, EntityModification modification)
		{
			return _serviceSwotController.ModifyServiceSwot(performingUserId, serviceSwot, modification);
		}

		public ISwotActivityDto GetSwotActivity(int performingUserId, int swotActivityId)
		{
			return _swotActivityController.GetSwotActivity(performingUserId, swotActivityId);
		}

		public ISwotActivityDto ModifySwotActivity(int performingUserId, ISwotActivityDto swotActivity, EntityModification modification)
		{
			return _swotActivityController.ModifySwotActivity(performingUserId, swotActivity, modification);
		}

		public IServiceGoalDto GetServiceGoal(int performingUserId, int serviceGoalId)
		{
			return _serviceGoalController.GetServiceGoal(performingUserId, serviceGoalId);
		}

		public IServiceGoalDto ModifyServiceGoal(int performingUserId, IServiceGoalDto serviceGoal, EntityModification modification)
		{
			return _serviceGoalController.ModifyServiceGoal(performingUserId, serviceGoal, modification);
		}

		public IServiceContractDto GetServiceContract(int performingUserId, int serviceContractId)
		{
			return _serviceContractController.GetServiceContract(performingUserId, serviceContractId);
		}

		public IServiceContractDto ModifyServiceContract(int performingUserId, IServiceContractDto serviceContract, EntityModification modification)
		{
			return _serviceContractController.ModifyServiceContract(performingUserId, serviceContract, modification);
		}

		public IServiceWorkUnitDto GetServiceWorkUnit(int performingUserId, int serviceWorkUnitId)
		{
			return _serviceWorkUnitController.GetServiceWorkUnit(performingUserId, serviceWorkUnitId);
		}

		public IServiceWorkUnitDto ModifyServiceWorkUnit(int performingUserId, IServiceWorkUnitDto serviceWorkUnit, EntityModification modification)
		{
			return _serviceWorkUnitController.ModifyServiceWorkUnit(performingUserId, serviceWorkUnit, modification);
		}

		public IServiceMeasureDto GetServiceMeasure(int performingUserId, int serviceMeasureId)
		{
			return _serviceMeasureController.GetServiceMeasure(performingUserId, serviceMeasureId);
		}

		public IServiceMeasureDto ModifyServiceMeasure(int performingUserId, IServiceMeasureDto serviceMeasure, EntityModification modification)
		{
			return _serviceMeasureController.ModifyServiceMeasure(performingUserId, serviceMeasure, modification);
		}

		public IServiceOptionDto GetServiceOption(int performingUserId, int serviceOptionId)
		{
			return _serviceOptionController.GetServiceOption(performingUserId, serviceOptionId);
		}

		public IServiceOptionDto ModifyServiceOption(int performingUserId, IServiceOptionDto serviceOption, EntityModification modification)
		{
			return _serviceOptionController.ModifyServiceOption(performingUserId, serviceOption, modification);
		}

		public IInputGroupDto GetInputsForServiceOptions(int performingUserId, IEnumerable<IServiceOptionDto> serviceOptions)
		{
			return _serviceOptionController.GetInputsForServiceOptions(performingUserId, serviceOptions);
		}

		public IServiceOptionDto AddInputsToServiceOption(int performingUserId, int serviceOptionId, IInputGroupDto inputsToAdd)
		{
			return _serviceOptionController.AddInputsToServiceOption(performingUserId, serviceOptionId, inputsToAdd);
		}

		public IServiceOptionDto RemoveInputsFromServiceOption(int performingUserId, int serviceOptionId, IInputGroupDto inputsToRemove)
		{
			return _serviceOptionController.RemoveInputsFromServiceOption(performingUserId, serviceOptionId, inputsToRemove);
		}

		public IServiceProcessDto GetServiceProcess(int performingUserId, int serviceProcessId)
		{
			return _serviceProcessController.GetServiceProcess(performingUserId, serviceProcessId);
		}

		public IServiceProcessDto ModifyServiceProcess(int performingUserId, IServiceProcessDto serviceProcess, EntityModification modification)
		{
			return _serviceProcessController.ModifyServiceProcess(performingUserId, serviceProcess, modification);
		}

		public IServiceOptionCategoryDto GetServiceOptionCategory(int performingUserId, int optionCategoryId)
		{
			return _optionCategoryController.GetServiceOptionCategory(performingUserId, optionCategoryId);
		}

		public IServiceOptionCategoryDto ModifyServiceOptionCategory(int performingUserId, IServiceOptionCategoryDto optionCategory, EntityModification modification)
		{
			return _optionCategoryController.ModifyServiceOptionCategory(performingUserId, optionCategory, modification);
		}

		public ITextInputDto GetTextInput(int performingUserId, int textInputId)
		{
			return _textInputController.GetTextInput(performingUserId, textInputId);
		}

		public IEnumerable<ITextInputDto> GetTextInputs(int performingUserId)
		{
			return _textInputController.GetTextInputs(performingUserId);
		}

		public ITextInputDto ModifyTextInput(int performingUserId, ITextInputDto textInput, EntityModification modification)
		{
			return _textInputController.ModifyTextInput(performingUserId, textInput, modification);
		}

		public ISelectionInputDto GetSelectionInput(int performingUserId, int selectionInputId)
		{
			return _selectionInputController.GetSelectionInput(performingUserId, selectionInputId);
		}

		public IEnumerable<ISelectionInputDto> GetSelectionInputs(int performingUserId)
		{
			return _selectionInputController.GetSelectionInputs(performingUserId);
		}

		public ISelectionInputDto ModifySelectionInput(int performingUserId, ISelectionInputDto selectionInput, EntityModification modification)
		{
			return _selectionInputController.ModifySelectionInput(performingUserId, selectionInput, modification);
		}

		public IScriptedSelectionInputDto GetScriptedSelectionInput(int performingUserId, int scriptedSelection)
		{
			return _scriptedSelectionController.GetScriptedSelectionInput(performingUserId, scriptedSelection);
		}

		public IEnumerable<IScriptedSelectionInputDto> GetScriptedSelectionInputs(int performingUserId)
		{
			return _scriptedSelectionController.GetScriptedSelectionInputs(performingUserId);
		}

		public IScriptedSelectionInputDto ModifyScriptedSelectionInput(int performingUserId, IScriptedSelectionInputDto scriptedSelection,
			EntityModification modification)
		{
			return _scriptedSelectionController.ModifyScriptedSelectionInput(performingUserId, scriptedSelection, modification);
		}

		public IServiceRequestPackageDto GetServiceRequestPackage(int performingUserId, int servicePackageId)
		{
			return _serviceRequestPackageController.GetServiceRequestPackage(performingUserId, servicePackageId);
		}

		public IServiceRequestPackageDto ModifyServiceRequestPackage(int performingUserId, IServiceRequestPackageDto servicePackage,
			EntityModification modification)
		{
			return _serviceRequestPackageController.ModifyServiceRequestPackage(performingUserId, servicePackage, modification);
		}

		public IEnumerable<IServiceRequestPackageDto> AllServiceRequestPackages
		{
			get { return _serviceRequestPackageController.AllServiceRequestPackages; }
		}

		public IEnumerable<IServiceRequestPackageDto> GetServiceRequestPackagesForServiceOption(int performingUserId, int serviceOptionId, ServiceRequestAction action)
		{
			return _serviceRequestPackageController.GetServiceRequestPackagesForServiceOption(performingUserId, serviceOptionId, action);
		}
	}
}
