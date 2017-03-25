using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Dto;
using Common.Enums.Entities;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Infrastructure;
using Prometheus.WebUI.Models.Shared;
using RequestService.Controllers;
using Common.Utilities;

namespace Prometheus.WebUI.Controllers
{
	public class ScriptController : PrometheusController
	{
		private readonly IScriptFileController _scriptFileController;

		public ScriptController(IScriptFileController scriptFileController)
		{
			_scriptFileController = scriptFileController;
		}

		/// <summary>
		/// Default page of Scripting
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
		{
			var model = _scriptFileController.GetScripts(UserId).ToList();
			return View(model);
		}

		/// <summary>
		/// Genearlly for viewing the namne and description of the script - not for execvuting scripts
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult GetScript(int id)
		{
			var model = _scriptFileController.GetScript(UserId, id);
			return View(model);
		}

		/// <summary>
		/// Create a link list of scripts to select from
		/// </summary>
		/// <param name="id">selected script</param>
		/// <returns></returns>
		public ActionResult GetScripts(int id)
		{

			LinkListModel model = new LinkListModel
			{
				AddAction = "Add",
				SelectAction = "GetScript",
				Controller = "Script",
				SelectedItemId = id,
				Title = "Scripts"
			};

			model.ListItems = new List<Tuple<int, string>>();
			model.ListItems = from s in _scriptFileController.GetScripts(UserId) select new Tuple<int, string>(s.Id, s.Name);

			return View("PartialViews/_LinkList", model);

		}

		/// <summary>
		/// GET: Script/Add
		/// </summary>
		/// <returns></returns>
		public ActionResult Add()
		{
			return View(new ScriptDto());
		}

		/// <summary>
		/// GET: Script/UpdateScript/id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult UpdateScript(int id)
		{
			var model = new ScriptDto();

			if (id == 0)
			{
				// new script
				return View("Add", model);
			}

			// update script
			model = (ScriptDto)_scriptFileController.GetScript(UserId, id);
			return View("Add", model);
		}

		/// <summary>
		/// Save and update script entries
		/// </summary>
		/// <param name="newScript"></param>
		/// <param name="file"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SaveScript(ScriptDto newScript, HttpPostedFileBase file)
		{
			if (!ModelState.IsValid) /* Server side validation */
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = "Failed to save script due to invalid data";
				return RedirectToAction("Add");
			}

			try
			{
				if (Request.Files.Count > 0)
				{
					var fileName = Path.GetFileName(file.FileName);

					if (fileName != null)
					{
						Guid newFileName = Guid.NewGuid(); //to rename document			
														   //file path location comes from the Web.config file
						try
						{
							var path = Path.Combine(ConfigHelper.GetScriptPath(), newFileName + ".ps1");
							file.SaveAs(Server.MapPath(path)); /*create new doc and upload it */
							newScript = new ScriptDto()
							{
								Id = newScript.Id,
								Name = newScript.Name,
								Description = newScript.Description,
								Version = newScript.Version,
								MimeType = file.ContentType,
								ScriptFile = newFileName,
								UploadDate = DateTime.Now,
							};

							_scriptFileController.ModifyScript(UserId, newScript, newScript.Id <= 0 ? EntityModification.Create : EntityModification.Update);
						}
						catch (Exception e)
						{
							TempData["MessageType"] = WebMessageType.Failure;
							TempData["Message"] = $"Failed to upload document, error: {e.Message}";
						}
					}
				}

				TempData["MessageType"] = WebMessageType.Success;
				TempData["Message"] = $"New script {newScript.Name} saved successfully";
			}
			catch (Exception e)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to save script {newScript.Name}, error: {e}";
				return RedirectToAction("Add");
			}

			//return to index
			return RedirectToAction("Index");
		}

		public ActionResult ConfirmDeleteScript(int id)
		{
			ScriptDto model = new ScriptDto() { Id = id };

			try
			{
				model.Name = _scriptFileController.GetScript(UserId, id).Name; //complete model details
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to retrieve script, error: {exception.Message}";
			}

			return View("ConfirmDeleteScript", model);
		}

		[HttpPost]
		public ActionResult DeleteScript(DeleteModel model)
		{
			if (!ModelState.IsValid)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = "Failed to delete, incomplete data sent";
				return RedirectToAction("ConfirmDeleteScript", new { id = model.Id });
			}

			try
			{
				_scriptFileController.ModifyScript(UserId, new ScriptDto() { Id = model.Id }, EntityModification.Delete);
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to delete scripte, error: {exception.Message}";
			}
			return RedirectToAction("Index");
		}

		/// <summary>
		/// Special case: used in service request for generating
		///                 who the SR is intended for
		/// </summary>
		/// <param name="userId">user calling the script</param>
		/// <returns></returns>
		public JsonResult GetRequestees(Guid userId)
		{
			var scriptId = ConfigHelper.GetDepartmentUsersScriptId();

			try
			{
				Guid scriptFile = _scriptFileController.GetScript(UserId, scriptId).ScriptFile;
				ScriptExecutor elScriptador = new ScriptExecutor();

				// Formatting to output the a JsonResult
				var requestees = elScriptador.ExecuteScript(userId, scriptFile);
				return Json(requestees, JsonRequestBehavior.AllowGet);

			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to retrieve requestees, error: {exception}";
				return null;
			}
		}

		/// <summary>
		/// General purpose for running scripts
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="scriptId"></param>
		/// <returns></returns>
		public JsonResult GetOptions(Guid userId, int scriptId)
		{
			Guid scriptFile;		//get the script file's name
			try
			{
				scriptFile = _scriptFileController.GetScript(UserId, scriptId).ScriptFile;
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to retrieve scripted data, error: {exception}";
				return null;
			}

			ScriptExecutor elScriptador = new ScriptExecutor();

			// Formatting to output the a JsonResult
			var requestees = elScriptador.ExecuteScript(userId, scriptFile);
			return Json(requestees, JsonRequestBehavior.AllowGet); 
		}
		
		/// <summary>
		/// Used by the ServiceRequestMaintenance ScriptedInput Views
		/// </summary>
		/// <returns></returns>
		[ChildActionOnly]
		public ActionResult GetScriptsDropDownList(int id)
		{
			var model = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Script..." } };
			var scripts = _scriptFileController.GetScripts(UserId).ToList();
			if (scripts.Any())
			{
				model.AddRange(scripts.Select(s =>
					new SelectListItem
					{
						Value = s.Id.ToString(),
						Text = s.Name.ToString(),
						Selected = s.Id == id
					}).ToList());

				model = model.OrderBy(c => c.Text).ToList();
			}
			return PartialView("PartialViews/GetScriptsDropDownList", model);
		}

	}
}