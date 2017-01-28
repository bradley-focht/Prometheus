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

            if (id == 0)
            {
                model = new ServiceDto();
            }
            else
            {
                _ps = InterfaceFactory.CreatePortfolioService(_dummyId);
                model = _ps.GetService(id);
            }

            return View(model);
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

                newService = _ps.GetService(service.Id);
                newService.BusinessValue = service.BusinessValue;
                _ps.ModifyService(newService, EntityModification.Update);
            }
            catch(Exception exception)
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
        /// <param name="serviceId"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveServiceOption(ServiceOptionAbbreviatedModel option, HttpPostedFileBase image = null)
        {
            _ps = InterfaceFactory.CreatePortfolioService(_dummyId);
            var existingOption = (ServiceOptionDto)_ps.GetServiceOption(option.Id);       //option to amend
            if (image != null)
            {    
                    if (existingOption.Picture != null)
                    {
                        var path = Path.Combine(ConfigHelper.GetOptionPictureLocation(), option.Picture.ToString());

                    try   //catch error if key is not in web.config
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
                    TempData["Message"] = $"Failed to save option, file error: {exception.Message}";
                    return RedirectToAction("UpdateServiceOption", new { id =option.Id });
                        
                }
            }
            existingOption = AbbreviatedEntityUpdate.UpdateServiceOption(option, existingOption);
            _ps.ModifyServiceOption(existingOption, EntityModification.Update);

            return RedirectToAction("ShowServiceOption", new {id = option.Id});
            
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

        public ActionResult ShowOptionCategory(int id)
        {
            var ps = InterfaceFactory.CreatePortfolioService(_dummyId);
            var service = ps.GetServiceOptionCategory(id);

            return View("ShowServiceOptionCategory", service);
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
                model.ServiceName = ps.GetService(ps.GetServiceOptionCategory(model.Option.ServiceOptionCategoryId).Id).Name;
                model.ServiceId = ps.GetService(ps.GetServiceOptionCategory(model.Option.ServiceOptionCategoryId).Id).Id;

            }
            catch (Exception exception)
            {
                TempData["MessageType"] = WebMessageType.Failure;
                TempData["Message"] = exception.Message;

                return View(model);
            }
            return View(model);
        }

        public ActionResult UpdateServiceOption(int id)
        {
            ServiceRequestOptionModel model = new ServiceRequestOptionModel();
            var ps = InterfaceFactory.CreatePortfolioService(_dummyId);
            try
            {
                model.Option = ps.GetServiceOption(id);                 //get data for back links
                model.ServiceName = ps.GetService(ps.GetServiceOptionCategory(model.Option.ServiceOptionCategoryId).Id).Name;
                model.ServiceId = ps.GetService(ps.GetServiceOptionCategory(model.Option.ServiceOptionCategoryId).Id).Id;

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
                    input = new SelectionInputDto { Delimiter = "," };  //set the default to comma
                    break;
                default:                                                //need a default
                    input = null;
                    break;
            }

            input.ServiceOptionId = id;

            var model = new UserInputModel { InputType = type, OptionId = id, OptionName = option.Name, UserInput = input };
            model.ServiceName = ps.GetService(ps.GetServiceOptionCategory(option.ServiceOptionCategoryId).Id).Name;
            model.ServiceId = ps.GetServiceOptionCategory(option.ServiceOptionCategoryId).ServiceId;

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
                    return RedirectToAction("AddUserInput", new { type = UserInputTypes.Selection, id = input.ServiceOptionId });
                return RedirectToAction("UpdateUserInput", new { type = UserInputTypes.Text, parentId = input.ServiceOptionId, id = input.Id });
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

        /// <summary>
        /// Return view to update a user input
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
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
            var option = ps.GetServiceOption(input.ServiceOptionId);
            UserInputModel model = new UserInputModel
            {
                InputType = type,
                OptionId = id,
                OptionName = option.Name,
                UserInput = input,
                ServiceId = ps.GetServiceOptionCategory(option.ServiceOptionCategoryId).ServiceId,
                ServiceName = ps.GetService(ps.GetServiceOptionCategory(option.ServiceOptionCategoryId).ServiceId).Name
            };
            return View("EditUserInput", model);
        }

        /// <summary>
        /// First step of deleting a user input
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ConfirmDeleteUserInput(UserInputTypes type, int id)
        {
            ConfirmDeleteModel model = new ConfirmDeleteModel { Type = type, Id = id };
            model.DeleteAction = "DeleteUserInput";
            model.ReturnAction = "ShowUserInput";

            var ps = InterfaceFactory.CreatePortfolioService(_dummyId);
            IUserInput input = null;
            IServiceOptionDto option = null;

            try
            {
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
                }

                if (input != null)
                {
                    option = ps.GetServiceOption(input.ServiceOptionId);
                    if (option != null)
                    {
                        model.ServiceName = ps.GetService(ps.GetServiceOptionCategory(option.ServiceOptionCategoryId).ServiceId).Name;
                        model.OptionId = option.Id;
                        model.OptionName = option.Name;
                        model.Name = input.DisplayName;
                        model.ServiceId = ps.GetServiceOptionCategory(option.ServiceOptionCategoryId).ServiceId;
                    }
                }
            }
            catch (Exception exception)
            {
                TempData["MessageType"] = WebMessageType.Failure;               //return incomplete data with an error message
                TempData["Message"] = $"Failed to find user input, error: {exception.Message}";
                return View(model);
            }


            return View(model);
        }

        /// <summary>
        /// Show details of a user input
        /// </summary>
        /// <param name="type">input type</param>
        /// <param name="id">id of input</param>
        /// <returns></returns>
        public ActionResult ShowUserInput(UserInputTypes type, int id)
        {
            var ps = InterfaceFactory.CreatePortfolioService(_dummyId);
            var model = new UserInputModel { InputType = type };
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
            model.ServiceId = ps.GetServiceOptionCategory(option.ServiceOptionCategoryId).ServiceId;
            model.ServiceName = ps.GetService(ps.GetServiceOptionCategory(option.ServiceOptionCategoryId).ServiceId).Name;
            model.OptionId = option.Id;
            model.OptionName = option.Name;
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
                    case UserInputTypes.Text:
                        ps.ModifyTextInput(new TextInputDto { Id = deleteModel.Id }, EntityModification.Delete);
                        break;
                    case UserInputTypes.ScriptedSelection:
                        ps.ModifyScriptedSelectionInput(new ScriptedSelectionInputDto { Id = deleteModel.Id }, EntityModification.Delete);
                        break;
                    case UserInputTypes.Selection:
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


            return RedirectToAction("ShowServiceOption", new { id = deleteModel.Id });
        }

    }
}