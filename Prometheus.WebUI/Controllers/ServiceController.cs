using Common.Dto;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Models.Service;
using Prometheus.WebUI.Models.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Common.Enums;
using ServicePortfolioService;
using ServicePortfolioService.Controllers;

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
            var ps = new PortfolioService(dummId, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
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

			ServiceNavModel model = new ServiceNavModel(ServiceSectionHelper.GenerateNavLinks(), section, id, "Show" + section);

			return PartialView("PartialViews/_ServiceNav", model);
		}

		/// <summary>
		/// Show service list
		/// </summary>
		/// <param name="section"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult Show(string section = "", int id = 0)
		{
			ServiceModel sm = new ServiceModel();

			if (id == 0)
			{
				sm.Service = new ServiceDto() { Id = 0 };
			}
			else
			{
                var ps = new PortfolioService(dummId, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
                sm = new ServiceModel(new ServiceDto() { Id = id, Name = "Support Services" }, section.Replace(" ", ""));
			    sm.Service = ps.GetService(id);
			}

			return View(sm);
		}

		/// <summary>
		/// Add a new service, this is unique from the add sections and the form is restricted to the general section only
		/// </summary>
		/// <returns></returns>
		public ActionResult Add()
		{
			return View("AddService");
		}

		/// <summary>
		/// Save a new Service and then redirect to show the full SDP of the service for data entry
		/// </summary>
		/// <param name="newService"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Save(ServiceDto newService)
		{
		    if (!ModelState.IsValid)                    /* Server side validation */
		    {
                TempData["MessageType"] = WebMessageType.Failure;
                TempData["Message"] = "Failed to save service due to invalid data";
                return RedirectToAction("Add");
            }
            //save service
            PortfolioService ps = new PortfolioService(dummId, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
		    int newId = ps.SaveService(newService).Id;

            TempData["MessageType"] = WebMessageType.Success;
            TempData["Message"] = $"New service {newService.Name} saved successfully";

            //return to a vew that will let the user now add to the SDP of the service
            return RedirectToAction("Show", new { section = "General", id = newId});
		}


	    [HttpPost]
	    public ActionResult SaveSwotItem(ServiceSwotDto swotItem)
	    {
	        return RedirectToAction("Show", new {section = "Swot", id = swotItem.ServiceId});
	    }


		/// <summary>
		/// Save and update work units
		/// </summary>
		/// <param name="workUnit"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SaveServiceWorkUnitItem(ServiceWorkUnitDto workUnit)
		{
			return RedirectToAction("Show");
		}

		[HttpPost]
		public ActionResult SaveServiceGoalItem(ServiceGoalDto goal)
		{
			return RedirectToAction("Show");
		}

		[HttpPost]
		public ActionResult SaveServiceSwotItem(ServiceSwotDto swotItem)
		{
			return RedirectToAction("Show");
		}

		[HttpPost]
		public ActionResult SaveSwotActivityItem(SwotActivityDto activity)
		{
			return RedirectToAction("Show");
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
		public ActionResult ShowServiceGoals(ServiceDto service)
		{
			TableDataModel tblModel = new TableDataModel();
			tblModel.Titles = new List<string> { "Goal", "Duration", "Start Date", "End Date" };
			tblModel.Data = new List<Tuple<int, IEnumerable<string>>>
			{
				new Tuple<int, IEnumerable<string>>(1,
					new List<string> {"test the system", "short term", "september", "october"}),
				new Tuple<int, IEnumerable<string>>(1,
					new List<string> {"add actual data", "short term", "october", "march"})
			};
			tblModel.Action = "ShowServiceSectionItem";
			tblModel.ServiceSection = "Goals";
			tblModel.Controller = "Service";

			return PartialView("/Views/Shared/PartialViews/_TableViewer.cshtml", tblModel);
		}

		[ChildActionOnly]
		public ActionResult ShowServiceContracts(ServiceDto service)
		{
			TableDataModel tblModel = new TableDataModel();
			tblModel.Titles = new List<string> { "Vendor", "Contract Number", "Start Date", "End Date" };
			tblModel.Data = new List<Tuple<int, IEnumerable<string>>>
			{
				new Tuple<int, IEnumerable<string>>(1,
					new List<string> {"Prometheus", "44-4507-A", "next month", "last month"})
			};
			tblModel.Action = "ShowServiceSectionItem";
			tblModel.ServiceSection = "Contracts";
			return PartialView("/Views/Shared/PartialViews/_TableViewer.cshtml", tblModel);
		}

		[ChildActionOnly]
		public ActionResult ShowServiceWorkUnits(ServiceDto service)
		{
			TableDataModel tblModel = new TableDataModel();
			tblModel.Titles = new List<string> { "Work Unit", "Manager", "Roles" };
			tblModel.Action = "ShowServiceSectionItem";
			tblModel.ServiceSection = "WorkUnits";
			tblModel.Controller = "Service";


			tblModel.Data = new List<Tuple<int, IEnumerable<string>>>
			{
				new Tuple<int, IEnumerable<string>>(1,
					new List<string> {"Executive", "Sean Boczulak", "be da boss"})
			};

			return PartialView("/Views/Shared/PartialViews/_TableViewer.cshtml", tblModel);
		}

		[ChildActionOnly]
		public ActionResult ShowServiceMeasures(ServiceDto service)
		{
			TableDataModel tblModel = new TableDataModel();
			tblModel.Titles = new List<string> { "Method", "Outcome" };
			tblModel.Action = "ShowServiceSectionItem";

			tblModel.ServiceSection = "Measures";
			tblModel.Data = new List<Tuple<int, IEnumerable<string>>>
			{
				new Tuple<int, IEnumerable<string>>(1, new List<string> {"divide by 0", "exception"})
			};

			return PartialView("/Views/Shared/PartialViews/_TableViewer.cshtml", tblModel);
		}


		[ChildActionOnly]
		public ActionResult ShowServiceSwot(ServiceDto service)
		{
			SwotTableModel model = new SwotTableModel();

			model.Opportunities = new List<IServiceSwotDto> { new ServiceSwotDto { Id = 1, Item = "Room for more beers" } };
			model.ServiceId = 1;

			return PartialView("PartialViews/ShowSwotTable", model);
		}

		[ChildActionOnly]
		public ActionResult ShowSwotActivities(ICollection<ServiceSwotDto> activities)
		{
			TableDataModel model = new TableDataModel();
			model.Action = "ShowServiceSectionItem";
		    model.ServiceSection = "SwotActivity";
			model.Titles = new List<string> { "Item", "Date" };
			model.Data = new List<Tuple<int, IEnumerable<string>>> { new Tuple<int, IEnumerable<string>>(1, new List<string> { "find love on campus" }) };

			return View("PartialViews/_TableViewer", model);
		}


		[ChildActionOnly]
		public ActionResult ShowServiceProcesses(ServiceDto service)
		{
			TableDataModel tblModel = new TableDataModel();
			tblModel.Titles = new List<string> { "Method", "Outcome" };
			tblModel.Data = new List<Tuple<int, IEnumerable<string>>>
			{
				new Tuple<int, IEnumerable<string>>(1, new List<string> {"divide by 0", "exception"})
			};

			return PartialView("/Views/Shared/PartialViews/_TableViewer.cshtml", tblModel);
		}


		[ChildActionOnly]
		public ActionResult ShowServicePricing(ServiceDto service)
		{
			TableDataModel tblModel = new TableDataModel();
			tblModel.Titles = new List<string> { "Method", "Outcome" };
			tblModel.Data = new List<Tuple<int, IEnumerable<string>>>
			{
				new Tuple<int, IEnumerable<string>>(1, new List<string> {"divide by 0", "exception"})
			};

			return PartialView("/Views/Shared/PartialViews/_TableViewer.cshtml", tblModel);
		}

		/// <summary>
		/// Sends service to UpdateSection view for form input
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult UpdateGeneral(int id = 0)
		{
			if (id == 0)
			{
				return RedirectToAction("Show");
			}

            var ps = new PortfolioService(dummId, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
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
            var ps = new PortfolioService(dummId, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
		    ps.SaveService(service);

            return RedirectToAction("Show", new { section = "General", id = service.Id });
		}

		[HttpPost]
		public ActionResult SaveGoalsItem(ServiceGoalDto goal)
		{
			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = "Sucessfully saved goal";

			return RedirectToAction("show", new { section = "Goals", id = 10 });
		}

		/// <summary>
		/// Action used to show the SectionItem view that will load the specific partial view for the item required
		///    the affiliated child actions for their corresponding partial views must follow the convention ShowService***Item
		/// </summary>
		/// <param name="section"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult ShowServiceSectionItem(string section, int id = 0)
		{
			ServiceSectionModel model = new ServiceSectionModel();
		    SwotActivityDto activity = new SwotActivityDto {Id = 10, Name = "Test item", Date = DateTime.Now};

            model.Section = section;
			model.Service = new ServiceDto();
			model.Service.Name = "Support Services";
			model.Service.Id = id;
			model.SectionItemId = id;
			model.Service.ServiceGoals = new List<ServiceGoalDto> { new ServiceGoalDto() { Id = 1, ServiceId = 10, Description = "some new goal goes here", Name = "new goal to acheive", StartDate = DateTime.Now, EndDate = DateTime.Now} }.ToArray();
			model.Service.ServiceWorkUnits = new List<IServiceWorkUnitDto>(new List<IServiceWorkUnitDto> { new ServiceWorkUnitDto { Id = 1, WorkUnit = "some team", Contact = "a manager", Responsibilities = "keep out of trouble" } });
			model.Service.ServiceContracts = new List<IServiceContractDto>(new List<IServiceContractDto>());
		    model.Service.ServiceSwots = new List<IServiceSwotDto>();
            model.Service.ServiceSwots.Add(new ServiceSwotDto {Id = 1, Description = "new test", Item = "new test", SwotActivities = new List<ISwotActivityDto>(
                new List<ISwotActivityDto>(new List<ISwotActivityDto> {new SwotActivityDto {Id = 1, Name = "Test activity", ServiceSwotId = 1, Date = DateTime.Now} }))});
            
		       
            
			model.Service.ServiceMeasures = new List<IServiceMeasureDto>();

			return View("ShowSectionItem", model);
		}


		public ActionResult UpdateServiceSectionItem(string section, int id = 0)
		{
			ServiceSectionModel model = new ServiceSectionModel();
			model.Section = section;
			model.Service = new ServiceDto();
			model.Service.Name = "Support Services";
			model.Service.Id = 10;
			model.Service.ServiceGoals = new List<ServiceGoalDto> { new ServiceGoalDto() { Description = "some new goal goes here", Name = "new goal" } }.ToArray();
			return View("UpdateSectionItem", model);
		}

		public ActionResult UpdateSwotItem(int id)
		{
			return View("PartialViews/UpdateSwotItem");
		}

		public ActionResult AddServiceSectionItem(string section, int id = 0)
		{
		    if (id == 0)
		        return RedirectToAction("Show");

            var model = new ServiceSectionModel();
            model.Section = section;

            var ps = new PortfolioService(dummId, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());

		    model.Service = ps.GetService(id);

			return View("AddSectionItem", model);
		}

	    /// <summary>
	    /// Generates the confirm deletion warning page
	    ///  function is generalized enough to handle all section items
	    ///  deletion is specialized and has specialized actions to complete
	    /// </summary>
	    /// <param name="id"></param>
	    /// <returns></returns>
	    public ActionResult ConfirmDeleteServiceGoalsItem(int id = 0)
		{
            var model = new ConfirmDeleteSectionItemModel();
			

			return View("ConfirmDeleteSection", model);
		}

		[HttpPost]
		public ActionResult DeleteServiceGoalsItem(DeleteSectionItemModel model)
		{
			TempData["messageType"] = WebMessageType.Success;
			TempData["message"] = "successfully deleted " + model.FriendlyName;

			return RedirectToAction("Show", new { id = model.Serviceid, section = model.Section });
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
					Guid newFileName = Guid.NewGuid();                              //to rename document

                    var path = Path.Combine(ConfigurationManager.AppSettings["FilePath"], newFileName.ToString());  //file path location comes from the Web.config file

					file.SaveAs(Server.MapPath(path));
                    var ps = new PortfolioService(dummId, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
				    ps.SaveServiceDocument(new ServiceDocumentDto
				    {
				        ServiceId = id,
                        Filename =  Path.GetFileNameWithoutExtension(fileName),
                        StorageNameGuid = newFileName,
                        FileExtension = Path.GetExtension(fileName)
				    });
				}
			}
			return RedirectToAction("Show", new { id, section = "Documents" });
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
            var ps = new PortfolioService(dummId, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
            ps.SaveServiceDocument(document);

            TempData["MessageType"] = WebMessageType.Success;
            TempData["Message"] = $"Successfully saved document {document.Filename}";

            return RedirectToAction("Show", new { section = "Documents", id = document.ServiceId });
		}

        /// <summary>
        /// Update (Rename) Service Documents
        ///  They do not follow convention
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
	    public ActionResult UpdateServiceDocument(Guid id)
	    {
            var ps = new PortfolioService(dummId, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
            var doc = ps.GetServiceDocument(id);
	        var service = ps.GetService(doc.ServiceId);

	        ServiceSectionModel md = new ServiceSectionModel {Service = service, SectionItemId = doc.Id, SectionItemGuid = doc.StorageNameGuid, Section = "Documents"};

            return View("UpdateSectionItem", md);
	    }

        /// <summary>
        /// Serves the file with its Filename, FileExtension and MIME type to the browser
        /// </summary>
        /// <param name="id">Use file's storage name</param>
        /// <returns></returns>
        public FileResult DownloadServiceDocument(Guid id)
		{

            var ps = new PortfolioService(dummId, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
		    var doc = ps.GetServiceDocument(id);

			Response.AddHeader("Content-Disposition", @"filename=" + doc.Filename + doc.FileExtension);

			var path = Path.Combine(ConfigurationManager.AppSettings["FilePath"], id.ToString());

			return new FilePathResult(path, MimeMapping.GetMimeMapping(path));
		}

        [HttpPost]
	    public ActionResult DeleteServiceDocument(Guid id)
	    {
            //don't forget to delete the document in the file system

	        return RedirectToAction("Show");
	    }

        public ActionResult ConfirmDeleteServiceDocument(Guid id)
        {
            var sps = new PortfolioService(dummId, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
            var document = sps.GetServiceDocument(id);
            var service = sps.GetService(document.ServiceId);

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
            
            IPortfolioService sps = new PortfolioService(dummId, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());

            model.LifecycleStatuses = sps.GetLifecycleStatusNames();

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

	        var sps = new PortfolioService(dummId, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
            model.ServiceBundles = sps.GetServiceBundleNames();

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
            var ps = new PortfolioService(dummId, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());

            //create the model 
            LinkListModel servicesModel = new LinkListModel
            {
                AddAction = "Add",
                SelectAction = "Show/General",
                Controller = "Service",
                Title = "Services",
                SelectedItemId = id,
                ListItems = ps.GetServiceNames()
            };

            return PartialView("PartialViews/_LinkList", servicesModel);
        }
        #endregion
    }
}