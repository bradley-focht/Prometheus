using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;


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
		public string GetUserDepartment(Guid userGuid)
		{
			//to be removed
			Runspace runspace = RunspaceFactory.CreateRunspace();

			// open it
			runspace.Open();
			Pipeline pipeline = runspace.CreatePipeline();
			pipeline.Commands.AddScript("(Get-ADUser -Filter \"ObjectGUID -eq '$guid'\" -properties Department | Select-Object Department | Format-Table -HideTableHeaders | Out-String).Trim()");
			runspace.SessionStateProxy.SetVariable("guid", userGuid);
			Collection<PSObject> results = pipeline.Invoke();
			runspace.Close();

			var firstOrDefault = results.FirstOrDefault();
			return firstOrDefault?.ToString();
		}
	}
}

