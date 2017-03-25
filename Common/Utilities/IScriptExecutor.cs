using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;

namespace Common.Utilities
{
	public interface IScriptExecutor
	{
		string GetUserDepartment(Guid userGuid, Guid scriptGuid);
		string GetDepartmentUsers(Guid userGuid, Guid scriptGuid);
		List<ScriptResult<string, string>> ExecuteScript(Guid userGuid, Guid scriptGuid);
		Collection<PSObject> GeneralElScriptador(Guid userGuid, Guid scriptGuid);


	}
}