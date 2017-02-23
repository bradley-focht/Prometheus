using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Dto;
using Common.Enums;
using Common.Enums.Entities;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Models.ServiceRequestMaintenance;
using Prometheus.WebUI.Models.Shared;
using RequestService.Controllers;
using ServicePortfolioService;

namespace Prometheus.WebUI.Controllers
{
	public class ServiceRequestMaintenanceController : Controller
	{
		private int _dummyId = 1;
		private readonly ICatalogController _requestService;
		private IPortfolioService _ps;

		/// <summary>
		/// Default constructor
		/// </summary>
		public ServiceRequestMaintenanceController()
		{
			_requestService = new CatalogController(InterfaceFactory.CreateUserManagerService());
		}

		/// <summary>
		/// Tasks page
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// Entry point for Service Catalog Entries data
		/// </summary>
		/// <returns></returns>
		public ActionResult ShowServices(int id = 0)
		{
			IServiceDto model;

			if (id == 0) { model = new ServiceDto(); }  //return no selected item
			else
			{
				_ps = InterfaceFactory.CreatePortfolioService(_dummyId);
				model = _ps.GetService(id);
			}

			return View(model);
		}

		/// <summary>
		/// Return view of packages
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult ShowPackages(int id = 0)
		{
			_ps = InterfaceFactory.CreatePortfolioService(_dummyId);

			ServiceRequestPackageDto srp = null;        //determine if one is selected or not
			if (id > 0)
			{
				try
				{
					srp = (ServiceRequestPackageDto)_ps.GetServiceRequestPackage(id);
				}
				catch (Exception exception)
				{
					TempData["MessageType"] = WebMessageType.Failure;
					TempData["Message"] = $"Failed to retrieve service package, error: {exception.Message}";
				}
			}

			if (srp == null)                       //send an empty object if nothing selected, razor will handle
			{
				srp = new ServiceRequestPackageDto();
			}
			return View(srp);
		}

		/// <summary>
		/// Return view to update a package
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult UpdatePackage(int id)
		{
			_ps = InterfaceFactory.CreatePortfolioService(_dummyId);
			PackageModel model = new PackageModel();

			try		//try and get a package
			{
				var package = (ServiceRequestPackageDto)_ps.GetServiceRequestPackage(id);
				model.Name = package.Name;
				//TODO: add in selected packages
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to retrieve service package, error: {exception.Message}";
			}

			try //build services list to select from
			{
				var serviceController = InterfaceFactory.CreateCatalogController(_dummyId);
				var services = serviceController.RequestBusinessCatalog(_dummyId).ToList(); //build list
				services.AddRange(serviceController.RequestSupportCatalog(_dummyId));

				model.Services = services;
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"failed to obtain services, error: {exception.Message}";
			}
			return View(model);
		}

		/// <summary>
		/// Editor to add a new package
		/// </summary>
		/// <returns></returns>
		public ActionResult AddPackage()
		{
			PackageModel model = new PackageModel();
			try
			{
				var serviceController = InterfaceFactory.CreateCatalogController(_dummyId);
				var services = serviceController.RequestBusinessCatalog(_dummyId).ToList(); //build list
				services.AddRange(serviceController.RequestSupportCatalog(_dummyId));

				model.Services = services;
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"failed to obtain services, error: {exception.Message}";
			}

			return View(model);
		}

		/// <summary>
		/// confirm delete a package
		/// </summary>
		/// <param name="id">package id</param>
		/// <returns></returns>
		public ActionResult ConfirmDeletePackage(int id)
		{
			ConfirmDeleteModel model = new ConfirmDeleteModel {Id = id, DeleteAction = "DeletePackage", ReturnAction = "ShowPackage"};
			_ps = InterfaceFactory.CreatePortfolioService(_dummyId);
			try
			{
				model.Name = _ps.GetServiceRequestPackage(id).Name; //complete model details
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to retrieve service request package, error: {exception.Message}";
			}
			
			return View("ConfirmDeletePackage", model);
		}
		[HttpPost]
		public ActionResult DeletePackage(DeleteModel model)
		{
			if (!ModelState.IsValid)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = "Failed to delete, incomplete data sent";
				return RedirectToAction("ConfirmDeletePackage", new {id = model.Id});
			}
			
			try
			{
				_ps = InterfaceFactory.CreatePortfolioService(_dummyId);
				_ps.ModifyServiceRequestPackage(new ServiceRequestPackageDto {Id = model.Id}, EntityModification.Delete);
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to delete service request package, error: {exception.Message}";
			}
			return RedirectToAction("ShowPackages");
		}

		/// <summary>
		/// Editor to update changes to Service
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult UpdateService(int id)
		{
			IServiceDto model;
			try
			{
				_ps = InterfaceFactory.CreatePortfolioService(_dummyId);
				model = _ps.GetService(id);
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to retrieve servce {exception.Message}";
				model = new ServiceDto();
			}


			return View("UpdateService", model);
		}

		/// <summary>
		/// Save changes to service
		/// </summary>
		/// <param name="service"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SaveService(ServiceAbbreviatedModel service)
		{
			IServiceDto newService;
			if (!ModelState.IsValid)            //validate model
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = "Failed to save changes to service";
			}

			try                                 //update state
			{
				_ps = InterfaceFactory.CreatePortfolioService(_dummyId);

				newService = AbbreviatedEntityUpdate.UpdateService(service, _ps.GetService(service.Id));    //preserve service design documentation
				_ps.ModifyService(newService, EntityModification.Update);
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to save changes to service, error: {exception.Message}";
				newService = new ServiceDto();
				return View("ShowServices", newService);
			}

			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = "Successfully saved service";

			return View("ShowServices", newService);
		}

		/// <summary>
		/// Save updates to an option
		/// </summary>
		/// <param name="option"></param>
		/// <param name="userInputs"></param>
		/// <param name="image"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SaveServiceOption(ServiceOptionAbbreviatedModel option, ICollection<string> userInputs = null, HttpPostedFileBase image = null)
		{	/*there is way too much code in this controller */
			_ps = InterfaceFactory.CreatePortfolioService(_dummyId);
			var existingOption = (ServiceOptionDto)_ps.GetServiceOption(option.Id);       //option to amend
			if (image != null)
			{
				if (existingOption.Picture != null) /* deal with previous picture by deleting it */
				{
					var path = Path.Combine(ConfigHelper.GetOptionPictureLocation(), option.Picture.ToString());

					try //catch error if key is not in web.config
					{
						System.IO.File.Delete(Server.MapPath(path));
					}
					catch (Exception exception)
					{
						TempData["MessageType"] = WebMessageType.Failure; //unable to delete, exit at this point
						TempData["Message"] = $"Failed to delete existing file, error: {exception.Message}";
						return RedirectToAction("UpdateServiceOption", new {id = option.Id});
					}
				}

				option.PictureMimeType = image.ContentType; //rename file to a guid and store original file type
				option.Picture = Guid.NewGuid();

				try
				{
					var path = Path.Combine(ConfigurationManager.AppSettings["OptionPicsPath"],
						option.Picture.ToString()); //save file
					image.SaveAs(Server.MapPath(path));
				}
				catch (Exception exception)
				{
					TempData["MessageType"] = WebMessageType.Failure;
					TempData["Message"] = $"Failed to save file, error: {exception.Message}";
					return RedirectToAction("UpdateServiceOption", new {id = option.Id});
				}
			}
			else //preserve picture data for now
			{
				if (option.Id > 0)
				{
					var tempOption = _ps.GetServiceOption(option.Id);
					option.Picture = tempOption.Picture;
					option.PictureMimeType = tempOption.PictureMimeType;
				}
			}
			/*end of dealing with pictures */
			/* deal with user inputs */
			_ps.RemoveInputsFromServiceOption(option.Id, _ps.GetInputsForServiceOptions(new[] {new ServiceOptionDto {Id = option.Id} }));
			if (userInputs != null)
			{
				var inputGroup = UserInputHelper.MakeInputGroup(userInputs);
				_ps.AddInputsToServiceOption(option.Id, inputGroup);
			}
			try
				{
					existingOption = AbbreviatedEntityUpdate.UpdateServiceOption(option, existingOption);
					_ps.ModifyServiceOption(existingOption, EntityModification.Update);
				}
				catch (Exception exception)
				{
					TempData["MessageType"] = WebMessageType.Failure;
					TempData["Message"] = $"Failed to save option, error: {exception.Message}";
					return RedirectToAction("ShowServiceOption", new {id = option.Id});
				}
			
			return RedirectToAction("ShowServiceOption", new { id = option.Id });

		}

		/// <summary>
		/// Save updates to category
		/// </summary>
		/// <param name="category"></param>
		/// <returns></returns>
		public ActionResult SaveServiceCategory(ServiceCategoryAbbreviatedModel category)
		{
			_ps = InterfaceFactory.CreatePortfolioService(_dummyId);
			try
			{
				var existingCategory = (ServiceOptionCategoryDto)_ps.GetServiceOptionCategory(category.Id);//category to amend
				existingCategory = AbbreviatedEntityUpdate.UpdateServiceCategory(category, existingCategory);
				_ps.ModifyServiceOptionCategory(existingCategory, EntityModification.Update);
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to save category, error: {exception.Message}";
				return RedirectToAction("UpdateServiceCategory", new { id = category.Id });
			}
			return RedirectToAction("ShowOptionCategory", new { id = category.Id });
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
			return PartialView("PartialViews/GetServiceNames", model);
		}

		/// <summary>
		/// Create list of packages
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[ChildActionOnly]
		public ActionResult GetPackageNames(int id)
		{
			_ps = InterfaceFactory.CreatePortfolioService(_dummyId);
			LinkListModel model = new LinkListModel
			{
				AddAction = "AddPackage",
				Controller = "ServiceRequestMaintenance",
				Title = "Packages",
				SelectedItemId = id,
				SelectAction = "ShowPackages",
			};
			IEnumerable<Tuple<int, string>> items = null;
			try
			{
				items = from s in _ps.AllServiceRequestPackages
						select new Tuple<int, string>(s.Id, s.Name);
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to retrieve service packages {exception.Message}";
			}
			model.ListItems = items;
			return View("PartialViews/_LinkList", model);
		}

		/// <summary>
		/// Save changes to existing or new packages
		/// </summary>
		/// <param name="package">package details</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SavePackage(PackageModel package)
		{
			if (!ModelState.IsValid)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = "Failed to retrieve service package due to invalid data";
				if (package.Id > 0)
				{
					return RedirectToAction("AddPackage");
				}
				return RedirectToAction("UpdatePackage", new { id = package.Id });
			}

			_ps = InterfaceFactory.CreatePortfolioService(_dummyId);
			IServiceRequestPackageDto newPackage = new ServiceRequestPackageDto();      //transfer data to new package
			newPackage.Name = package.Name;
			newPackage.ServiceOptionCategoryTags = new List<IServiceOptionCategoryTagDto>();
			newPackage.Id = package.Id;

			int i = 1;		//used to order the category tags in a package
			foreach (var category in package.Associations)      //build new package
			{
				newPackage.ServiceOptionCategoryTags.Add(new ServiceOptionCategoryTagDto { ServiceOptionCategoryId = category, Order = i});
				i++;
			}
			try
			{
				_ps.ModifyServiceRequestPackage(newPackage, newPackage.Id > 0 ? EntityModification.Update : EntityModification.Create);
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to save service package, error: {exception.Message}";
			}

			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = "Successfully saved package";

			return RedirectToAction("ShowPackages", new { id = 0 });
		}

		/// <summary>
		/// show details of an option category
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult ShowOptionCategory(int id)
		{
			var model = new ServiceRequestCategoryModel();
			var ps = InterfaceFactory.CreatePortfolioService(_dummyId);

			try
			{
				model.Category = ps.GetServiceOptionCategory(id);
				model.ServiceId = model.Category.ServiceId;
				model.ServiceName = ps.GetService(model.ServiceId).Name;
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to retrieve category {exception.Message}";
			}
			return View("ShowServiceOptionCategory", model);
		}

		/// <summary>
		/// Show a service option and editor functions
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult ShowServiceOption(int id)
		{
			ServiceRequestOptionModel model = new ServiceRequestOptionModel();
			var ps = InterfaceFactory.CreatePortfolioService(_dummyId);
			try
			{
				model.Option = ps.GetServiceOption(id);                 //get data for back links
				model.ServiceName = ps.GetService(ps.GetServiceOptionCategory(model.Option.ServiceOptionCategoryId).ServiceId).Name;
				model.ServiceId = ps.GetServiceOptionCategory(model.Option.ServiceOptionCategoryId).ServiceId;

			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = exception.Message;

				return View(model);
			}
			return View(model);
		}

		/// <summary>
		/// update service option view
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult UpdateServiceOption(int id)
		{
			ServiceRequestOptionModel model = new ServiceRequestOptionModel();
			var ps = InterfaceFactory.CreatePortfolioService(_dummyId);
			try
			{
				model.Option = ps.GetServiceOption(id);                 //get data for back links
				model.ServiceName = ps.GetService(ps.GetServiceOptionCategory(model.Option.ServiceOptionCategoryId).ServiceId).Name;
				model.ServiceId = ps.GetServiceOptionCategory(model.Option.ServiceOptionCategoryId).ServiceId;

			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = exception.Message;
			}
			return View(model);
		}

		/// <summary>
		/// Update catalog attributes of a category
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult UpdateServiceCategory(int id)
		{
			ServiceRequestCategoryModel model = new ServiceRequestCategoryModel();
			var ps = InterfaceFactory.CreatePortfolioService(_dummyId);
			try
			{
				model.Category = ps.GetServiceOptionCategory(id);                 //get data for back links
				model.ServiceName = ps.GetService(model.Category.ServiceId).Name;
				model.ServiceId = model.Category.ServiceId;

			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = exception.Message;
			}
			return View(model);
		}


		/// <summary>
		/// Returns View to add form of corresponding type
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public ActionResult AddUserInput(UserInputType type)
		{
			UserInputModel model = new UserInputModel();
			IUserInput input;
			switch (type)
			{
				case UserInputType.Text:
					input = new TextInputDto();
					break;
				case UserInputType.ScriptedSelection:
					input = new ScriptedSelectionInputDto();
					break;
				case UserInputType.Selection:
					input = new SelectionInputDto { Delimiter = "," };  //set the default to comma
					break;
				default:                                                //need a default
					input = null;                                       //null is ok, razor will handle
					break;
			}

			model.InputType = type;
			model.UserInput = input;

			return View("EditUserInput", model);
		}

		/// <summary>
		/// Save new or updates to existing text user inputs
		/// </summary>
		/// <param name="input">input dto</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SaveSelectionInput(SelectionInputDto input)
		{
			if (!ModelState.IsValid)                            //server side validation
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = "Failed to save new User Input due to invalid data";
				if (input.Id == 0)                              //depending on user action at the time
					return RedirectToAction("AddUserInput", new { type = UserInputType.Selection });
				return RedirectToAction("UpdateUserInput", new { type = UserInputType.Text, id = input.Id });
			}

			var ps = InterfaceFactory.CreatePortfolioService(_dummyId);
			int entityId;           //returning id
				try
			{
				if (input.Id == 0)
				{
					input.Delimiter = ConfigHelper.GetDelimiter();
				}
				entityId = ps.ModifySelectionInput(input, input.Id > 0 ? EntityModification.Update : EntityModification.Create).Id;
			}
			catch (Exception exception)
			{

				if (input.Id == 0)                              //depending on user action at the time
					return RedirectToAction("AddUserInput", new { type = UserInputType.Selection });
				return RedirectToAction("UpdateUserInput", new { type = UserInputType.Selection, id = input.Id });
			}
			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = "Successfully saved new User Input";

			return RedirectToAction("ShowUserInput", new { id = entityId, type = UserInputType.Selection });
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
					return RedirectToAction("AddUserInput", new { type = UserInputType.ScriptedSelection });
				return RedirectToAction("UpdateUserInput", new { type = UserInputType.ScriptedSelection, id = input.Id });
			}

			var ps = InterfaceFactory.CreatePortfolioService(_dummyId);
			int entityId;               //get returning id
			try
			{
				entityId = ps.ModifyScriptedSelectionInput(input, input.Id > 0 ? EntityModification.Update : EntityModification.Create).Id;
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to save new User Input, error: {exception.Message}";
				if (input.Id == 0)                              //depending on user action at the time
					return RedirectToAction("AddUserInput", new { type = UserInputType.ScriptedSelection });
				return RedirectToAction("UpdateUserInput", new { type = UserInputType.ScriptedSelection, id = input.Id });
			}
			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = "Successfully saved new User Input";

			return RedirectToAction("ShowUserInput", new { id = entityId, type = UserInputType.ScriptedSelection });
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
					return RedirectToAction("AddUserInput", new { type = UserInputType.Text });
				return RedirectToAction("UpdateUserInput", new { type = UserInputType.Text, id = input.Id });
			}

			var ps = InterfaceFactory.CreatePortfolioService(_dummyId);
			int entityId;         //new id of entity if not existing
			try
			{
				entityId = ps.ModifyTextInput(input, input.Id > 0 ? EntityModification.Update : EntityModification.Create).Id;
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to save new User Input, error: {exception.Message}";
				if (input.Id == 0)                              //depending on user action at the time
					return RedirectToAction("AddUserInput", new { type = UserInputType.Text });
				return RedirectToAction("UpdateUserInput", new { type = UserInputType.Text, id = input.Id });
			}
			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = "Successfully saved new User Input";

			return RedirectToAction("ShowUserInput", new { id = entityId, UserInputType.Text });
		}

		/// <summary>
		/// Return view to update a user input
		/// </summary>
		/// <param name="type"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult UpdateUserInput(UserInputType type, int id)
		{
			var ps = InterfaceFactory.CreatePortfolioService(_dummyId);

			IUserInput input;

			switch (type)
			{
				case UserInputType.Text:
					input = ps.GetTextInput(id);
					break;
				case UserInputType.Selection:
					input = ps.GetSelectionInput(id);
					break;
				case UserInputType.ScriptedSelection:
					input = ps.GetScriptedSelectionInput(id);

					break;
				default: //need a default
					input = new TextInputDto();
					break;
			}
			//input.ServiceOptionId = id;
			UserInputModel model = new UserInputModel
			{
				InputType = type,
				UserInput = input
			};
			return View("EditUserInput", model);
		}

		/// <summary>
		/// First step of deleting a user input
		/// </summary>
		/// <param name="type"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult ConfirmDeleteUserInput(UserInputType type, int id)
		{
			ConfirmDeleteModel model = new ConfirmDeleteModel { Type = type, Id = id };
			model.DeleteAction = "DeleteUserInput";
			model.ReturnAction = "ShowUserInput";

			var ps = InterfaceFactory.CreatePortfolioService(_dummyId);
			IUserInput input = null;

			try
			{
				switch (type)
				{
					case UserInputType.Text:
						input = ps.GetTextInput(id);
						break;
					case UserInputType.Selection:
						input = ps.GetSelectionInput(id);
						break;
					case UserInputType.ScriptedSelection:
						input = ps.GetScriptedSelectionInput(id);
						break;
				}
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;               //return incomplete data with an error message
				TempData["Message"] = $"Failed to find user input, error: {exception.Message}";
				return View(model);
			}
			if (input != null) model.Name = input.DisplayName;

			return View(model);
		}

		/// <summary>
		/// Returns a link list of user inputs
		/// </summary>
		/// <param name="type"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		[ChildActionOnly]
		public ActionResult GetUserInputs(UserInputType type = UserInputType.Text, int id = 0)
		{
			UserInputsLinkListModel itemList = new UserInputsLinkListModel { SelectedInputId = id, SelectedInputType = type, Action = "ShowUserInput" };
			_ps = InterfaceFactory.CreatePortfolioService(_dummyId);

			List<Tuple<UserInputType, int, string>> items = new List<Tuple<UserInputType, int, string>>();    //for the model

			try
			{
				var textInputs = _ps.GetTextInputs();                                   //store temporarily to check for nulls after
				var selectionInputs = _ps.GetSelectionInputs();
				var scriptedInputs = _ps.GetScriptedSelectionInputs();
				if (textInputs != null)                                                //
					items.AddRange(from s in textInputs select new Tuple<UserInputType, int, string>(UserInputType.Text, s.Id, s.DisplayName));
				if (textInputs != null)
					items.AddRange(from s in selectionInputs select new Tuple<UserInputType, int, string>(UserInputType.Selection, s.Id, s.DisplayName));
				if (textInputs != null)
					items.AddRange(from s in scriptedInputs select new Tuple<UserInputType, int, string>(UserInputType.ScriptedSelection, s.Id, s.DisplayName));
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed retrieving user inputs {exception.Message}";
			}

			itemList.Items = items.OrderBy(t => t.Item3);

			return View("PartialViews/UserInputsLinkList", itemList);
		}


		/// <summary>
		/// Show details of a user input
		/// </summary>
		/// <param name="type">input type</param>
		/// <param name="id">id of input</param>
		/// <returns></returns>
		public ActionResult ShowUserInput(UserInputType type = UserInputType.Text, int id = 0)
		{
			var ps = InterfaceFactory.CreatePortfolioService(_dummyId);
			var model = new UserInputModel { InputType = type };
			IUserInput input;

			if (id > 0)
			{
				try
				{
					switch (type)
					{
						case UserInputType.Text:
							input = ps.GetTextInput(id);
							break;
						case UserInputType.ScriptedSelection:
							input = ps.GetScriptedSelectionInput(id);
							break;
						case UserInputType.Selection:
							input = ps.GetSelectionInput(id);

							break;
						default: //need a default
							input = new TextInputDto();
							break;
					}
				}
				catch (Exception exception)
				{
					TempData["MessageType"] = WebMessageType.Failure;
					TempData["Message"] = $"Failed to retreive user input, error: {exception.Message}";
					input = new TextInputDto();     //some default where id = 0
				}
			}
			else
			{
				input = new TextInputDto();     //some default where id = 0
			}
			//input.ServiceOptionId = id;
			model.UserInput = input;

			return View("ShowUserInput", model);
		}

		/// <summary>
		/// Complete deltion process of a user input
		/// </summary>
		/// <param name="deleteModel"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult DeleteUserInput(ConfirmDeleteModel deleteModel)
		{
			var ps = InterfaceFactory.CreatePortfolioService(_dummyId);
			try
			{
				switch (deleteModel.Type)
				{
					case UserInputType.Text:
						ps.ModifyTextInput(new TextInputDto { Id = deleteModel.Id }, EntityModification.Delete);
						break;
					case UserInputType.ScriptedSelection:
						ps.ModifyScriptedSelectionInput(new ScriptedSelectionInputDto { Id = deleteModel.Id }, EntityModification.Delete);
						break;
					case UserInputType.Selection:
						ps.ModifySelectionInput(new SelectionInputDto { Id = deleteModel.Id }, EntityModification.Delete);
						break;
				}
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to delete {deleteModel.Name}, error: {exception.Message}";
			}
			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = "Successfully deleted user input";


			return RedirectToAction("ShowUserInput", new { id = 0 });
		}
		
		public ActionResult GetOptionUserInputsDropDown(int id, bool readOnly=false)
		{
			_ps = InterfaceFactory.CreatePortfolioService(_dummyId);
			List<SelectListItem> inputDropDownList = new List<SelectListItem>();
			List<IUserInput> inputs = new List<IUserInput>();

			var textInputs = _ps.GetTextInputs();       //store temporarily to check for nulls after
			var selectionInputs = _ps.GetSelectionInputs();
			var scriptedInputs = _ps.GetScriptedSelectionInputs();

			if (textInputs != null)
				inputs.AddRange(from  i in textInputs select (IUserInput)i);
			if (selectionInputs != null)
				inputs.AddRange(from i in selectionInputs select (IUserInput)i);
			if(scriptedInputs != null)
				inputs.AddRange(from i in scriptedInputs select (IUserInput)i);
			inputs = inputs.OrderByDescending(i => i.DisplayName).ToList();         //sort all the data by display name
																					//need to find already selected items
			//user inputs for this user input
			IInputGroupDto optionInputs = _ps.GetInputsForServiceOptions(new List<IServiceOptionDto> {new ServiceOptionDto {Id = id}});

			//now convert it to a select list for the drop down list

			foreach (var input in inputs)
			{
				var selected = false;		//drop down list selection
				string type = null;
				if (input is TextInputDto)
				{
					type = "textInput";
					if (optionInputs.TextInputs.Any() && (from i in optionInputs.TextInputs where i.Id == input.Id select true).FirstOrDefault())
						selected = true;
				}
				else if (input is SelectionInputDto)
				{
					type = "selectionInput";
					if (optionInputs.SelectionInputs.Any() && (from i in optionInputs.SelectionInputs where i.Id == input.Id select true).FirstOrDefault())
						selected = true;
				}
				else if (input is ScriptedSelectionInputDto)
				{
					type = "scriptedSelectionInput";
					if (optionInputs.ScriptedSelectionInputs.Any() && (from i in optionInputs.ScriptedSelectionInputs where i.Id == input.Id select true).FirstOrDefault())
						selected = true;
				}

				inputDropDownList.Add(new SelectListItem
				{
					Value = $"{type}_{input.Id}",
					Text = input.DisplayName, 
					Selected = selected
				});
			}		
			return View("OptionUserInputsDropDown", inputDropDownList);
		}

		public ActionResult GetOptionUserInputsText(int id)
		{
			_ps = InterfaceFactory.CreatePortfolioService(_dummyId);
			var model = _ps.GetInputsForServiceOptions(new List<IServiceOptionDto> {new ServiceOptionDto {Id = id}});

			return View(model);
		}

	}
}