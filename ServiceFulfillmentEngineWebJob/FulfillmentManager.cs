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
			var newRequests = GetFulfillableServiceRequests();
			foreach (var request in newRequests)
			{
				ProcessRequest(request);
			}
		}

		/// <summary>
		/// Returns if the request is fulfillable or not
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		private bool IsFulfillableRequest(IServiceRequest request)
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

			Script script = null;
			string code = GetCodeFromRequest(request);

			//see if there is any script for this
			if (code != null)
			{
				using (var context = new ServiceFulfillmentEngineContext())
				{
					script = context.Scripts.FirstOrDefault(x => x.ApplicableCode == code);
				}
			}
			if (script != null)
			{
				Console.WriteLine($"Identified as executable on '{code}'");
				ExecuteScriptForRequest(script, request);
			}

			ForwardRequest(request);

			FulfillPrometheusRequest(request);
		}

		/// <summary>
		/// Returns the appropriate code for a Request provided one exists. 
		/// Otherwise return null
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		private string GetCodeFromRequest(IServiceRequest request)
		{
			using (var context = new ServiceFulfillmentEngineContext())
			{
				IEnumerable<Script> highPriorityScripts = null;
				IEnumerable<Script> lowPriorityScripts = null;
				try
				{
					//get scripts
					highPriorityScripts = from s in context.Scripts where s.Priority == Priority.High select s;
					lowPriorityScripts = from s in context.Scripts where s.Priority == Priority.Low select s;
				}
				catch (Exception) {/* database empty or other db problem */ }

				string code = null;
				//try high priority codes available first

				if (highPriorityScripts != null)
				{
					code = (from r in request.ServiceRequestOptions
							where
								r.Code ==
								(from s in highPriorityScripts where s.ApplicableCode == r.Code select s.ApplicableCode).FirstOrDefault()
							select r.Code).FirstOrDefault();
				}

				//if not try for a lower priority codes
				if (code == null && lowPriorityScripts != null)
				{
					code = (from r in request.ServiceRequestOptions
							where
							r.Code == (from s in lowPriorityScripts where s.ApplicableCode == r.Code select s.ApplicableCode).FirstOrDefault()
							select r.Code).FirstOrDefault();
				}
				return code;
			}
		}

		/// <summary>
		/// Do really cool powershell things
		/// </summary>
		/// <param name="script"></param>
		/// <param name="request"></param>
		private void ExecuteScriptForRequest(Script script, IServiceRequest request)
		{
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
		/// Gets all of the service requests from the API and then filters for ones that can be fulfilled
		/// </summary>
		/// <returns></returns>
		private IEnumerable<IServiceRequest> GetFulfillableServiceRequests()
		{
			var controller = new PrometheusApiController(_username, _password);
			var requests = controller.GetServiceRequests();
			if (requests != null)
			{
				foreach (var request in requests)
				{
					if (!IsFulfillableRequest(request))
					{
						yield return request;
					}
				}
			}
		}
	}
}
