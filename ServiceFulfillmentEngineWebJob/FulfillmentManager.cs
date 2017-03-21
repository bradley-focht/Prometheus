using System;
using System.Collections.Generic;
using Common.Dto;
using Common.Enums;
using ServiceFulfillmentEngineWebJob.ApiControllers;

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
			var newRequests = GetServiceRequests();
			foreach (var request in newRequests)
			{
				if (!IsProcessedRequest(request))
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
			var processedServiceOptions = new List<IServiceRequestOptionDto>();

			foreach (var option in request.ServiceRequestOptions)
			{
				if (IsProcessableOption(option))
				{
					ProcessServiceOption(option);
					processedServiceOptions.Add(option);
				}
			}

			ForwardRequest(request, processedServiceOptions);
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
		private IEnumerable<IServiceRequestDto> GetServiceRequests()
		{
			var controller = new PrometheusApiController(_userId, _apiKey);
			return controller.GetServiceRequests();
		}
	}
}
