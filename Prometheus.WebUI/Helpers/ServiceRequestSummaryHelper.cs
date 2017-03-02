using System.Collections.Generic;
using System.Linq;
using Common.Dto;
using Common.Enums;
using Prometheus.WebUI.Models.ServiceRequest;
using Prometheus.WebUI.Models.ServiceRequestApproval;
using ServicePortfolioService;

namespace Prometheus.WebUI.Helpers
{
	public class ServiceRequestSummaryHelper
	{
		public static ServiceRequestStateChangeModel CreateStateChangeModel(IPortfolioService ps,
			ServiceRequestState nextState, int userId, int serviceRequestId)
		{
			var model = new ServiceRequestStateChangeModel
			{
				NextState = nextState,
				ServiceRequestModel = new ServiceRequestModel()
			};
			model.ServiceRequestModel.ServiceRequest = ps.GetServiceRequest(userId, serviceRequestId);

			//model.ServiceRequestModel.Package = ServicePackageHelper.GetPackage(userId, ps, model.ServiceRequestModel.ServiceOptionId);

			List<DisplayListOption> displayList = new List<DisplayListOption>();

			foreach (var serviceRequestOption in model.ServiceRequestModel.ServiceRequest.ServiceRequestOptions)
			{
				var listOption = new DisplayListOption {ServiceRequestOption = serviceRequestOption, UserInputs =  new List<DisplayListUserInput>()};
				listOption.ServiceOption = ps.GetServiceOption(userId, serviceRequestOption.ServiceOptionId);

				var userInputs = ps.GetInputsForServiceOptions(userId,
					new IServiceOptionDto[1] {new ServiceOptionDto {Id = serviceRequestOption.ServiceOptionId}});
				if (userInputs != null)
				{
					foreach (var userInput in userInputs.UserInputs)
					{
						var displayUserInput = new DisplayListUserInput {DisplayName = userInput.DisplayName};
						//displayUserInput.ServiceRequestUserInput = (from u in model.ServiceRequestModel.ServiceRequest.ServiceRequestUserInputs
						//											where u.InputId)
						listOption.UserInputs.Add(displayUserInput);

					}
					
				}
				displayList.Add(listOption);
			}
			model.DisplayList = displayList;
			return model;
		}
	}
}