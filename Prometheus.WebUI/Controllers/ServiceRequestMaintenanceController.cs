using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common.Dto;
using Common.Enums;
using Common.Enums.Entities;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Models.ServiceRequestMaintenance;
using Prometheus.WebUI.Models.Shared;
using RequestService.Controllers;

namespace Prometheus.WebUI.Controllers
{
	public class ServiceRequestMaintenanceController : Controller
	{
		private int _dummyId = 1;
		private readonly ICatalogController _requestService;

		/// <summary>
		/// Default constructor
		/// </summary>
		public ServiceRequestMaintenanceController()
		{
			_requestService = new CatalogController(InterfaceFactory.CreateUserManagerService());
		}

		// GET: ServiceRequestMaintenance
		public ActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// Generate a link list of service names with filtering control
		/// </summary>
		/// <param name="catalog"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public PartialViewResult GetServiceNames(string catalog = "Both", int id = 0)
		{
			var services = new List<Tuple<int, string>>();
			if (catalog == "Business" || catalog == "Both")
			{
				services.AddRange(from s in _requestService.RequestBusinessCatalog(_dummyId)
								  select new Tuple<int, string>(s.Id, s.Name));
			}

			if (catalog == "Support" || catalog == "Both")
			{
				services.AddRange(from s in _requestService.RequestSupportCatalog(_dummyId)
								  select new Tuple<int, string>(s.Id, s.Name));
			}
			LinkListModel model = new LinkListModel { SelectedItemId = id, ListItems = services };
			return PartialView(model);
		}

		public ActionResult ShowServiceOptions(int id)
		{
			var ps = InterfaceFactory.CreatePortfolioService(_dummyId);
			var service = ps.GetService(id);

			return View("Index", service);
		}

		/// <summary>
		/// Show a service option and editor functions
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult ShowServiceOption(int id)
		{
			ServiceRequestOptionModel model = new ServiceRequestOptionModel();
			try
			{
				model.Option = InterfaceFactory.CreatePortfolioService(_dummyId).GetServiceOption(id);
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = exception.Message;
				return View(model);
			}
			var inputs = new List<IUserInput>();						//sort user inputs
			if (model.Option.TextInputs != null) {
				inputs.AddRange(from t in model.Option.TextInputs select (IUserInput)t);
			}
			if (model.Option.SelectionInputs != null) {
				inputs.AddRange(from t in model.Option.SelectionInputs select (IUserInput)t);
			}
			if (model.Option.ScriptedSelecentionInputs != null) {
				inputs.AddRange(from t in model.Option.ScriptedSelecentionInputs select (IUserInput)t);
			}
			model.UserInputs = inputs.OrderBy(i => i.DisplayName);
			

			return View(model);
		}

		/// <summary>
		/// Returns View to add form of corresponding type
		/// </summary>
		/// <param name="type"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult AddUserInput(UserInputTypes type, int id)
		{
			var ps = InterfaceFactory.CreatePortfolioService(_dummyId);
			var option = ps.GetServiceOption(id);
			IUserInput input;
			switch (type)
			{
				case UserInputTypes.Text:
					input = new TextInputDto();
					break;
					case UserInputTypes.ScriptedSelection:
					input = new ScriptedSelectionInputDto();
					break;
					case UserInputTypes.Selection:
					input = new SelectionInputDto();
					break;
				default:									//need a default
					input = null;
					break;
			}

			input.ServiceOptionId = id;

			return View("EditUserInput", new UserInputModel { InputType = type, OptionId = id, OptionName = option.Name, UserInput = input});
		}

		/// <summary>
		/// Save new or updates to existing text user inputs
		/// </summary>
		/// <param name="input">input dto</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SaveSelectionInput(SelectionInputDto input)
		{
			if (!ModelState.IsValid)							//server side validation
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = "Failed to save new User Input due to invalid data";
				if (input.Id == 0)								//depending on user action at the time
					return RedirectToAction("AddUserInput", new { type = UserInputTypes.Selection, id = input.ServiceOptionId});
				return RedirectToAction("UpdateUserInput", new {type = UserInputTypes.Text, parentId = input.ServiceOptionId, id = input.Id});
			}

			var ps = InterfaceFactory.CreatePortfolioService(_dummyId);
			
			try
			{
				ps.ModifySelectionInput(input, input.Id > 0 ? EntityModification.Update : EntityModification.Create);
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to save new User Input, error: {exception.Message}";
				if (input.Id == 0)                              //depending on user action at the time
					return RedirectToAction("AddUserInput", new { type = UserInputTypes.Selection, id = input.ServiceOptionId });
				return RedirectToAction("UpdateUserInput", new { type = UserInputTypes.Selection, id = input.Id });
			}
			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = "Successfully saved new User Input";

			return RedirectToAction("ShowServiceOption", new { id = input.ServiceOptionId });
		}

		/// <summary>
		/// Save new or updates to existing text user inputs
		/// </summary>
		/// <param name="input">input dto</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SaveScriptedSelectionInput(ScriptedSelectionInputDto input)
		{
			if (!ModelState.IsValid)                            //server side validation
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = "Failed to save new User Input due to invalid data";
				if (input.Id == 0)                              //depending on user action at the time
					return RedirectToAction("AddUserInput", new { type = UserInputTypes.ScriptedSelection, id = input.ServiceOptionId });
				return RedirectToAction("UpdateUserInput", new { type = UserInputTypes.ScriptedSelection, parentId = input.ServiceOptionId, id = input.Id });
			}

			var ps = InterfaceFactory.CreatePortfolioService(_dummyId);

			try
			{
				ps.ModifyScriptedSelectionInput(input, input.Id > 0 ? EntityModification.Update : EntityModification.Create);
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to save new User Input, error: {exception.Message}";
				if (input.Id == 0)                              //depending on user action at the time
					return RedirectToAction("AddUserInput", new { type = UserInputTypes.ScriptedSelection, id = input.ServiceOptionId });
				return RedirectToAction("UpdateUserInput", new { type = UserInputTypes.ScriptedSelection, id = input.Id });
			}
			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = "Successfully saved new User Input";

			return RedirectToAction("ShowServiceOption", new { id = input.ServiceOptionId });
		}

		/// <summary>
		/// Save new or updates to existing text user inputs
		/// </summary>
		/// <param name="input">input dto</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SaveTextInput(TextInputDto input)
		{
			if (!ModelState.IsValid)                            //server side validation
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = "Failed to save new User Input due to invalid data";
				if (input.Id == 0)                              //depending on user action at the time
					return RedirectToAction("AddUserInput", new { type = UserInputTypes.Text, id = input.ServiceOptionId });
				return RedirectToAction("UpdateUserInput", new { type = UserInputTypes.Text, parentId = input.ServiceOptionId, id = input.Id });
			}

			var ps = InterfaceFactory.CreatePortfolioService(_dummyId);

			try
			{
				ps.ModifyTextInput(input, input.Id > 0 ? EntityModification.Update : EntityModification.Create);
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to save new User Input, error: {exception.Message}";
				if (input.Id == 0)                              //depending on user action at the time
					return RedirectToAction("AddUserInput", new { type = UserInputTypes.Text, id = input.ServiceOptionId });
				return RedirectToAction("UpdateUserInput", new { type = UserInputTypes.Text, id = input.Id });
			}
			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = "Successfully saved new User Input";

			return RedirectToAction("ShowServiceOption", new { id = input.ServiceOptionId });
		}

		public ActionResult UpdateUserInput(UserInputTypes type, int id)
		{
			var ps = InterfaceFactory.CreatePortfolioService(_dummyId);
			
			IUserInput input;

				switch (type)
				{
					case UserInputTypes.Text:
						input = ps.GetTextInput(id);
						break;
					case UserInputTypes.Selection:
						input = ps.GetSelectionInput(id);
						break;
					case UserInputTypes.ScriptedSelection:
						input = ps.GetScriptedSelectionInput(id);
						
						break;
					default: //need a default
						input = new TextInputDto();
						break;
				}
				//input.ServiceOptionId = id;
			string optionName = ps.GetServiceOption(input.ServiceOptionId).Name;

			return View("EditUserInput", new UserInputModel { InputType = type, OptionId = id, OptionName = optionName, UserInput = input });
		}

		public ActionResult ConfirmDeleteUserInput(UserInputTypes inputType, int id)
		{
			ConfirmDeleteModel model = new ConfirmDeleteModel();
			return View();
		}

		public ActionResult ShowUserInput(UserInputTypes type, int id)
		{
			var ps = InterfaceFactory.CreatePortfolioService(_dummyId);
			var model = new UserInputModel {InputType = type};
			IUserInput input;

			switch (type)
			{
				case UserInputTypes.Text:
					input = ps.GetTextInput(id);
					break;
				case UserInputTypes.ScriptedSelection:
					input = ps.GetScriptedSelectionInput(id);
					break;
				case UserInputTypes.Selection:
					input = ps.GetSelectionInput(id);

					break;
				default: //need a default
					input = new TextInputDto();
					break;
			}
			//input.ServiceOptionId = id;
			var option = ps.GetServiceOption(input.ServiceOptionId);
			model.OptionId = option.Id;
			model.OptionName = option.Name;
			model.UserInput = input;

			return View("ShowUserInput", model);
		}

	}
}