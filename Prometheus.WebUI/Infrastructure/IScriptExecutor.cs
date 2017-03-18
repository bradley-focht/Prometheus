using System;

namespace Prometheus.WebUI.Infrastructure
{
	public interface IScriptExecutor
	{
		string GetUserDepartment(Guid userGuid);

	}
}