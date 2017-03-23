using System.Collections.Generic;
using System.Linq;
using Common.Dto;
using Common.Enums;
using Prometheus.WebUI.Models.ServiceRequest;
using Prometheus.WebUI.Models.ServiceRequestApproval;
using RequestService.Controllers;
using ServicePortfolioService;

namespace Prometheus.WebUI.Helpers
{
	/// <summary>
	/// Creates model for Service Request Summary Partial View
	/// </summary>
	public class ServiceRequestSummaryHelper
	{
		/// <summary>
		/// Create the model
		/// </summary>
		/// <param name="ps">portfolio service</param>
		/// <param name="sr"></param>
		/// <param name="nextState"></param>
		/// <param name="userId"></param>
		/// <param name="serviceRequestId"></param>
		/// <returns></returns>
		public static ServiceRequestStateChangeModel CreateStateChangeModel(IPortfolioService ps, IServiceRequestController sr,
			ServiceRequestState nextState, int userId, int serviceRequestId)
		{
			var model = new ServiceRequestStateChangeModel
			{
				NextState = nextState,
				ServiceRequestModel = new ServiceRequestModel(),
				ConfirmNextState = true
			};

			return AddModelData(model, ps, sr, userId, serviceRequestId);
		}

		public static ServiceRequestStateChangeModel CreateStateChangeModel(IPortfolioService ps, int userId, IServiceRequestController sr,
			int serviceRequestId)
		{
			var model = new ServiceRequestStateChangeModel
			{
				ServiceRequestModel = new ServiceRequestModel(),
				ConfirmNextState = false
			};
			model.ServiceRequestModel.ServiceRequest = sr.GetServiceRequest(userId, serviceRequestId);

			List<DisplayListOption> displayList = new List<DisplayListOption>();
			foreach (var serviceRequestOption in model.ServiceRequestModel.ServiceRequest.ServiceRequestOptions)    //add the option name
			{
				var listOption = new DisplayListOption
				{
					ServiceRequestOption = serviceRequestOption,
					UserInputs = new List<DisplayListUserInput>(),
					ServiceOption = ps.GetServiceOption(userId, serviceRequestOption.ServiceOptionId)
				};

				var userInputs = ps.GetInputsForServiceOptions(userId,
					new IServiceOptionDto[1] { new ServiceOptionDto { Id = serviceRequestOption.ServiceOptionId } });//get user inputs
				if (userInputs != null)
				{

					foreach (var userData in model.ServiceRequestModel.ServiceRequest.ServiceRequestUserInputs)
					{
						if (userData.UserInputType == UserInputType.Text)
						{
							foreach (var userInput in userInputs.TextInputs)
							{
								if (userInput.Id == userData.InputId)
								{
									var displayUserInput = new DisplayListUserInput { DisplayName = userInput.DisplayName };
									displayUserInput.ServiceRequestUserInput = userData;
									listOption.UserInputs.Add(displayUserInput);
								}
							}
						}
						else if (userData.UserInputType == UserInputType.Selection)
						{
							foreach (var userInput in userInputs.SelectionInputs)
							{
								if (userInput.Id == userData.InputId)
								{
									var displayUserInput = new DisplayListUserInput { DisplayName = userInput.DisplayName };
									displayUserInput.ServiceRequestUserInput = userData;
									listOption.UserInputs.Add(displayUserInput);
								}
							}
						}
						else if (userData.UserInputType == UserInputType.ScriptedSelection)
						{
							foreach (var userInput in userInputs.ScriptedSelectionInputs)
							{
								if (userInput.Id == userData.InputId)
								{
									var displayUserInput = new DisplayListUserInput { DisplayName = userInput.DisplayName };
									displayUserInput.ServiceRequestUserInput = userData;
									listOption.UserInputs.Add(displayUserInput);
								}
							}
						}
					}
				}
				displayList.Add(listOption);
			}
			model.DisplayList = displayList;
			return model;
		}

		/// <summary>
		/// Common behaviour of methods in this class
		/// </summary>
		/// <param name="model"></param>
		/// <param name="ps">portfolio service</param>
		/// <param name="sr">service request</param>
		/// <param name="userId"></param>
		/// <param name="serviceRequestId"></param>
		/// <returns></returns>
		public static ServiceRequestStateChangeModel AddModelData(ServiceRequestStateChangeModel model, IPortfolioService ps, IServiceRequestController sr, int userId, int serviceRequestId)
		{
			model.ServiceRequestModel.ServiceRequest = sr.GetServiceRequest(userId, serviceRequestId);

			List<DisplayListOption> displayList = new List<DisplayListOption>();
			foreach (var serviceRequestOption in model.ServiceRequestModel.ServiceRequest.ServiceRequestOptions)    //add the option name
			{
				var listOption = new DisplayListOption { ServiceRequestOption = serviceRequestOption, UserInputs = new List<DisplayListUserInput>() };
				listOption.ServiceOption = ps.GetServiceOption(userId, serviceRequestOption.ServiceOptionId);

				var userInputs = ps.GetInputsForServiceOptions(userId,
					new IServiceOptionDto[1] { new ServiceOptionDto { Id = serviceRequestOption.ServiceOptionId } });//get user inputs
				if (userInputs != null)
				{

					foreach (var userData in model.ServiceRequestModel.ServiceRequest.ServiceRequestUserInputs)		//user inputs
					{
						if (userData.UserInputType == UserInputType.Text)			//text
						{
							foreach (var userInput in userInputs.TextInputs)
							{
								if (userInput.Id == userData.InputId)
								{
									var displayUserInput = new DisplayListUserInput { DisplayName = userInput.DisplayName };
									displayUserInput.ServiceRequestUserInput = userData;
									listOption.UserInputs.Add(displayUserInput);
								}
							}
						}
						else if (userData.UserInputType == UserInputType.Selection)	//selection
						{
							foreach (var userInput in userInputs.SelectionInputs)
							{
								if (userInput.Id == userData.InputId)
								{
									var displayUserInput = new DisplayListUserInput { DisplayName = userInput.DisplayName };
									displayUserInput.ServiceRequestUserInput = userData;
									listOption.UserInputs.Add(displayUserInput);
								}
							}
						}
						else if (userData.UserInputType == UserInputType.ScriptedSelection)	//scripted
						{
							foreach (var userInput in userInputs.ScriptedSelectionInputs)
							{
								if (userInput.Id == userData.InputId)
								{
									var displayUserInput = new DisplayListUserInput { DisplayName = userInput.DisplayName };
									displayUserInput.ServiceRequestUserInput = userData;
									listOption.UserInputs.Add(displayUserInput);
								}
							}
						}
					}
				}
				displayList.Add(listOption);
			}
			model.DisplayList = displayList;
			return model;
		}
	}
}