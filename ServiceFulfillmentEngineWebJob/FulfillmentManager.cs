using System;
using System.Collections.Generic;
using Common.Dto;
using Common.Enums;
using ServiceFulfillmentEngineWebJob.ApiControllers;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace ServiceFulfillmentEngineWebJob
{
	public class FulfillmentManager
	{
		private int _userId;
		private string _apiKey;

		public FulfillmentManager(int userId, string apiKey)
		{
			_userId = userId;
			_apiKey = apiKey;
		}

		public void FulfillNewRequests()
		{
			var newRequests = GetNewServiceRequests();
			foreach (var request in newRequests)
			{
				ProcessRequest(request);
			}
		}

		/// <summary>
		/// Returns if the request is already processed or not
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		private bool IsProcessedRequest(IServiceRequestDto request)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Processes Fulfillment of a Prometheus Service Request
		/// </summary>
		/// <param name="request"></param>
		private void ProcessRequest(IServiceRequestDto request)
		{
			Runspace runspace = RunspaceFactory.CreateRunspace();
			runspace.Open();

			// create a pipeline and feed it the script text

			Pipeline pipeline = runspace.CreatePipeline();
			pipeline.Commands.AddScript(" A B C ");
			foreach (var userInput in request.ServiceRequestUserInputs)		//just add everything
			{
				runspace.SessionStateProxy.SetVariable(userInput.Name, userInput.Value);    
			}
			

			/* Just chill for now
			var processedServiceOptions = new List<IServiceRequestOptionDto>();

			foreach (var option in request.ServiceRequestOptions)
			{
				if (IsProcessableOption(option))
				{
					ProcessServiceOption(option);
					processedServiceOptions.Add(option);
				}
			}
			
			ForwardRequest(request, processedServiceOptions); */
			FulfillPrometheusRequest(request);
		}

		/// <summary>
		/// Fulfills the request in Prometheus
		/// </summary>
		private void FulfillPrometheusRequest(IServiceRequestDto request)
		{
			request.State = ServiceRequestState.Fulfilled;
			var controller = new PrometheusApiController(_userId, _apiKey);

			controller.UpdateRequestById(request.Id, request);
		}

		/// <summary>
		/// Sends the request and relevant data to the ticketing system
		/// Also Saves the fulfillment record in the database
		/// </summary>
		/// <param name="request"></param>
		/// <param name="processedServiceOptions"></param>
		private void ForwardRequest(IServiceRequestDto request, List<IServiceRequestOptionDto> processedServiceOptions)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Runs the script relevant to the SRO
		/// </summary>
		/// <param name="option"></param>
		private void ProcessServiceOption(IServiceRequestOptionDto option)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Returns if the SRO has a script
		/// </summary>
		/// <param name="option"></param>
		/// <returns></returns>
		private bool IsProcessableOption(IServiceRequestOptionDto option)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets all of the service requests from the API and then filters for new ones
		/// </summary>
		/// <returns></returns>
		private IEnumerable<IServiceRequestDto> GetNewServiceRequests()
		{
			var controller = new PrometheusApiController(_userId, _apiKey);
			var requests = controller.GetServiceRequests();
			foreach (var request in requests)
			{
				if (!IsProcessedRequest(request))
				{
					yield return request;
				}
			}
		}
	}
}
