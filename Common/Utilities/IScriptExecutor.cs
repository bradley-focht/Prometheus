using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;

namespace Common.Utilities
{
	public interface IScriptExecutor
	{
		/// <summary>
		/// Returns the user's department
		/// </summary>
		/// <param name="userGuid"></param>
		/// <param name="scriptGuid"></param>
		/// <returns></returns>
		string GetUserDepartment(Guid userGuid, Guid scriptGuid);

		/// <summary>
		/// Returns all the users in the dapartment
		/// </summary>
		/// <param name="userGuid"></param>
		/// <param name="scriptGuid"></param>
		/// <returns></returns>
		List<ScriptResult<string, string>> GetDepartmentUsers(Guid userGuid, Guid scriptGuid);

		/// <summary>
		/// General case for executing the script
		/// </summary>
		/// <param name="userGuid"></param>
		/// <param name="scriptGuid"></param>
		/// <returns></returns>
		List<ScriptResult<string, string>> ExecuteScript(Guid userGuid, Guid scriptGuid);

		/// <summary>
		/// A general function that executes a script,
		/// given the script guid
		/// </summary>
		/// <param name="userGuid"></param>
		/// <param name="scriptGuid"></param>
		/// <param name="path"></param>
		/// <returns>
		/// Collection of PSObject
		/// </returns>
		Collection<PSObject> GeneralElScriptador(Guid userGuid, Guid scriptGuid, string path);
	}
}