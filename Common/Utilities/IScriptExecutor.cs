using System;

namespace Common.Utilities
{
	public interface IScriptExecutor
	{
		string GetUserDepartment(Guid userGuid);
	}
}