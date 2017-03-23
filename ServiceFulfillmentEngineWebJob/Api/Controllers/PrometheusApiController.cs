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
		private int _userId;
		private string _apiKey;

		public PrometheusApiController(int userId, string apiKey)
		{
			_userId = userId;
			_apiKey = apiKey;
			_apiString = ConfigurationManager.AppSettings["ApiUrl"];
		}

		public IEnumerable<IServiceRequest> GetServiceRequests()
		{
			var response = Request(Method.GET, null);

			if (response.StatusCode == HttpStatusCode.OK)
			{
				return JsonConvert.DeserializeObject<List<IServiceRequest>>(response.Content);
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

		public void UpdateRequestById(int serviceRequestId, IServiceRequest serviceRequest)
		{
			var response = Request(Method.PUT, serviceRequest, serviceRequestId);

			if (response.StatusCode != HttpStatusCode.OK)
			{
				throw response.ErrorException;
			}
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

			if (serviceRequest != null)
			{
				request.RequestFormat = DataFormat.Json;
				request.AddBody(serviceRequest);
			}

			return client.Execute(request);
		}
	}
}
