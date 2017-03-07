
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Common.Dto;

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


        // TO DO:
        // GetDepartment()

    }
}
