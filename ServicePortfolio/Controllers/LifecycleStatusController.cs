using ServicePortfolio.Dto;
using System;
using System.Collections.Generic;

namespace ServicePortfolio.Controllers
{
	internal class LifecycleStatusController : ILifecycleStatusController
	{
		public IEnumerable<Tuple<int, string>> GetLifecycleStatusNames()
		{
			throw new NotImplementedException();
		}

		public ILifecycleStatusDto GetLifecycleStatus(int lifecycleStatusId)
		{
			throw new NotImplementedException();
		}

		public bool SaveLifecycleStatus(ILifecycleStatusDto lifecycleStatus)
		{
			throw new NotImplementedException();
		}


		public bool DeleteLifecycleStatus(int lifecycleStatusId)
		{
			throw new NotImplementedException();
		}
	}
}
