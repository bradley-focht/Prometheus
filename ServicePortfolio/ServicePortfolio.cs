using ServicePortfolio.Controllers;
using ServicePortfolio.Dto;
using System;
using System.Collections.Generic;

namespace ServicePortfolio
{
	public class ServicePortfolio : IServicePortfolio
	{
		private IServiceBundleController _serviceBundleController;
		private IServiceController _serviceController;
		private ILifecycleStatusController _lifecycleStatusController;

		public ServicePortfolio(IServiceBundleController serviceBundleController, IServiceController serviceController,
			ILifecycleStatusController lifecycleStatusController)
		{
			_serviceBundleController = serviceBundleController;
			_serviceController = serviceController;
			_lifecycleStatusController = lifecycleStatusController;
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
		public IEnumerable<Tuple<int, string>> GetServiceBundleNames()
		{
			return _serviceBundleController.GetServiceBundleNames();
		}

		public bool SaveServiceBundle(IServiceBundleDto serviceBundle)
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

		public bool SaveLifecycleStatus(ILifecycleStatusDto lifecycleStatus)
		{
			return _lifecycleStatusController.SaveLifecycleStatus(lifecycleStatus);
		}

		public bool DeleteLifecycleStatus(int lifecycleStatusId)
		{
			return _lifecycleStatusController.DeleteLifecycleStatus(lifecycleStatusId);
		}


		public IEnumerable<IServiceDto> GetServicesForServiceBundle(int serviceBundleId)
		{
			return _serviceController.GetServicesForServiceBundle(serviceBundleId);
		}

		public IServiceDto GetService(int serviceId)
		{
			return _serviceController.GetService(serviceId);
		}

		//TODO: Brad / Sean Should this be a dictionary
		public IEnumerable<Tuple<int, string>> GetServiceNamesForServiceBundle(int serviceBundleId)
		{
			return _serviceController.GetServiceNamesForServiceBundle(serviceBundleId);
		}

		public bool SaveService(IServiceDto service)
		{
			return _serviceController.SaveService(service);
		}

		public bool DeleteService(int serviceId)
		{
			return _serviceController.DeleteService(serviceId);
		}

	}
}
