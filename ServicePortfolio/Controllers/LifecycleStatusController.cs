using Common.Dto;
using System;
using System.Collections.Generic;

namespace ServicePortfolio.Controllers
{
	public class LifecycleStatusController : ILifecycleStatusController
	{
		public IEnumerable<Tuple<int, string>> GetLifecycleStatusNames(int userId)
		{
			throw new NotImplementedException();
		}

		public ILifecycleStatusDto GetLifecycleStatus(int userId, int lifecycleStatusId)
		{
			throw new NotImplementedException();
		}

		public ILifecycleStatusDto SaveLifecycleStatus(int userId, ILifecycleStatusDto lifecycleStatus)
		{
			throw new NotImplementedException();
		}

		public bool DeleteLifecycleStatus(int userId, int lifecycleStatusId)
		{
			throw new NotImplementedException();
		}
	}
}
