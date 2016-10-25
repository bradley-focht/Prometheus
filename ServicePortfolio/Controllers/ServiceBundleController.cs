using Common.Dto;
using System;
using System.Collections.Generic;

namespace ServicePortfolio.Controllers
{
	internal class ServiceBundleController : IServiceBundleController
	{
		public bool DeleteServiceBundle(int serviceBundleId)
		{
			throw new NotImplementedException();
		}

		public IServiceBundleDto GetServiceBundle(int serviceBundleId)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Tuple<int, string>> GetServiceBundleNames()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<IServiceBundleDto> GetServiceBundles()
		{
			throw new NotImplementedException();
		}

		public bool SaveServiceBundle(IServiceBundleDto serviceBundle)
		{
			throw new NotImplementedException();
		}
	}
}
