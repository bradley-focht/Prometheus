using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;


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

        /// <summary>
        /// General case for executing the script
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="scriptId"></param>
        /// <returns></returns>
	    public List<ScriptResult<string, string>> ExecuteScript(Guid userGuid, Guid scriptId)
	    {
	        List<ScriptResult<string, string>> myOptions = new List<ScriptResult<string, string>>();

			string scriptText = "";

	        var path = ConfigurationManager.AppSettings["ServiceDocsPath"];

            // create PowerShell runspace
            Runspace runspace = RunspaceFactory.CreateRunspace();

            runspace.Open();

            // create a pipeline and feed it the script text

            Pipeline pipeline = runspace.CreatePipeline();
            pipeline.Commands.AddScript(scriptText);

            // add an extra command to transform the script
            // output objects into nicely formatted strings

            // remove this line to get the actual objects
            // that the script returns. For example, the script

            // "Get-Process" returns a collection
            // of System.Diagnostics.Process instances.

            pipeline.Commands.Add("Out-String");

            // execute the script

            Collection <PSObject> results = pipeline.Invoke();

            // close the runspace

            runspace.Close();

            // convert the script result into a single string

            StringBuilder stringBuilder = new StringBuilder();
            foreach (PSObject obj in results)
            {
				//here we go
                stringBuilder.AppendLine(obj.ToString());
            }

            return myOptions;
	    }
	}
}

