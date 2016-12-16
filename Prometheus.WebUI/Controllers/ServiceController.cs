using Common.Dto;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Models.Service;
using Prometheus.WebUI.Models.Shared;
using ServicePortfolioService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Enums;

namespace Prometheus.WebUI.Controllers
{
    //[Authorize]
    public class ServiceController : Controller
    {
        private int dummId = 0;

        /// <summary>
        /// Default page 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            return View(ps.GetServices());
        }

        /// <summary>
        /// Navigation Control, entity names are coded here
        ///   names here are used in action names
        ///   incoming section names are validated against the list, if no match then first in list is used
        ///   names should be put just as they will show up, spaces are removed, special characters will cause problems
        /// </summary>
        /// <param name="section"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult ShowNav(string section, int id = 0)
        {

            if (!ServiceSectionHelper.ValidateRoute(section))
                section = "General";

            ServiceNavModel model = new ServiceNavModel(ServiceSectionHelper.GenerateNavLinks(), section, id,
                "Show" + section);

            return PartialView("PartialViews/_ServiceNav", model);
        }

        /// <summary>
        /// Show service list
        /// </summary>
        /// <param name="section"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Show(string section, int id)
        {
            ServiceModel sm = new ServiceModel();

            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            sm.Service = ps.GetService(id);
            sm.SelectedSection = section;

            return View(sm);
        }

        /// <summary>
        /// Add a new service
        /// </summary>
        /// <returns></returns>
        public ActionResult AddService()
        {
            return View("AddService");
        }

        /// <summary>
        /// Save a new Service and then redirect to show the full SDP of the service for data entry
        /// </summary>
        /// <param name="newService"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveService(ServiceDto newService)
        {
            if (!ModelState.IsValid) /* Server side validation */
            {
                TempData["MessageType"] = WebMessageType.Failure;
                TempData["Message"] = "Failed to save service due to invalid data";
                return RedirectToAction("AddService");
            }
            //save service
            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            int newId = ps.ModifyService(newService, EntityModification.Create).Id;

            TempData["MessageType"] = WebMessageType.Success;
            TempData["Message"] = $"New service {newService.Name} saved successfully";

            //return to a vew that will let the user now add to the SDP of the service
            return RedirectToAction("Show", new {section = "General", id = newId});
        }

        /// <summary>
        /// Save new or existing swot items
        ///  if the id is 0, it is assumed to be new
        /// </summary>
        /// <param name="swotItem"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveSwotItem(ServiceSwotDto swotItem)
        {
            if (!ModelState.IsValid)
            {
                TempData["MessageType"] = WebMessageType.Failure;
                TempData["Message"] = "Failed to save SWOT item due to invalid data";
                return View("AddSectionItem");
            }

            IPortfolioService ps = InterfaceFactory.CreatePortfolioService(dummId);
            ps.ModifyServiceSwot(swotItem, swotItem.Id <= 0 ? EntityModification.Create : EntityModification.Update);

            TempData["MessageType"] = WebMessageType.Success;
            TempData["Message"] = $"Successfully saved {swotItem.Item}";
            return RedirectToAction("Show", new {section = "Swot", id = swotItem.ServiceId});
        }



        /// <summary>
        /// Save and update work units
        /// </summary>
        /// <param name="workUnit"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveWorkUnitsItem(ServiceWorkUnitDto workUnit)
        {
            if (!ModelState.IsValid) /* Server side validation */
            {
                TempData["MessageType"] = WebMessageType.Failure;
                TempData["Message"] = "Failed to save Work Unit due to invalid data";
                return RedirectToAction("UpdateServiceSectionItem", new {section = "WorkUnits", id = workUnit.ServiceId});
            }

            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            ps.ModifyServiceWorkUnit(workUnit, workUnit.Id < 1 ? EntityModification.Create : EntityModification.Update);

            TempData["MessageType"] = WebMessageType.Success;
            TempData["Message"] = $"Successfully saved {workUnit.Name}";

            return RedirectToAction("Show", new {section = "WorkUnits", id = workUnit.ServiceId});
        }

        [HttpPost]
        public ActionResult SaveServiceGoalItem(ServiceGoalDto goal)
        {
            if (!ModelState.IsValid) /* Server side validation */
            {
                TempData["MessageType"] = WebMessageType.Failure;
                TempData["Message"] = "Failed to save goal due to invalid data";
                return RedirectToAction("AddService");
            }
            //save service
            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            ps.ModifyServiceGoal(goal, EntityModification.Create);

            TempData["MessageType"] = WebMessageType.Success;
            TempData["Message"] = $"New service {goal.Name} saved successfully";

            return RedirectToAction("Show", new {id = goal.ServiceId, section = "Goals"});
        }

        /// <summary>
        /// Save a new Swot Activity
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveSwotActivityItem(SwotActivityDto activity)
        {
            if (!ModelState.IsValid)
            {
                TempData["MessageType"] = WebMessageType.Failure;
                TempData["Message"] = "Failed to save SWOT activity due to invalid data";
                return View("UpdateSwotActivityItem", new SwotActivityItemModel(activity));
            }

            IPortfolioService ps = InterfaceFactory.CreatePortfolioService(dummId);
            ps.ModifySwotActivity(activity, activity.Id <= 0 ? EntityModification.Create : EntityModification.Update);
            var activityParent = ps.GetServiceSwot(activity.ServiceSwotId);

            TempData["MessageType"] = WebMessageType.Success;
            TempData["Message"] = $"Successfully saved {activity.Name}";

            return RedirectToAction("ShowServiceSectionItem",
                new {section = "Swot", serviceId = activityParent.ServiceId, id = activity.ServiceSwotId});
        }

        [HttpPost]
        public ActionResult SaveSwotServiceMeasureItem(ServiceMeasureDto activity)
        {
            return RedirectToAction("Show");
        }

        [HttpPost]
        public ActionResult SaveContractItem(ServiceContractDto contract)
        {
            return RedirectToAction("Show");
        }

        [ChildActionOnly]
        public ActionResult ShowServiceGoals(int id)
        {
            TableDataModel tblModel = new TableDataModel
            {
                ServiceSection = "Goals",
                Controller = "Service",
                AddAction = "AddServiceSectionItem",
                ServiceId = id
            };

            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            var service = ps.GetService(id);

            if (service.ServiceGoals != null && service.ServiceGoals.Any())
            {
                tblModel.Titles = new List<string> {"Goal", "Duration", "Start Date", "End Date"};
                List<Tuple<int, ICollection<string>>> data = new List<Tuple<int, ICollection<string>>>();

                foreach (var goal in service.ServiceGoals)
                    //check for data before doing anything, if no data a "add new" message will be displayed
                {
                    data.Add(new Tuple<int, ICollection<string>>(goal.Id, new List<string>
                    {
                        goal.Name,
                        goal.Type.ToString(),
                        goal.StartDate?.ToString("d") ?? "n/a",
                        goal.EndDate?.ToString("d") ?? "n/a"
                    }));
                }
                tblModel.Data = data;
                tblModel.Action = "ShowServiceSectionItem"; //add rest of functionality if needed
                tblModel.ConfirmDeleteAction = "ConfirmDeleteServiceGoalsItem";
                tblModel.UpdateAction = "UpdateServiceSectionItem";
            }
            return PartialView("/Views/Shared/PartialViews/_TableViewer.cshtml", tblModel);
        }

        [ChildActionOnly]
        public ActionResult ShowServiceContracts(ServiceDto service)
        {
            TableDataModel tblModel = new TableDataModel();
            tblModel.Titles = new List<string> {"Vendor", "Contract Number", "Start Date", "End Date"};
            tblModel.Data = new List<Tuple<int, ICollection<string>>>
            {
                new Tuple<int, ICollection<string>>(1,
                    new List<string> {"Prometheus", "44-4507-A", "next month", "last month"})
            };
            tblModel.Action = "ShowServiceSectionItem";
            tblModel.ServiceSection = "Contracts";
            return PartialView("/Views/Shared/PartialViews/_TableViewer.cshtml", tblModel);
        }

        [ChildActionOnly]
        public ActionResult ShowServiceWorkUnits(int id)
        {
            TableDataModel tblModel = new TableDataModel
            {
                Action = "ShowServiceSectionItem",
                ServiceSection = "WorkUnits",
                Controller = "Service"
            };

            tblModel.AddAction = "AddServiceSectionItem";
            tblModel.ConfirmDeleteAction = "ConfirmDeleteWorkUnitsItem";
            tblModel.UpdateAction = "UpdateServiceSectionItem";


            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            var service = ps.GetService(id);
            var workUnits = service.ServiceWorkUnits;
            tblModel.ServiceId = service.Id;

            if (workUnits != null)
            {
                tblModel.Titles = new List<string> {"Name", "Contact"};
                tblModel.Data = new List<Tuple<int, ICollection<string>>>();
                foreach (var unit in workUnits)
                {
                    tblModel.Data.Add(new Tuple<int, ICollection<string>>(unit.Id,
                        new List<string> {unit.Name, unit.Contact}));
                }
            }

            return PartialView("/Views/Shared/PartialViews/_TableViewer.cshtml", tblModel);
        }

        [ChildActionOnly]
        public ActionResult ShowServiceMeasures(ServiceDto service)
        {
            TableDataModel tblModel = new TableDataModel();
            tblModel.Titles = new List<string> {"Method", "Outcome"};
            tblModel.Action = "ShowServiceSectionItem";

            tblModel.ServiceSection = "Measures";
            tblModel.Data = new List<Tuple<int, ICollection<string>>>
            {
                new Tuple<int, ICollection<string>>(1, new List<string> {"divide by 0", "exception"})
            };

            return PartialView("/Views/Shared/PartialViews/_TableViewer.cshtml", tblModel);
        }

        /// <summary>
        /// Build the model for displaying types of SWOT items
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult ShowServiceSwot(ServiceDto service)
        {
            SwotTableModel model = new SwotTableModel();
            model.ServiceId = service.Id;

            if (service.ServiceSwots != null)
            {
                model.Opportunities = service.ServiceSwots.Where(s => s.Type == ServiceSwotType.Opportunity);
                model.Strengths = service.ServiceSwots.Where(s => s.Type == ServiceSwotType.Strength);
                model.Threats = service.ServiceSwots.Where(s => s.Type == ServiceSwotType.Threat);
                model.Weaknesses = service.ServiceSwots.Where(s => s.Type == ServiceSwotType.Weakness);
            }
            return PartialView("PartialViews/ShowSwotTable", model);
        }


        /// <summary>
        /// Returns table view for SWOT activities for a SWOT item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult ShowSwotActivities(int id)
        {
            TableDataModel model = new TableDataModel();

            model.Action = "ShowSwotActivity";

            model.Titles = new List<string> {"Activity", "Date"};
            model.Data = new List<Tuple<int, ICollection<string>>>();
            model.UpdateAction = "UpdateSwotActivityItem";
            model.ConfirmDeleteAction = "ConfirmDeleteSwotActivityItem";

            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            var swot = ps.GetServiceSwot(id);

            foreach (var activity in swot.SwotActivities)
            {
                model.Data.Add(new Tuple<int, ICollection<string>>(activity.Id,
                    new List<string> {activity.Name, activity.Date.ToString("d")}));
            }

            return View("PartialViews/_TableViewer", model);
        }

        public ActionResult ConfirmDeleteSwotActivityItem(int id)
        {
            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            var activity = ps.GetSwotActivity(id);
            var swot = ps.GetServiceSwot(activity.ServiceSwotId);
            var model = new ConfirmDeleteSectionItemModel
            {
                Id = id,
                ServiceId = swot.ServiceId,
                DeleteAction = "DeleteSwotActivity",
                Name = activity.Name,
                AltId = swot.Id,
                AltName = swot.Item

            };
            model.Service = ps.GetService(swot.ServiceId).Name;
            model.ServiceId = swot.ServiceId;

            return View("ConfirmDeleteSwotActivityItem", model);
        }

        [HttpPost]
        public ActionResult DeleteSwotActivity(DeleteSectionItemModel model)
        {
            TempData["messageType"] = WebMessageType.Success;
            TempData["message"] = "Successfully deleted " + model.FriendlyName;

            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            int swotId = ps.GetSwotActivity(model.Id).ServiceSwotId;
            ps.ModifySwotActivity(new SwotActivityDto {Id = model.Id}, EntityModification.Delete);


            return RedirectToAction("ShowServiceSectionItem",
                new {id = swotId, serviceId = model.ServiceId, section = "Swot"});
        }



        [ChildActionOnly]
        public ActionResult ShowServiceProcesses(ServiceDto service)
        {
            TableDataModel tblModel = new TableDataModel();
            tblModel.Titles = new List<string> {"Method", "Outcome"};
            tblModel.Data = new List<Tuple<int, ICollection<string>>>
            {
                new Tuple<int, ICollection<string>>(1, new List<string> {"divide by 0", "exception"})
            };

            return PartialView("/Views/Shared/PartialViews/_TableViewer.cshtml", tblModel);
        }


        [ChildActionOnly]
        public ActionResult ShowServicePricing(ServiceDto service)
        {
            TableDataModel tblModel = new TableDataModel();
            tblModel.Titles = new List<string> {"Method", "Outcome"};
            tblModel.Data = new List<Tuple<int, ICollection<string>>>
            {
                new Tuple<int, ICollection<string>>(1, new List<string> {"divide by 0", "exception"})
            };

            return PartialView("/Views/Shared/PartialViews/_TableViewer.cshtml", tblModel);
        }

        /// <summary>
        /// Sends service to UpdateSection view for form input
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UpdateGeneral(int id)
        {
            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            ServiceSectionModel model = new ServiceSectionModel();
            model.Service = ps.GetService(id);
            model.Section = "General";

            return View("UpdateSectionItem", model);
        }

        /// <summary>
        /// Save updated Service/General information or create a new one
        ///   model is validated, redirects to the Show/General
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveGeneralItem(ServiceDto service)
        {
            if (ModelState.IsValid)
            {
                TempData["MessageType"] = WebMessageType.Failure;
                TempData["Message"] = $"{service.Name} has not been failed due to invalid data";
            }
            TempData["MessageType"] = WebMessageType.Success;
            TempData["Message"] = $"{service.Name} has been saved";

            //perform the save
            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            ps.ModifyService(service, EntityModification.Update);

            return RedirectToAction("Show", new {section = "General", id = service.Id});
        }

        [HttpPost]
        public ActionResult SaveGoalsItem(ServiceGoalDto goal)
        {
            if (!ModelState.IsValid)
            {
                TempData["MessageType"] = WebMessageType.Failure;
                TempData["Message"] = $"Unable to save {goal.Name}";
                RedirectToAction("Show", new {section = "Goals", id = goal.ServiceId});
            }

            TempData["MessageType"] = WebMessageType.Success;
            TempData["Message"] = "Sucessfully saved goal";
            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            ps.ModifyServiceGoal(goal, goal.Id < 1 ? EntityModification.Create : EntityModification.Update);

            return RedirectToAction("show", new {section = "Goals", id = goal.ServiceId});
        }

        /// <summary>
        /// Action used to show the SectionItem view that will load the specific partial view for the item required
        ///    the affiliated child actions for their corresponding partial views must follow the convention ShowService***Item
        /// </summary>
        /// <param name="section"></param>
        /// <param name="id">id of item serviceItem</param>
        /// <param name="serviceId">id of service</param>
        /// <returns></returns>
        public ActionResult ShowServiceSectionItem(int serviceId, string section, int id)
        {
            ServiceSectionModel model = new ServiceSectionModel();
            IPortfolioService ps = InterfaceFactory.CreatePortfolioService(dummId);

            model.Service = ps.GetService(serviceId);

            model.Section = section;
            model.SectionItemId = id;

            if (ServiceSectionHelper.ParentSection(section) != null)
            {
                model.ParentName = ServiceSectionHelper.ParentSection(section);
                //   model.SectionItemParentId =
            }

            return View("ShowSectionItem", model);
        }


        /// <summary>
        /// Show the service section and other service data is availalbe
        /// </summary>
        /// <param name="section"></param>
        /// <param name="serviceId"></param>
        /// <param name="id">service id</param>
        /// <returns></returns>
        public ActionResult UpdateServiceSectionItem(string section, int serviceId, int id)
        {
            ServiceSectionModel model = new ServiceSectionModel();
            IPortfolioService ps = InterfaceFactory.CreatePortfolioService(dummId);
            model.Section = section;
            model.SectionItemId = id;
            model.Service = ps.GetService(serviceId);
            return View("UpdateSectionItem", model);
        }

        /// <summary>
        /// Update a Swot Item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult UpdateSwotItem(int id)
        {
            IPortfolioService ps = InterfaceFactory.CreatePortfolioService(dummId);

            return View("PartialViews/UpdateSwotItem", ps.GetServiceSwot(id));
        }

        public ActionResult UpdateSwotActivityItem(int id)
        {
            IPortfolioService ps = InterfaceFactory.CreatePortfolioService(dummId);
            var model = new SwotActivityItemModel(new SwotActivityDto {ServiceSwotId = id});
            ISwotActivityDto swotActivity = ps.GetSwotActivity(id);
            var swotItem = ps.GetServiceSwot(swotActivity.ServiceSwotId);
            model.SwotName = swotItem.Item;
            model.ServiceId = swotItem.ServiceId;
            model.ServiceName = ps.GetService(swotItem.ServiceId).Name;
            model.SwotActivity = swotActivity;

            return View("UpdateSwotActivityItem", model);
        }

        public ActionResult AddSwotActivityItem(int id)
        {

            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            var swot = ps.GetServiceSwot(id);

            var model = new SwotActivityItemModel(new SwotActivityDto {ServiceSwotId = id});
            model.ServiceName = ps.GetService(swot.ServiceId).Name;
            model.ServiceId = swot.ServiceId;
            model.Action = "Add";
            model.SwotName = swot.Item;

            return View("UpdateSwotActivityItem", model);
        }

        /// <summary>
        /// General Add new ServiceSectionItem
        /// </summary>
        /// <param name="section"></param>
        /// <param name="id"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public ActionResult AddServiceSectionItem(string section, int id, int parentId = 0)
        {
            var model = new ServiceSectionModel();
            model.Section = section;

            IPortfolioService ps = InterfaceFactory.CreatePortfolioService(dummId);
            model.Service = ps.GetService(id);


            return View("AddSectionItem", model);
        }

        /// <summary>
        /// Generates the confirm deletion warning page
        /// </summary>
        /// <param name="id">Id of ServiceGoal</param>
        /// <returns></returns>
        public ActionResult ConfirmDeleteServiceGoalsItem(int id)
        {
            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            var goal = ps.GetServiceGoal(id);
            var model = new ConfirmDeleteSectionItemModel
            {
                Id = id,
                ServiceId = goal.ServiceId,
                DeleteAction = "DeleteServiceGoal",
                Name = goal.Name,
                Section = "Goals"

            };
            model.Service = ps.GetService(goal.ServiceId).Name;
            model.ServiceId = goal.ServiceId;

            return View("ConfirmDeleteSection", model);
        }

        /// <summary>
        /// Confirmation before deleting a SWOT item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ConfirmDeleteServiceSwotItem(int id)
        {
            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            var item = ps.GetServiceSwot(id);
            var model = new ConfirmDeleteSectionItemModel
            {
                Id = id,
                ServiceId = item.ServiceId,
                DeleteAction = "DeleteServiceSwotItem",
                Name = item.Item,
                Section = "Swot"
            };
            model.Service = ps.GetService(item.ServiceId).Name;
            return View("ConfirmDeleteSection", model);
        }

        /// <summary>
        /// Completes the deletion from ConfirmDeleteServiceSwotItem
        /// </summary>
        /// <param name="model">item to delete plus information for redirection</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteServiceSwotItem(DeleteSectionItemModel model)
        {
            TempData["messageType"] = WebMessageType.Success;
            TempData["message"] = "Successfully deleted " + model.FriendlyName;

            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            ps.ModifyServiceSwot(new ServiceSwotDto {Id = model.Id}, EntityModification.Delete);

            return RedirectToAction("Show", new {id = model.ServiceId, section = "Swot"});
        }


        /// <summary>
        /// Complete deletion of a ServiceGoal
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteServiceGoal(DeleteSectionItemModel model)
        {
            TempData["messageType"] = WebMessageType.Success;
            TempData["message"] = "Successfully deleted " + model.FriendlyName;
            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            ps.ModifyServiceGoal(new ServiceGoalDto {Id = model.Id}, EntityModification.Delete);

            return RedirectToAction("Show", new {id = model.ServiceId, section = "Goals"});
        }

        [HttpPost]
        public ActionResult DeleteServiceWorkUnit(DeleteSectionItemModel model)
        {
            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            try
            {
                ps.ModifyServiceGoal(new ServiceGoalDto {Id = model.Id}, EntityModification.Delete);
            }
            catch (Exception e)
            {
                TempData["messageType"] = WebMessageType.Failure;
                TempData["message"] = $"Failed to delete {model.FriendlyName}: {e.Message}";

                return RedirectToAction("Show", new { id = model.ServiceId, section = "WorkUnits" });
            }

            TempData["messageType"] = WebMessageType.Success;
            TempData["message"] = "Successfully deleted " + model.FriendlyName;

            return RedirectToAction("Show", new { id = model.ServiceId, section = "WorkUnits" });
        }



        #region Service Documents

        /// <summary>
        /// Upload and save files if they are present. Always redirects to the Show action.
        ///   File location is taken from the FilePath key in Web.config. 
        ///   Ensure web server is running with sufficient permissions to that folder location
        ///   Error messages are put into TempData[]
        /// </summary>
        /// <param name="file"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadServiceDocument(HttpPostedFileBase file, int id = 0)
        {
            if (Request.Files.Count > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                if (fileName != null)
                {
                    Guid newFileName = Guid.NewGuid(); //to rename document

                    var path = Path.Combine(ConfigurationManager.AppSettings["FilePath"], newFileName.ToString());
                        //file path location comes from the Web.config file

                    file.SaveAs(Server.MapPath(path));
                    var ps = InterfaceFactory.CreatePortfolioService(dummId);

                    ps.ModifyServiceDocument(new ServiceDocumentDto
                    {
                        ServiceId = id,
                        Filename = Path.GetFileNameWithoutExtension(fileName),
                        StorageNameGuid = newFileName,
                        FileExtension = Path.GetExtension(fileName)
                    }, EntityModification.Create);
                }
            }
            return RedirectToAction("Show", new {id, section = "Documents"});
        }

        /// <summary>
        /// Rename the document
        /// </summary>
        /// <param name="document">Storage name of documentId</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveDocumentsItem(ServiceDocumentDto document)
        {
            if (!ModelState.IsValid)
            {
                TempData["MessageType"] = WebMessageType.Failure;
                TempData["Message"] = $"Failed to save document {document.Filename}";
                return RedirectToAction("UpdateServiceDocument", new {id = document.StorageNameGuid});
            }
            //perform the save
            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            var doc = ps.GetServiceDocument(document.StorageNameGuid);
            doc.Filename = document.Filename;
            ps.ModifyServiceDocument(doc, EntityModification.Update);

            TempData["MessageType"] = WebMessageType.Success;
            TempData["Message"] = $"Successfully saved document {document.Filename}";

            return RedirectToAction("Show", new {section = "Documents", id = document.ServiceId});
        }

        /// <summary>
        /// Update (Rename) Service Documents
        ///  They do not follow convention
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UpdateServiceDocument(Guid id)
        {
            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            var doc = ps.GetServiceDocument(id);
            var service = ps.GetService(doc.ServiceId);

            ServiceSectionModel md = new ServiceSectionModel
            {
                Service = service,
                SectionItemId = doc.Id,
                SectionItemGuid = doc.StorageNameGuid,
                Section = "Documents"
            };

            return View("UpdateSectionItem", md);
        }

        /// <summary>
        /// Serves the file with its Filename, FileExtension and MIME type to the browser
        /// </summary>
        /// <param name="id">Use file's storage name</param>
        /// <returns></returns>
        public FileResult DownloadServiceDocument(Guid id)
        {

            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            var doc = ps.GetServiceDocument(id);

            Response.AddHeader("Content-Disposition", @"filename=" + doc.Filename + doc.FileExtension);

            var path = Path.Combine(ConfigurationManager.AppSettings["FilePath"], id.ToString());

            return new FilePathResult(path, MimeMapping.GetMimeMapping(path));
        }

        /// <summary>
        /// Delete a document from the file system and from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteServiceDocument(Guid id)
        {
            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            var file = ps.GetServiceDocument(id);

            ps.ModifyServiceDocument(ps.GetServiceDocument(id), EntityModification.Delete);

            //don't forget to delete the document in the file system
            var path = Path.Combine(ConfigurationManager.AppSettings["FilePath"], id.ToString());
            try
            {
                System.IO.File.Delete(path);
            }
            catch (Exception e)
            {
                TempData["MessageType"] = WebMessageType.Failure;
                TempData["Message"] = $"Failed to delete: {e.Message}";
                return RedirectToAction("Show", new {id = file.ServiceId});
            }

            TempData["MessageType"] = WebMessageType.Success;
            TempData["Message"] = $"Sucessfully deleted file {file.Filename}{file.FileExtension}";

            return RedirectToAction("Show", new { id = file.ServiceId });
        }

        /// <summary>
        /// Document deletion confirmation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ConfirmDeleteServiceDocument(Guid id)
        {
            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            var document = ps.GetServiceDocument(id);
            var service = ps.GetService(document.ServiceId);

            var model = new ConfirmDeleteSectionItemModel
            {
                DeleteAction = "DeleteServiceDocument",
                Id = document.Id,
                ServiceId = document.ServiceId,
                Section = "Documents",
                Service = service.Name,
                Name = document.Filename
            };

            return View("ConfirmDeleteSection", model);
        }

        #endregion

        #region Lists

        /// <summary>
        /// Return a few for Lifecycle Statuses
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult ShowLifecycleStatuses(int selectedId = 0)
        {
            LifecycleStatusesModel model = new LifecycleStatusesModel();
            model.SelectedStatus = selectedId;

            var ps = InterfaceFactory.CreatePortfolioService(dummId);

            model.LifecycleStatuses = ps.GetLifecycleStatusNames();

            return View("PartialViews/ShowLifeCycleStatuses", model);
        }

        /// <summary>
        /// Return a view for a list of Service Bundles
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult ShowServiceBundles(int selectedId = 0)
        {
            ServiceBundleModel model = new ServiceBundleModel();
            model.SelectedServiceBundle = selectedId;

            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            model.ServiceBundles = ps.GetServiceBundleNames();

            return View("PartialViews/ShowServiceBundles", model);
        }

        /// <summary>
        /// Builds the partial view with selected item
        ///    actions are assumed to follow Add - Show - Update - Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult ShowServiceList(int id = 0)
        {
            var ps = InterfaceFactory.CreatePortfolioService(dummId);

            //create the model 
            LinkListModel servicesModel = new LinkListModel
            {
                AddAction = "AddService",
                SelectAction = "Show/General",
                Controller = "Service",
                Title = "Services",
                SelectedItemId = id,
                ListItems = ps.GetServiceNames()
            };

            return PartialView("PartialViews/_LinkList", servicesModel);
        }

        #endregion

        /// <summary>
        /// Create the model to show the Swot Activity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ShowSwotActivity(int id)
        {
            var ps = InterfaceFactory.CreatePortfolioService(id);
            var model = new SwotActivityItemModel((SwotActivityDto) ps.GetSwotActivity(id));
            var swot = ps.GetServiceSwot(model.SwotId);
            model.SwotName = swot.Item;
            model.SwotId = swot.Id;
            model.ServiceId = swot.ServiceId;
            model.ServiceName = ps.GetService(swot.ServiceId).Name;

            return PartialView("ShowSwotActivityItem", model);
        }


        public ActionResult ConfirmDeleteWorkUnitsItem(int id)
        {
            var ps = InterfaceFactory.CreatePortfolioService(dummId);
            var item = ps.GetServiceWorkUnit(id);
            var model = new ConfirmDeleteSectionItemModel
            {
                Id = id,
                ServiceId = item.ServiceId,
                DeleteAction = "DeleteServiceWorkUnit",
                Name = item.Name,
                Section = "WorkUnits"
            };
            model.Service = ps.GetService(item.ServiceId).Name;
            return View("ConfirmDeleteSection", model);
        }
    }
}