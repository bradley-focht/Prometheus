using System.Collections.Generic;
using System.Configuration;
using System.Net;
using Common.Dto;
using Newtonsoft.Json;
using RestSharp;

namespace ServiceFulfillmentEngineWebJob.ApiControllers
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

		public IEnumerable<IServiceRequestDto> GetServiceRequests()
		{
			var response = Request(Method.GET, null);

			if (response.StatusCode == HttpStatusCode.OK)
			{
				return JsonConvert.DeserializeObject<List<IServiceRequestDto>>(response.Content);
			}

			throw response.ErrorException;
		}

		public IServiceRequestDto GetServiceRequestById(int serviceRequestId)
		{
			var response = Request(Method.GET, null, serviceRequestId);

			if (response.StatusCode == HttpStatusCode.OK)
			{
				return JsonConvert.DeserializeObject<IServiceRequestDto>(response.Content);
			}

			throw response.ErrorException;
		}

		public void UpdateRequestById(int serviceRequestId, IServiceRequestDto serviceRequest)
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

		private IRestResponse Request(Method method, IServiceRequestDto serviceRequest, int requestId = 0)
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
