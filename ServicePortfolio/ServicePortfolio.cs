using Common.Dto;
using ServicePortfolio.Controllers;
using System;
using System.Collections.Generic;

namespace ServicePortfolio
{
	public class ServicePortfolio : IServicePortfolio
	{
		private readonly IServiceBundleController _serviceBundleController;
		private readonly IServiceController _serviceController;
		private readonly ILifecycleStatusController _lifecycleStatusController;

		public ServicePortfolio(IServiceBundleController serviceBundleController, IServiceController serviceController,
			ILifecycleStatusController lifecycleStatusController)
		{
			_serviceBundleController = serviceBundleController;
			_serviceController = serviceController;
			_lifecycleStatusController = lifecycleStatusController;
		}

		public IEnumerable<IServiceBundleDto> GetServiceBundles(int userId)
		{
			return _serviceBundleController.GetServiceBundles(userId);
		}

		public IServiceBundleDto GetServiceBundle(int userId, int serviceBundleId)
		{
			return _serviceBundleController.GetServiceBundle(userId, serviceBundleId);
		}

		//TODO: Brad / Sean Should this be a dictionary
		public IEnumerable<Tuple<int, string>> GetServiceBundleNames(int userId)
		{
			return _serviceBundleController.GetServiceBundleNames(userId);
		}

		public IServiceBundleDto SaveServiceBundle(int userId, IServiceBundleDto serviceBundle)
		{
			return _serviceBundleController.SaveServiceBundle(userId, serviceBundle);
		}

		public bool DeleteServiceBundle(int userId, int serviceBundleId)
		{
			return _serviceBundleController.DeleteServiceBundle(userId, serviceBundleId);
		}

		public IEnumerable<Tuple<int, string>> GetLifecycleStatusNames(int userId)
		{
			return _lifecycleStatusController.GetLifecycleStatusNames(userId);
		}

		public ILifecycleStatusDto GetLifecycleStatus(int userId, int lifecycleStatusId)
		{
			return _lifecycleStatusController.GetLifecycleStatus(userId, lifecycleStatusId);
		}

		public ILifecycleStatusDto SaveLifecycleStatus(int userId, ILifecycleStatusDto lifecycleStatus)
		{
			return _lifecycleStatusController.SaveLifecycleStatus(userId, lifecycleStatus);
		}

		public bool DeleteLifecycleStatus(int userId, int lifecycleStatusId)
		{
			return _lifecycleStatusController.DeleteLifecycleStatus(userId, lifecycleStatusId);
		}


		public IEnumerable<IServiceDto> GetServicesForServiceBundle(int userId, int serviceBundleId)
		{
			return _serviceController.GetServicesForServiceBundle(userId, serviceBundleId);
		}

		public IServiceDto GetService(int userId, int serviceId)
		{
			return _serviceController.GetService(userId, serviceId);
		}

		//TODO: Brad / Sean Should this be a dictionary
		public IEnumerable<Tuple<int, string>> GetServiceNamesForServiceBundle(int userId, int serviceBundleId)
		{
			return _serviceController.GetServiceNamesForServiceBundle(userId, serviceBundleId);
		}

		public IServiceDto SaveService(int userId, IServiceDto service)
		{
			return _serviceController.SaveService(userId, service);
		}

		public bool DeleteService(int userId, int serviceId)
		{
			return _serviceController.DeleteService(userId, serviceId);
		}
	}
}
