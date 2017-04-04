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

		/// <summary>
		/// Returns all service bundles
		/// </summary>
		/// <returns></returns>
		public IEnumerable<IServiceBundleDto> GetServiceBundles()
		{
			return _serviceBundleController.GetServiceBundles();
		}

		/// <summary>
		/// Finds service bundle with identifier provided and returns its DTO
		/// </summary>
		/// <param name="serviceBundleId"></param>
		/// <returns></returns>
		public IServiceBundleDto GetServiceBundle(int serviceBundleId)
		{
			return _serviceBundleController.GetServiceBundle(serviceBundleId);
		}

		/// <summary>
		/// KVP of all service bundle IDs and names in ascending order by name
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Tuple<int, string>> GetServiceBundleNames()
		{
			return _serviceBundleController.GetServiceBundleNames();
		}

		/// <summary>
		/// Modifies the service bundle in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceBundle"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Bundle</returns>
		public IServiceBundleDto ModifyServiceBundle(int performingUserId, IServiceBundleDto serviceBundle, EntityModification modification)
		{
			return _serviceBundleController.ModifyServiceBundle(performingUserId, serviceBundle, modification);
		}

		/// <summary>
		/// KVP of all lifecycle IDs and names in ascending order by name
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Tuple<int, string>> GetLifecycleStatusNames()
		{
			return _lifecycleStatusController.GetLifecycleStatusNames();
		}

		/// <summary>
		/// Finds lifecycle status with identifier provided and returns its DTO
		/// </summary>
		/// <param name="lifecycleStatusId"></param>
		/// <returns></returns>
		public ILifecycleStatusDto GetLifecycleStatus(int lifecycleStatusId)
		{
			return _lifecycleStatusController.GetLifecycleStatus(lifecycleStatusId);
		}

		/// <summary>
		/// Modifies the status in the database
		/// </summary>
		/// <param name="performingUserId">ID of user performing the modification</param>
		/// <param name="lifecycleStatus"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Lifecycle Status</returns>
		public ILifecycleStatusDto ModifyLifecycleStatus(int performingUserId, ILifecycleStatusDto lifecycleStatus, EntityModification modification)
		{
			return _lifecycleStatusController.ModifyLifecycleStatus(performingUserId, lifecycleStatus, modification);
		}

		/// <summary>
		/// returns a count of the number of Lifecycle statuses found
		/// </summary>
		/// <returns></returns>
		public int CountLifecycleStatuses()
		{
			return _lifecycleStatusController.CountLifecycleStatuses();
		}

		/// <summary>
		/// Finds the service bundle from identifier then returns all of its services
		/// </summary>
		/// <param name="serviceBundleId"></param>
		/// <returns></returns>
		public IEnumerable<IServiceDto> GetServicesForServiceBundle(int serviceBundleId)
		{
			return _serviceController.GetServicesForServiceBundle(serviceBundleId);
		}

		/// <summary>
		/// Modifies the service in the database
		/// </summary>
		/// <param name="performingUserId">ID for user doing modification</param>
		/// <param name="service"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified entity DTO</returns>
		public IServiceDto ModifyService(int performingUserId, IServiceDto service, EntityModification modification)
		{
			return _serviceController.ModifyService(performingUserId, service, modification);
		}

		/// <summary>
		/// Finds service with identifier provided and returns its DTO
		/// </summary>
		/// <param name="serviceId"></param>
		/// <returns></returns>
		public IServiceDto GetService(int serviceId)
		{
			return _serviceController.GetService(serviceId);
		}

		/// <summary>
		/// Finds the service bundle from identifier then uses its services
		/// KVP of all services IDs and names in ascending order by name
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Tuple<int, string>> GetServiceNamesForServiceBundle(int serviceBundleId)
		{
			return _serviceController.GetServiceNamesForServiceBundle(serviceBundleId);
		}

		/// <summary>
		/// Gets a list of services and names for making lists
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Tuple<int, string>> GetServiceNames()
		{
			return _serviceController.GetServiceNames();
		}

		/// <summary>
		/// Get a full list of services 
		/// </summary>
		/// <returns></returns>
		public IEnumerable<IServiceDto> GetServices()
		{
			return _serviceController.GetServices();
		}

		/// <summary>
		/// Applies a service bundle ID to multiple services.
		/// 
		/// NOTE: null can be applied as service bundle ID to remove the services from their bundle
		/// </summary>
		/// <param name="performingUserId">ID for user performing adjustment</param>
		/// <param name="serviceBundleId">ID to add to the services. Can be null</param>
		/// <param name="services">Services to set the Service Bundle on</param>
		/// <returns></returns>
		public IEnumerable<IServiceDto> SetServiceBundleForServices(int performingUserId, int? serviceBundleId, IEnumerable<IServiceDto> services)
		{
			return _serviceController.SetServiceBundleForServices(performingUserId, serviceBundleId, services);
		}

		/// <summary>
		/// Get all documents associated with a service
		/// </summary>
		/// <param name="serviceId"></param>
		/// <returns></returns>
		public IEnumerable<IServiceDocumentDto> GetServiceDocuments(int serviceId)
		{
			return _serviceController.GetServiceDocuments(serviceId);
		}

		/// <summary>
		/// Modifies the service document in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceDocument"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Document</returns>
		public IServiceDocumentDto ModifyServiceDocument(int performingUserId, IServiceDocumentDto serviceDocument, EntityModification modification)
		{
			return _serviceDocumentController.ModifyServiceDocument(performingUserId, serviceDocument, modification);
		}

		/// <summary>
		/// Finds service document with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceDocumentId"></param>
		/// <returns></returns>
		public IServiceDocumentDto GetServiceDocument(int performingUserId, int serviceDocumentId)
		{
			return _serviceDocumentController.GetServiceDocument(performingUserId, serviceDocumentId);
		}

		/// <summary>
		/// Finds service SWOT with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceSwotId"></param>
		/// <returns></returns>
		public IServiceSwotDto GetServiceSwot(int performingUserId, int serviceSwotId)
		{
			return _serviceSwotController.GetServiceSwot(performingUserId, serviceSwotId);
		}

		/// <summary>
		/// Modifies the service SWOT in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceSwot"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified SWOT</returns>
		public IServiceSwotDto ModifyServiceSwot(int performingUserId, IServiceSwotDto serviceSwot, EntityModification modification)
		{
			return _serviceSwotController.ModifyServiceSwot(performingUserId, serviceSwot, modification);
		}

		/// <summary>
		/// Finds SWOT activity with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="swotActivityId"></param>
		/// <returns></returns>
		public ISwotActivityDto GetSwotActivity(int performingUserId, int swotActivityId)
		{
			return _swotActivityController.GetSwotActivity(performingUserId, swotActivityId);
		}

		/// <summary>
		/// Modifies the SWOT activity in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="swotActivity"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified SWOT activity</returns>
		public ISwotActivityDto ModifySwotActivity(int performingUserId, ISwotActivityDto swotActivity, EntityModification modification)
		{
			return _swotActivityController.ModifySwotActivity(performingUserId, swotActivity, modification);
		}

		/// <summary>
		/// Finds service goal with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceGoalId"></param>
		/// <returns></returns>
		public IServiceGoalDto GetServiceGoal(int performingUserId, int serviceGoalId)
		{
			return _serviceGoalController.GetServiceGoal(performingUserId, serviceGoalId);
		}

		/// <summary>
		/// Modifies the service goal in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceGoal"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Goal</returns>
		public IServiceGoalDto ModifyServiceGoal(int performingUserId, IServiceGoalDto serviceGoal, EntityModification modification)
		{
			return _serviceGoalController.ModifyServiceGoal(performingUserId, serviceGoal, modification);
		}

		/// <summary>
		/// Finds service Contract with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceContractId"></param>
		/// <returns></returns>
		public IServiceContractDto GetServiceContract(int performingUserId, int serviceContractId)
		{
			return _serviceContractController.GetServiceContract(performingUserId, serviceContractId);
		}

		/// <summary>
		/// Modifies the service Contract in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceContract"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Contract</returns>
		public IServiceContractDto ModifyServiceContract(int performingUserId, IServiceContractDto serviceContract, EntityModification modification)
		{
			return _serviceContractController.ModifyServiceContract(performingUserId, serviceContract, modification);
		}

		/// <summary>
		/// Finds service WorkUnit with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceWorkUnitId"></param>
		/// <returns></returns>
		public IServiceWorkUnitDto GetServiceWorkUnit(int performingUserId, int serviceWorkUnitId)
		{
			return _serviceWorkUnitController.GetServiceWorkUnit(performingUserId, serviceWorkUnitId);
		}

		/// <summary>
		/// Modifies the service WorkUnit in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceWorkUnit"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service WorkUnit</returns>
		public IServiceWorkUnitDto ModifyServiceWorkUnit(int performingUserId, IServiceWorkUnitDto serviceWorkUnit, EntityModification modification)
		{
			return _serviceWorkUnitController.ModifyServiceWorkUnit(performingUserId, serviceWorkUnit, modification);
		}

		/// <summary>
		/// Finds service Measure with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceMeasureId"></param>
		/// <returns></returns>
		public IServiceMeasureDto GetServiceMeasure(int performingUserId, int serviceMeasureId)
		{
			return _serviceMeasureController.GetServiceMeasure(performingUserId, serviceMeasureId);
		}

		/// <summary>
		/// Modifies the service Measure in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceMeasure"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Measure</returns>
		public IServiceMeasureDto ModifyServiceMeasure(int performingUserId, IServiceMeasureDto serviceMeasure, EntityModification modification)
		{
			return _serviceMeasureController.ModifyServiceMeasure(performingUserId, serviceMeasure, modification);
		}

		/// <summary>
		/// Finds option with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceOptionId"></param>
		/// <returns></returns>
		public IServiceOptionDto GetServiceOption(int performingUserId, int serviceOptionId)
		{
			return _serviceOptionController.GetServiceOption(performingUserId, serviceOptionId);
		}

		/// <summary>
		/// Modifies the option in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceOption"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Option</returns>
		public IServiceOptionDto ModifyServiceOption(int performingUserId, IServiceOptionDto serviceOption, EntityModification modification)
		{
			return _serviceOptionController.ModifyServiceOption(performingUserId, serviceOption, modification);
		}

		/// <summary>
		/// Gets the required inputs for all supplied service options
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceOptions">Service Options to get the inputs for</param>
		/// <returns></returns>
		public IInputGroupDto GetInputsForServiceOptions(int performingUserId, IEnumerable<IServiceOptionDto> serviceOptions)
		{
			return _serviceOptionController.GetInputsForServiceOptions(performingUserId, serviceOptions);
		}

		/// <summary>
		/// Adds the supplied inputs to the ServiceOption provided
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceOptionId">ID of Service Option to add inputs to</param>
		/// <param name="inputsToAdd">Inputs to add to the Service Option</param>
		/// <returns></returns>
		public IServiceOptionDto AddInputsToServiceOption(int performingUserId, int serviceOptionId, IInputGroupDto inputsToAdd)
		{
			return _serviceOptionController.AddInputsToServiceOption(performingUserId, serviceOptionId, inputsToAdd);
		}

		/// <summary>
		/// Removes the supplied inputs from the ServiceOption provided
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceOptionId">ID of Service Option to remove inputs from</param>
		/// <param name="inputsToRemove">Inputs to remove from the Service Option</param>
		/// <returns></returns>
		public IServiceOptionDto RemoveInputsFromServiceOption(int performingUserId, int serviceOptionId, IInputGroupDto inputsToRemove)
		{
			return _serviceOptionController.RemoveInputsFromServiceOption(performingUserId, serviceOptionId, inputsToRemove);
		}

		/// <summary>
		/// Finds service process with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceProcessId"></param>
		/// <returns></returns>
		public IServiceProcessDto GetServiceProcess(int performingUserId, int serviceProcessId)
		{
			return _serviceProcessController.GetServiceProcess(performingUserId, serviceProcessId);
		}

		/// <summary>
		/// Modifies the service process in the database
		/// </summary>
		/// <param name="performingUserId">ID of user performing modification</param>
		/// <param name="serviceProcess"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Process</returns>
		public IServiceProcessDto ModifyServiceProcess(int performingUserId, IServiceProcessDto serviceProcess, EntityModification modification)
		{
			return _serviceProcessController.ModifyServiceProcess(performingUserId, serviceProcess, modification);
		}

		/// <summary>
		/// Finds option category with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="optionCategoryId"></param>
		/// <returns></returns>
		public IServiceOptionCategoryDto GetServiceOptionCategory(int performingUserId, int optionCategoryId)
		{
			return _optionCategoryController.GetServiceOptionCategory(performingUserId, optionCategoryId);
		}

		/// <summary>
		/// Modifies the option category in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="optionCategory"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Option Category</returns>
		public IServiceOptionCategoryDto ModifyServiceOptionCategory(int performingUserId, IServiceOptionCategoryDto optionCategory, EntityModification modification)
		{
			return _optionCategoryController.ModifyServiceOptionCategory(performingUserId, optionCategory, modification);
		}

		/// <summary>
		/// Finds text input with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="textInputId"></param>
		/// <returns></returns>
		public ITextInputDto GetTextInput(int performingUserId, int textInputId)
		{
			return _textInputController.GetTextInput(performingUserId, textInputId);
		}

		/// <summary>
		/// Retrieve all
		/// </summary>
		/// <returns></returns>
		public IEnumerable<ITextInputDto> GetTextInputs(int performingUserId)
		{
			return _textInputController.GetTextInputs(performingUserId);
		}

		/// <summary>
		/// Modifies the text input in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="textInput"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Text Input</returns>
		public ITextInputDto ModifyTextInput(int performingUserId, ITextInputDto textInput, EntityModification modification)
		{
			return _textInputController.ModifyTextInput(performingUserId, textInput, modification);
		}

		/// <summary>
		/// Finds text input with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="selectionInputId"></param>
		/// <returns></returns>
		public ISelectionInputDto GetSelectionInput(int performingUserId, int selectionInputId)
		{
			return _selectionInputController.GetSelectionInput(performingUserId, selectionInputId);
		}

		/// <summary>
		/// Retrieve all. 
		/// </summary>
		/// <returns></returns>
		public IEnumerable<ISelectionInputDto> GetSelectionInputs(int performingUserId)
		{
			return _selectionInputController.GetSelectionInputs(performingUserId);
		}

		/// <summary>
		/// Modifies the text input in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="selectionInput"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Measure</returns>
		public ISelectionInputDto ModifySelectionInput(int performingUserId, ISelectionInputDto selectionInput, EntityModification modification)
		{
			return _selectionInputController.ModifySelectionInput(performingUserId, selectionInput, modification);
		}

		/// <summary>
		/// Finds Scripted Selection Input with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="scriptedSelection"></param>
		/// <returns></returns>
		public IScriptedSelectionInputDto GetScriptedSelectionInput(int performingUserId, int scriptedSelection)
		{
			return _scriptedSelectionController.GetScriptedSelectionInput(performingUserId, scriptedSelection);
		}

		/// <summary>
		/// Returns a list of all of the Scripted Selection Inputs found
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <returns></returns>
		public IEnumerable<IScriptedSelectionInputDto> GetScriptedSelectionInputs(int performingUserId)
		{
			return _scriptedSelectionController.GetScriptedSelectionInputs(performingUserId);
		}

		/// <summary>
		/// Modifies the Scripted Selection Input in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="scriptedSelection"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Scripted Selection Input</returns>
		public IScriptedSelectionInputDto ModifyScriptedSelectionInput(int performingUserId, IScriptedSelectionInputDto scriptedSelection,
			EntityModification modification)
		{
			return _scriptedSelectionController.ModifyScriptedSelectionInput(performingUserId, scriptedSelection, modification);
		}

		/// <summary>
		/// Finds service package with identifier provided and returns its DTO
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="servicePackageId"></param>
		/// <returns></returns>
		public IServiceRequestPackageDto GetServiceRequestPackage(int performingUserId, int servicePackageId)
		{
			return _serviceRequestPackageController.GetServiceRequestPackage(performingUserId, servicePackageId);
		}

		/// <summary>
		/// Modifies the service Package in the database
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="servicePackage"></param>
		/// <param name="modification">Type of modification to make</param>
		/// <returns>Modified Service Request Package</returns>
		public IServiceRequestPackageDto ModifyServiceRequestPackage(int performingUserId, IServiceRequestPackageDto servicePackage,
			EntityModification modification)
		{
			return _serviceRequestPackageController.ModifyServiceRequestPackage(performingUserId, servicePackage, modification);
		}

		/// <summary>
		/// All of the Service Request Packages in the database
		/// </summary>
		public IEnumerable<IServiceRequestPackageDto> AllServiceRequestPackages
		{
			get { return _serviceRequestPackageController.AllServiceRequestPackages; }
		}

		/// <summary>
		/// Retrieves the service packages that the service option id exists in
		/// </summary>
		/// <param name="performingUserId"></param>
		/// <param name="serviceOptionId"></param>
		/// <returns></returns>
		public IEnumerable<IServiceRequestPackageDto> GetServiceRequestPackagesForServiceOption(int performingUserId, int serviceOptionId, ServiceRequestAction action)
		{
			return _serviceRequestPackageController.GetServiceRequestPackagesForServiceOption(performingUserId, serviceOptionId, action);
		}
	}
}
