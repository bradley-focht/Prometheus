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
		/// <param name="scriptGuid"></param>
		/// <returns></returns>
		public string GetUserDepartment(Guid userGuid, Guid scriptGuid)
		{
			var path = Path.Combine(ConfigurationManager.AppSettings["ScriptPath"], 
				ConfigurationManager.AppSettings["GetDepartmentScriptId"], 
				scriptGuid + ".ps1");

			Collection<PSObject> results = GeneralElScriptador(userGuid, scriptGuid, path);
			var firstOrDefault = results.FirstOrDefault();
			return firstOrDefault?.ToString();
		}

		/// <summary>
		/// Returns all the users in the dapartment
		/// </summary>
		/// <param name="userGuid"></param>
		/// <param name="scriptGuid"></param>
		/// <returns></returns>
		public List<ScriptResult<string, string>> GetDepartmentUsers(Guid userGuid, Guid scriptGuid)
		{
			// var path = Path.Combine(ConfigurationManager.AppSettings["GetDepartmentUsersScriptId"], scriptGuid + ".ps1");
			var path = Path.Combine(ConfigurationManager.AppSettings["GetDepartmentUsersScriptId"], scriptGuid + ".ps1");

			List<ScriptResult<string, string>> myOptions = new List<ScriptResult<string, string>>();
			Collection<PSObject> results = GeneralElScriptador(userGuid, scriptGuid, path);

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
		/// General case for executing the script
		/// </summary>
		/// <param name="userGuid"></param>
		/// <param name="scriptGuid"></param>
		/// <returns></returns>
		public List<ScriptResult<string, string>> ExecuteScript(Guid userGuid, Guid scriptGuid)
		{
			var path = Path.Combine(ConfigurationManager.AppSettings["ScriptPath"], scriptGuid + ".ps1");

			List<ScriptResult<string, string>> myOptions = new List<ScriptResult<string, string>>();
			Collection<PSObject> results = GeneralElScriptador(userGuid, scriptGuid, path);

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
		/// A general function that executes a script,
		/// given the script guid
		/// </summary>
		/// <param name="userGuid"></param>
		/// <param name="scriptGuid"></param>
		/// <param name="path"></param>
		/// <returns>
		/// Collection of PSObject
		/// </returns>
		public Collection<PSObject> GeneralElScriptador(Guid userGuid, Guid scriptGuid, string path)
		{
			// create PowerShell runspace
			Runspace runspace = RunspaceFactory.CreateRunspace();
			runspace.Open();

			// create a pipeline and feed it the script text
			Pipeline pipeline = runspace.CreatePipeline();
			pipeline.Commands.AddScript(System.Web.HttpContext.Current.Server.MapPath(path));
			runspace.SessionStateProxy.SetVariable("guid", userGuid);

			Collection<PSObject> results = pipeline.Invoke();
			runspace.Close();

			return results;
		}
	}
}

