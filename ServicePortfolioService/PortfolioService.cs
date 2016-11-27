using AutoMapper;
using Common.Dto;
using Common.Utilities;
using ServicePortfolioService.AutoMapperConfig;
using ServicePortfolioService.Controllers;
using System;
using System.Collections.Generic;

namespace ServicePortfolioService
{
	public class PortfolioService : IPortfolioService
	{
		//TODO: Figure out guest account
		public const int GuestUserId = -1;

		private readonly IServiceBundleController _serviceBundleController;
		private readonly IServiceController _serviceController;
		private readonly ILifecycleStatusController _lifecycleStatusController;

		//TODO: Add check for valid user being set
		private int _userId;
		public int UserId
		{
			get { return _userId; }
			set { _userId = value; }
		}

		public PortfolioService(int userId, IServiceBundleController serviceBundleController, IServiceController serviceController,
			ILifecycleStatusController lifecycleStatusController)
		{
			_userId = userId;

			_serviceBundleController = serviceBundleController;
			_serviceBundleController.UserId = _userId;

			_serviceController = serviceController;
			_serviceController.UserId = _userId;

			_lifecycleStatusController = lifecycleStatusController;
			_lifecycleStatusController.UserId = _userId;

			AutoMapperInitializer.Initialize();
			Mapper.Initialize(cfg => cfg.AddProfile<ServicePortfolioProfile>());
		}

		public IEnumerable<IServiceBundleDto> GetServiceBundles()
		{
			return _serviceBundleController.GetServiceBundles();
		}

		public IServiceBundleDto GetServiceBundle(int serviceBundleId)
		{
			return _serviceBundleController.GetServiceBundle(serviceBundleId);
		}

		//TODO: Brad / Sean Should this be a dictionary
		// probably not
		public IEnumerable<Tuple<int, string>> GetServiceBundleNames()
		{
			return _serviceBundleController.GetServiceBundleNames();
		}

		public IServiceBundleDto SaveServiceBundle(IServiceBundleDto serviceBundle)
		{
			return _serviceBundleController.SaveServiceBundle(serviceBundle);
		}

		public bool DeleteServiceBundle(int serviceBundleId)
		{
			return _serviceBundleController.DeleteServiceBundle(serviceBundleId);
		}

		public IEnumerable<Tuple<int, string>> GetLifecycleStatusNames()
		{
			return _lifecycleStatusController.GetLifecycleStatusNames();
		}

		public ILifecycleStatusDto GetLifecycleStatus(int lifecycleStatusId)
		{
			return _lifecycleStatusController.GetLifecycleStatus(lifecycleStatusId);
		}

		public ILifecycleStatusDto SaveLifecycleStatus(ILifecycleStatusDto lifecycleStatus)
		{
			return _lifecycleStatusController.SaveLifecycleStatus(lifecycleStatus);
		}

		public bool DeleteLifecycleStatus(int lifecycleStatusId)
		{
			return _lifecycleStatusController.DeleteLifecycleStatus(lifecycleStatusId);
		}

	    public int CountLifecycleStatuses()
	    {
	        return _lifecycleStatusController.CountLifecycleStatuses();
	    }


	    public IEnumerable<IServiceDto> GetServicesForServiceBundle(int serviceBundleId)
		{
			return _serviceController.GetServicesForServiceBundle(serviceBundleId);
		}

		public IServiceDto GetService(int serviceId)
		{
			return _serviceController.GetService(serviceId);
		}

		
		public IEnumerable<Tuple<int, string>> GetServiceNamesForServiceBundle(int serviceBundleId)
		{
			return _serviceController.GetServiceNamesForServiceBundle(serviceBundleId);
		}

	    public IEnumerable<Tuple<int, string>> GetServiceNames()
	    {
	        return _serviceController.GetServiceNames();
	    }

		public IServiceDto SaveService(IServiceDto service)
		{
			return _serviceController.SaveService(service);
		}

		public bool DeleteService(int serviceId)
		{
			return _serviceController.DeleteService(serviceId);
		}

	    public IEnumerable<IServiceDto> GetServices()
	    {
	        return _serviceController.GetServices();
	    }

	    public IEnumerable<IServiceDocumentDto> GetServiceDocuments(int serviceId)
	    {
	        return _serviceController.GetServiceDocuments(serviceId);
        }

	    public IServiceDocumentDto SaveServiceDocument(IServiceDocumentDto document)
	    {
	        return _serviceController.SaveServiceDocument(document);
	    }
    }
}
