using System.Collections.Generic;
using System.Configuration;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using ServiceFulfillmentEngineWebJob.Api.Models;

namespace ServiceFulfillmentEngineWebJob.Api.Controllers
{
	public class PrometheusApiController : IPrometheusApiController
	{
		private readonly string _apiString;
		private string _username;
		private string _password;

		/// <summary>
		/// Creates the API Controller and uses the credentials provided to make API calls
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		public PrometheusApiController(string username, string password)
		{
			_username = username;
			_password = password;
			_apiString = ConfigurationManager.AppSettings["ApiUrl"];
		}

		/// <summary>
		/// Makes Prometheus GET /Requests call
		/// </summary>
		public IEnumerable<ServiceRequest> GetServiceRequests()
		{
			var response = Request(Method.GET, null);

			if (response.StatusCode == HttpStatusCode.OK)
			{
				return JsonConvert.DeserializeObject<List<ServiceRequest>>(response.Content);
			}

			throw response.ErrorException;
		}

		/// <summary>
		/// Makes Prometheus GET /Requests/{id} call for an ID
		/// </summary>
		/// <param name="serviceRequestId"></param>
		public IServiceRequest GetServiceRequestById(int serviceRequestId)
		{
			var response = Request(Method.GET, null, serviceRequestId);

			if (response.StatusCode == HttpStatusCode.OK)
			{
				return JsonConvert.DeserializeObject<IServiceRequest>(response.Content);
			}

			throw response.ErrorException;
		}

		/// <summary>
		/// Makes Prometheus PUT /Requests/{id} call for a SR provided
		/// </summary>
		/// <param name="serviceRequestId"></param>
		/// <param name="serviceRequest">Updated SR</param>
		public IServiceRequest UpdateRequestById(int serviceRequestId, IServiceRequest serviceRequest)
		{
			var response = Request(Method.PUT, serviceRequest, serviceRequestId);

			if (response.StatusCode == HttpStatusCode.OK)
			{
				return JsonConvert.DeserializeObject<ServiceRequest>(response.Content);
			}
			throw response.ErrorException;
		}

		/// <summary>
		/// Makes Prometheus DELETE /Requests/{id} call for an ID
		/// </summary>
		/// <param name="serviceRequestId"></param>
		public void DeleteRequestById(int serviceRequestId)
		{
			var response = Request(Method.DELETE, null, serviceRequestId);

			if (response.StatusCode != HttpStatusCode.OK)
			{
				throw response.ErrorException;
			}
		}

		/// <summary>
		/// Builds a RestRequest and executes it for the Prometheus API
		/// </summary>
		/// <param name="method">Type of call to make</param>
		/// <param name="serviceRequest">Request for body of call</param>
		/// <param name="requestId">ID to add to URI of the call</param>
		/// <returns>Result of the RestRequest made</returns>
		private IRestResponse Request(Method method, IServiceRequest serviceRequest, int requestId = 0)
		{
			var url = $"{_apiString}/Request";

			if (requestId != 0)
			{
				url += $"/{requestId}";
			}

			var client = new RestClient(url);
			var request = new RestRequest(method);
			request.AddHeader("Username", _username);
			request.AddHeader("Password", _password);

			if (serviceRequest != null)
			{
				request.RequestFormat = DataFormat.Json;
				var serializedRequest = JsonConvert.SerializeObject(serviceRequest);
				request.AddBody(serializedRequest);
			}

			return client.Execute(request);
		}
	}
}
