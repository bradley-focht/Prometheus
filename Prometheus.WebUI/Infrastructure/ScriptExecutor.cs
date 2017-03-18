using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace Prometheus.WebUI.Infrastructure
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

	        var path = Path.Combine(ConfigurationManager.AppSettings["ScriptPath"], scriptId.ToString()) ;

            // create PowerShell runspace
            Runspace runspace = RunspaceFactory.CreateRunspace();

            runspace.Open();

            // create a pipeline and feed it the script text

            Pipeline pipeline = runspace.CreatePipeline();
	        string text = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath(path));
			pipeline.Commands.AddScript(text);
			// runspace.SessionStateProxy.SetVariable("guid", userGuid);
			//pipeline.Commands.Add("Out-String"); /* I don't actually know what this does */
			runspace.SessionStateProxy.SetVariable("guid", userGuid);
			Collection <PSObject> results = pipeline.Invoke();

            runspace.Close();

			// convert the script result into a single string

			/* StringBuilder stringBuilder = new StringBuilder();
			 foreach (PSObject obj in results)		/* this results in a bunch of powershell junk
			 {
				 // here we go
				 stringBuilder.AppendLine(obj.ToString());
			 } */
	        foreach (var result in results)
	        {
		        ScriptResult<string, string> myOption = new ScriptResult<string, string>();
		        foreach (var item in result.Members)
		        {
			        if (item.Name == "Label")
				        myOption.Label = item.Value.ToString();
					if (item.Name == "Value")
						myOption.Value = item.Value.ToString();
				}
				myOptions.Add(myOption);
	        }

			return myOptions;
	    }
	}
}

