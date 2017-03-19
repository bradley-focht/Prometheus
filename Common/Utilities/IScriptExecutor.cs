using System;
using System.Collections.Generic;

namespace Common.Utilities
{
	public interface IScriptExecutor
	{
		string GetUserDepartment(Guid userGuid);
		List<ScriptResult<string, string>> ExecuteScript(Guid userGuid, Guid scriptId);

	}
}