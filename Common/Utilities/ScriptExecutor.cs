using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
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

			var scriptGuid = Path.Combine(ConfigurationManager.AppSettings["GetDepartmentScriptId"]);

			// open it
			runspace.Open();
			Pipeline pipeline = runspace.CreatePipeline();
			// pipeline.Commands.AddScript("(Get-ADUser -Filter \"ObjectGUID -eq '$guid'\" -properties Department | Select-Object Department | Format-Table -HideTableHeaders | Out-String).Trim()");
			
			// I suppose this is the way to 
			pipeline.Commands.AddScript(scriptGuid + ".ps1");
			runspace.SessionStateProxy.SetVariable("guid", userGuid);
			Collection<PSObject> results = pipeline.Invoke();
			runspace.Close();

			var firstOrDefault = results.FirstOrDefault();
			return firstOrDefault?.ToString();
		}

		public string GetDepartmentUsers(Guid userGuid)
		{
			//to be removed
			Runspace runspace = RunspaceFactory.CreateRunspace();

			var scriptGuid = Path.Combine(ConfigurationManager.AppSettings["GetDepartmentUsersScriptId"]);

			// open it
			runspace.Open();
			Pipeline pipeline = runspace.CreatePipeline();
			// pipeline.Commands.AddScript("(Get-ADUser -Filter \"ObjectGUID -eq '$guid'\" -properties Department | Select-Object Department | Format-Table -HideTableHeaders | Out-String).Trim()");

			// I suppose this is the way to 
			pipeline.Commands.AddScript(scriptGuid + ".ps1");
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
		public List<ScriptResult<string, string>> ExecuteScript(Guid userGuid, Guid scriptGuid)
		{
			List<ScriptResult<string, string>> myOptions = new List<ScriptResult<string, string>>();
			Collection<PSObject> results = GeneralElScriptador(userGuid, scriptGuid);

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

		/// <summary>
		/// A general function that executes a script
		/// </summary>
		/// <param name="userGuid"></param>
		/// <param name="scriptGuid"></param>
		/// <returns>
		/// Collection of PSObject
		/// </returns>
		public Collection<PSObject> GeneralElScriptador(Guid userGuid, Guid scriptGuid)
		{

			var path = Path.Combine(ConfigurationManager.AppSettings["ScriptPath"], scriptGuid.ToString());

			// create PowerShell runspace
			Runspace runspace = RunspaceFactory.CreateRunspace();
			runspace.Open();

			// create a pipeline and feed it the script text
			Pipeline pipeline = runspace.CreatePipeline();

			// BRAD: whats the difference between this line of code and vs line 35?
			pipeline.Commands.AddScript(System.Web.HttpContext.Current.Server.MapPath(path) + "ps1");
			runspace.SessionStateProxy.SetVariable("guid", userGuid);

			Collection<PSObject> results = pipeline.Invoke();


			runspace.Close();

			return results;
		}
	}
}

