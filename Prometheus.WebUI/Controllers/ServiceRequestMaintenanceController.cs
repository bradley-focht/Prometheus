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
            ServiceRequestPackageDto srp = new ServiceRequestPackageDto();
            return View(srp);
        }

        /// <summary>
        /// Return view to update a package
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UpdatePackage(int id)
        {
            return View();
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

        public ActionResult ConfirmDeletePackage(int id)
        {
            return View();
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
                    TempData["Message"] = $"Failed to save file, error: {exception.Message}";
                    return RedirectToAction("UpdateServiceOption", new { id =option.Id });                     
                }
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
                return RedirectToAction("ShowServiceOption", new { id = option.Id });
            }

            return RedirectToAction("ShowServiceOption", new {id = option.Id});
            
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
                var existingCategory = (ServiceOptionCategoryDto) _ps.GetServiceOptionCategory(category.Id);//category to amend
                existingCategory = AbbreviatedEntityUpdate.UpdateServiceCategory(category, existingCategory);
                _ps.ModifyServiceOptionCategory(existingCategory, EntityModification.Update);
            }
            catch (Exception exception)
            {
                TempData["MessageType"] = WebMessageType.Failure;
                TempData["Message"] = $"Failed to save category, error: {exception.Message}";
                return RedirectToAction("UpdateServiceCategory", new { id = category.Id });
            }
            return RedirectToAction("ShowOptionCategory", new {id = category.Id});
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
                Controller = "ServiceRequestMaintenanceController",
                Title = "Packages",
                SelectAction = "ShowServicePackages",
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

        public ActionResult Savepackage(PackageModel package)
        {
            if (!ModelState.IsValid)
            {
                TempData["MessageType"] = WebMessageType.Failure;
                TempData["Message"] = "Failed to retrieve service package due to invalid data";
                if (package.Id > 0)
                {
                    return RedirectToAction("AddPackage");
                }
                return RedirectToAction("UpdatePackage", new {id = package.Id});
            }

            _ps = InterfaceFactory.CreatePortfolioService(_dummyId);
            IServiceRequestPackageDto newPackage = new ServiceRequestPackageDto();      //transfer data to new package
            newPackage.Name = package.Name;
            newPackage.ServiceOptionCategories = new List<IServiceOptionCategoryDto>();
            foreach (var category in package.Associations)
            {
                newPackage.ServiceOptionCategories.Add(new ServiceOptionCategoryDto {Id = category});
            }
            try
            {
                _ps.ModifyServiceRequestPackage(newPackage, EntityModification.Create);
            }
            catch (Exception exception)
            {
                TempData["MessageType"] = WebMessageType.Failure;
                TempData["Message"] = $"Failed to save service package, error: {exception.Message}";
            }


            return RedirectToAction("ShowPackages", new {id = 0});
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
        public ActionResult AddUserInput(UserInputTypes type)
        {
            var ps = InterfaceFactory.CreatePortfolioService(_dummyId);
            UserInputModel model = new UserInputModel();
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
                    return RedirectToAction("AddUserInput", new { type = UserInputTypes.Selection });
                return RedirectToAction("UpdateUserInput", new { type = UserInputTypes.Text, id = input.Id });
            }

            var ps = InterfaceFactory.CreatePortfolioService(_dummyId);
            int entityId = 0;           //returning id
            try
            {
                entityId = ps.ModifySelectionInput(input, input.Id > 0 ? EntityModification.Update : EntityModification.Create).Id;
            }
            catch (Exception exception)
            {
                TempData["MessageType"] = WebMessageType.Failure;
                TempData["Message"] = $"Failed to save new User Input, error: {exception.Message}";
                if (input.Id == 0)                              //depending on user action at the time
                    return RedirectToAction("AddUserInput", new { type = UserInputTypes.Selection });
                return RedirectToAction("UpdateUserInput", new { type = UserInputTypes.Selection, id = input.Id });
            }
            TempData["MessageType"] = WebMessageType.Success;
            TempData["Message"] = "Successfully saved new User Input";

            return RedirectToAction("ShowUserInput", new {id = entityId, type = UserInputTypes.Selection});
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
                    return RedirectToAction("AddUserInput", new { type = UserInputTypes.ScriptedSelection});
                return RedirectToAction("UpdateUserInput", new { type = UserInputTypes.ScriptedSelection, id = input.Id });
            }

            var ps = InterfaceFactory.CreatePortfolioService(_dummyId);
            int entityId = 0;               //get returning id
            try
            {
                entityId = ps.ModifyScriptedSelectionInput(input, input.Id > 0 ? EntityModification.Update : EntityModification.Create).Id;
            }
            catch (Exception exception)
            {
                TempData["MessageType"] = WebMessageType.Failure;
                TempData["Message"] = $"Failed to save new User Input, error: {exception.Message}";
                if (input.Id == 0)                              //depending on user action at the time
                    return RedirectToAction("AddUserInput", new { type = UserInputTypes.ScriptedSelection });
                return RedirectToAction("UpdateUserInput", new { type = UserInputTypes.ScriptedSelection, id = input.Id });
            }
            TempData["MessageType"] = WebMessageType.Success;
            TempData["Message"] = "Successfully saved new User Input";

            return RedirectToAction("ShowUserInput", new {id = entityId, type = UserInputTypes.ScriptedSelection});
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
                    return RedirectToAction("AddUserInput", new { type = UserInputTypes.Text });
                return RedirectToAction("UpdateUserInput", new { type = UserInputTypes.Text, id = input.Id });
            }

            var ps = InterfaceFactory.CreatePortfolioService(_dummyId);
            int entityId = 0;         //new id of entity if not existing
            try
            {
                entityId = ps.ModifyTextInput(input, input.Id > 0 ? EntityModification.Update : EntityModification.Create).Id;
            }
            catch (Exception exception)
            {
                TempData["MessageType"] = WebMessageType.Failure;
                TempData["Message"] = $"Failed to save new User Input, error: {exception.Message}";
                if (input.Id == 0)                              //depending on user action at the time
                    return RedirectToAction("AddUserInput", new { type = UserInputTypes.Text });
                return RedirectToAction("UpdateUserInput", new { type = UserInputTypes.Text, id = input.Id });
            }
            TempData["MessageType"] = WebMessageType.Success;
            TempData["Message"] = "Successfully saved new User Input";

            return RedirectToAction("ShowUserInput", new {id = entityId, UserInputTypes.Text});
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
        public ActionResult ConfirmDeleteUserInput(UserInputTypes type, int id)
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

            }
            catch (Exception exception)
            {
                TempData["MessageType"] = WebMessageType.Failure;               //return incomplete data with an error message
                TempData["Message"] = $"Failed to find user input, error: {exception.Message}";
                return View(model);
            }
            model.Name = input.DisplayName;


            return View(model);
        }

        /// <summary>
        /// Returns a link list of user inputs
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult GetUserInputs(UserInputTypes type = UserInputTypes.Text, int id = 0)
        {
            UserInputsLinkListModel itemList = new UserInputsLinkListModel {SelectedInputId = id, SelectedInputType = type, Action="ShowUserInput"};
            _ps = InterfaceFactory.CreatePortfolioService(_dummyId);

            List<Tuple<UserInputTypes, int, string>> items = new List<Tuple<UserInputTypes, int, string>>();
            try
            {
                if (_ps.GetSelectionInputs() != null )
                items.AddRange(from s in _ps.GetTextInputs() select new Tuple<UserInputTypes, int, string>(UserInputTypes.Text, s.Id, s.DisplayName));
                items.AddRange(from s in _ps.GetSelectionInputs() select new Tuple<UserInputTypes, int, string>(UserInputTypes.Selection, s.Id, s.DisplayName));
            }
            catch (Exception exception)
            {
                TempData["MessageType"] = WebMessageType.Failure;
                TempData["Message"] = $"Failed retrieving user inputs {exception.Message}";
            }
            
            itemList.Items = items.OrderBy(t=>t.Item3);

            return View("PartialViews/UserInputsLinkList", itemList);
        }


        /// <summary>
        /// Show details of a user input
        /// </summary>
        /// <param name="type">input type</param>
        /// <param name="id">id of input</param>
        /// <returns></returns>
        public ActionResult ShowUserInput(UserInputTypes type = UserInputTypes.Text, int id = 0)
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
                }
                catch(Exception exception)
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


            return RedirectToAction("ShowUserInput", new { id = 0 });
        }

    }
}