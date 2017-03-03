using System.Collections.Generic;
using System.Linq;
using Common.Dto;
using Common.Enums;
using Prometheus.WebUI.Models.ServiceRequest;
using Prometheus.WebUI.Models.ServiceRequestApproval;
using ServicePortfolioService;

namespace Prometheus.WebUI.Helpers
{
	/// <summary>
	/// Creates model for Service Request Summary Partial View
	/// </summary>
	public class ServiceRequestSummaryHelper
	{
		/// <summary>
		/// Create hte model
		/// </summary>
		/// <param name="ps">portfolio service</param>
		/// <param name="nextState"></param>
		/// <param name="userId"></param>
		/// <param name="serviceRequestId"></param>
		/// <returns></returns>
		public static ServiceRequestStateChangeModel CreateStateChangeModel(IPortfolioService ps,
			ServiceRequestState nextState, int userId, int serviceRequestId)
		{
			var model = new ServiceRequestStateChangeModel
			{
				NextState = nextState,
				ServiceRequestModel = new ServiceRequestModel()
			};
			model.ServiceRequestModel.ServiceRequest = ps.GetServiceRequest(userId, serviceRequestId);

			List<DisplayListOption> displayList = new List<DisplayListOption>();	
			foreach (var serviceRequestOption in model.ServiceRequestModel.ServiceRequest.ServiceRequestOptions)	//add the option name
			{
				var listOption = new DisplayListOption { ServiceRequestOption = serviceRequestOption, UserInputs = new List<DisplayListUserInput>() };
				listOption.ServiceOption = ps.GetServiceOption(userId, serviceRequestOption.ServiceOptionId);		

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
									var displayUserInput = new DisplayListUserInput {DisplayName = userInput.DisplayName};
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
						} else if (userData.UserInputType == UserInputType.ScriptedSelection)
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