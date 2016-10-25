using Common.Dto;
using System;
using System.Collections.Generic;

namespace ServicePortfolio.Controllers
{
	internal class ServiceController : IServiceController
	{
		public bool DeleteService(int serviceId)
		{
			throw new NotImplementedException();
		}

		public IServiceDto GetService(int serviceId)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Tuple<int, string>> GetServiceNamesForServiceBundle(int serviceBundleId)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<IServiceDto> GetServicesForServiceBundle(int serviceBundleId)
		{
			throw new NotImplementedException();
		}

		public bool SaveService(IServiceDto service)
		{
			throw new NotImplementedException();
		}
	}
}
