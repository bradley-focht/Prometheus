using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Runspaces;
using ServiceFulfillmentEngineWebJob.Api.Controllers;
using ServiceFulfillmentEngineWebJob.Api.Models;
using ServiceFulfillmentEngineWebJob.Api.Models.Enums;
using ServiceFulfillmentEngineWebJob.EntityFramework.DataAccessLayer;
using ServiceFulfillmentEngineWebJob.EntityFramework.Models;

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
		private bool IsProcessedRequest(IServiceRequest request)
		{
			return request.State != ServiceRequestState.Approved;
		}

		/// <summary>
		/// Processes Fulfillment of a Prometheus Service Request
		/// </summary>
		/// <param name="request"></param>
		private void ProcessRequest(IServiceRequest request)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Processing new Request {request.Name}");
			Console.BackgroundColor = ConsoleColor.White;

			List<Script> scripts = null;
			using (var context = new ServiceFulfillmentEngineContext())
			{
				scripts = context.Scripts.ToList();
			}

			DoSomeWeirdScriptLookingStuffThatShouldBeItsOwnFunctionOrCommented(request, scripts);

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

		private void DoSomeWeirdScriptLookingStuffThatShouldBeItsOwnFunctionOrCommented(IServiceRequest request, List<Script> scripts)
		{
			Runspace runspace = RunspaceFactory.CreateRunspace();
			runspace.Open();

			// create a pipeline and feed it the script text

			Pipeline pipeline = runspace.CreatePipeline();
			pipeline.Commands.AddScript(" A B C ");
			foreach (var userInput in request.ServiceRequestUserInputs)     //just add everything
			{
				runspace.SessionStateProxy.SetVariable(userInput.Name, userInput.Value);
			}
		}

		/// <summary>
		/// Fulfills the request in Prometheus
		/// </summary>
		private void FulfillPrometheusRequest(IServiceRequest request)
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
		private void ForwardRequest(IServiceRequest request, List<IServiceRequest> processedServiceOptions)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Runs the script relevant to the SRO
		/// </summary>
		/// <param name="option"></param>
		private void ProcessServiceOption(IServiceRequestOption option)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Returns if the SRO has a script
		/// </summary>
		/// <param name="option"></param>
		/// <returns></returns>
		private bool IsProcessableOption(IServiceRequestOption option)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets all of the service requests from the API and then filters for new ones
		/// </summary>
		/// <returns></returns>
		private IEnumerable<IServiceRequest> GetNewServiceRequests()
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
