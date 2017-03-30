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

		public PrometheusApiController(string username, string password)
		{
			_username = username;
			_password = password;
			_apiString = ConfigurationManager.AppSettings["ApiUrl"];
		}

		public IEnumerable<ServiceRequest> GetServiceRequests()
		{
			var response = Request(Method.GET, null);

			if (response.StatusCode == HttpStatusCode.OK)
			{
				return JsonConvert.DeserializeObject<List<ServiceRequest>>(response.Content);
			}

			throw response.ErrorException;
		}

		public IServiceRequest GetServiceRequestById(int serviceRequestId)
		{
			var response = Request(Method.GET, null, serviceRequestId);

			if (response.StatusCode == HttpStatusCode.OK)
			{
				return JsonConvert.DeserializeObject<IServiceRequest>(response.Content);
			}

			throw response.ErrorException;
		}

		public IServiceRequest UpdateRequestById(int serviceRequestId, IServiceRequest serviceRequest)
		{
			var response = Request(Method.PUT, serviceRequest, serviceRequestId);

			if (response.StatusCode == HttpStatusCode.OK)
			{
				return JsonConvert.DeserializeObject<ServiceRequest>(response.Content);
			}
			throw response.ErrorException;
		}
		public void DeleteRequestById(int serviceRequestId)
		{
			var response = Request(Method.DELETE, null, serviceRequestId);

			if (response.StatusCode != HttpStatusCode.OK)
			{
				throw response.ErrorException;
			}
		}

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
