
using System;

namespace Common.Utilities
{
	/// <summary>
	/// Executes scripts
	/// </summary>
    public class ScriptExecutor : IScriptExecutor
	{
		/// <summary>
		/// Returns the user's department
		/// </summary>
		/// <param name="userGuid"></param>
		/// <returns></returns>
		public int GetUserDepartment(Guid userGuid)
		{
			return 1;
		}


    }
}
