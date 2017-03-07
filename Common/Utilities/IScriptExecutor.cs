using System;

namespace Common.Utilities
{
	public interface IScriptExecutor
	{
		int GetUserDepartment(Guid userGuid);
	}
}