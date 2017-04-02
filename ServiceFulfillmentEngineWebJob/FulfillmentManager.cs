using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using ServiceFulfillmentEngineWebJob.Api.Controllers;
using ServiceFulfillmentEngineWebJob.Api.Models;
using ServiceFulfillmentEngineWebJob.Api.Models.Enums;
using ServiceFulfillmentEngineWebJob.EntityFramework.DataAccessLayer;
using ServiceFulfillmentEngineWebJob.EntityFramework.Models;
using ServiceFulfillmentEngineWebJob.EntityFramework.Models.Enums;
using ServiceFulfillmentEngineWebJob.Enums;

namespace ServiceFulfillmentEngineWebJob
{
	public class FulfillmentManager
	{
		private readonly string _username;
		private readonly string _password;

		public FulfillmentManager(string username, string password)
		{
			_username = username;
			_password = password;
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
			Console.WriteLine("\n");
			Displaymessage($"Processing new Request {request.Name}", MessageType.GoodNews);



			using (var context = new ServiceFulfillmentEngineContext())
			{
				Script script = null;
				IEnumerable<Script> highPriorityScripts;
				IEnumerable<Script> lowPriorityScripts;

				//get scripts
				highPriorityScripts = from s in context.Scripts where s.Priority == Priority.High select s;
				lowPriorityScripts = from s in context.Scripts where s.Priority == Priority.Low select s;

				//try high priority codes available first
				var code = (from r in request.ServiceRequestOptions
					where r.Code == (from s in highPriorityScripts where s.ApplicableCode == r.Code select s.ApplicableCode).FirstOrDefault()
					select r.Code).FirstOrDefault();

				//if not try for a lower priority codes
				if (code == null)
				{
					code = (from r in request.ServiceRequestOptions
								   where r.Code == (from s in lowPriorityScripts where s.ApplicableCode == r.Code select s.ApplicableCode).FirstOrDefault()
								   select r.Code).FirstOrDefault();
				}

				//see if there is any script for this
				if (code != null)
				{
					script = context.Scripts.FirstOrDefault(x => x.ApplicableCode == code);
				}
				if (script != null)
				{
					Console.WriteLine($"Identified as executable on {code}'");

					Runspace runspace = RunspaceFactory.CreateRunspace();
					runspace.Open();

					// create a pipeline and feed it the script
					Pipeline pipeline = runspace.CreatePipeline();

					try
					{
						pipeline.Commands.AddScript(@"C:\Scripts\" + script.FileName);
					}
					catch (Exception exception)
					{
						Displaymessage($"Error: {exception.Message}", MessageType.Failure);
					}

					foreach (var userInput in request.ServiceRequestUserInputs) //just add everything
					{
						runspace.SessionStateProxy.SetVariable(userInput.Name, userInput.Value);
					}
					try
					{
						Collection<PSObject> results = pipeline.Invoke();
						Console.WriteLine("Script results: ");

						foreach (var psObject in results)
						{
							Console.WriteLine(psObject);
						}


					}
					catch (Exception exception)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine($"Error in executing script: {exception.Message}");
						Console.ForegroundColor = ConsoleColor.White;
					}
					Console.WriteLine($"Completed execution of {request.Name}");
				}
			}
			ForwardRequest(request);

			FulfillPrometheusRequest(request);
		}

		/// <summary>
		/// Fulfills the request in Prometheus
		/// </summary>
		private void FulfillPrometheusRequest(IServiceRequest request)
		{
			Displaymessage($"Set Request {request.Name} to fulfilled", MessageType.Info);
			request.State = ServiceRequestState.Fulfilled;
			try
			{
				var controller = new PrometheusApiController(_username, _password);
				request = controller.UpdateRequestById(request.Id, request);
				if (request.State != ServiceRequestState.Fulfilled)
					Displaymessage($"State set to {request.State}", MessageType.Info);
			}
			catch (Exception exception)
			{
				Displaymessage($"Error : {exception.Message}", MessageType.Failure);
			}
		}

		/// <summary>
		/// Display messages to the console
		/// </summary>
		/// <param name="message"></param>
		/// <param name="type"></param>
		private void Displaymessage(string message, MessageType type)
		{
			switch (type)
			{
				case MessageType.Failure:
					Console.ForegroundColor = ConsoleColor.Red;
					break;
				case MessageType.GoodNews:
					Console.ForegroundColor = ConsoleColor.Green;
					break;
			}

			Console.WriteLine(message);
			Console.ForegroundColor = ConsoleColor.White;
		}

		/// <summary>
		/// Sends the request and relevant data to the ticketing system
		/// Also Saves the fulfillment record in the database
		/// </summary>
		/// <param name="request"></param>

		private void ForwardRequest(IServiceRequest request)
		{
			Displaymessage($"Forward Request {request.Name} to Ticketing System", MessageType.Info);
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
			var controller = new PrometheusApiController(_username, _password);
			var requests = controller.GetServiceRequests();
			if (requests != null)
			{
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
}
