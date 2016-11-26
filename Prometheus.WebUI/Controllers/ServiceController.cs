using Common.Dto;
using Common.Enums;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Models.Service;
using Prometheus.WebUI.Models.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;
using ServicePortfolioService;
using ServicePortfolioService.Controllers;

namespace Prometheus.WebUI.Controllers
{
	public class ServiceController : Controller
	{
	    private int dummId = 0;
		/// <summary>
		/// Default page 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult Index(int id = 0)
		{
			var services = new List<ServiceDto>
			{
				new ServiceDto {Name = "Support Services", Description = "support for the masses"},
				new ServiceDto
				{
					Id = 10,
					Name = "Communications and Messaging",
					Description = "send a message to that special someone"
				},
				new ServiceDto {Id = 10, Name="Collaboration Services", Description = "this is really just Sharepoint... and it's an outdated version too."}
			};


			return View(services);
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
			/* I am here for the looks, remove me */
			LinkListModel servicesModel = new LinkListModel();
			servicesModel.SelectedItemId = id;
			servicesModel.ListItems = new List<Tuple<int, string>>
			{
				new Tuple<int, string>(10, "Support Services")
			};
			servicesModel.AddAction = "Add";
			servicesModel.SelectAction = "Show/General";
			servicesModel.Controller = "Service";
			servicesModel.Title = "Services";

			return PartialView("PartialViews/_LinkList", servicesModel);
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
				sm = new ServiceModel(new ServiceDto() { Id = id, Name = "Support Services" }, section.Replace(" ", ""));
				sm.Service.Name = "Support Services";
				sm.Service.Id = 10;
				sm.Service.ServiceOwner = "A person";
				sm.Service.Description = "This is quite the service. It lets you do a lot of things. <ul><li>it functions</li><li>it sometimes stop functioning</li></ul>";
                sm.Service.ServiceTypeRole = ServiceTypeRole.Business;
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
                TempData["messageType"] = WebMessageType.Failure;
                TempData["message"] = "Failed to save service due to invalid data";
                return RedirectToAction("Add");
            }
            //save service
            PortfolioService ps = new PortfolioService(dummId, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
		    int newId = ps.SaveService(newService).Id;

            TempData["messageType"] = WebMessageType.Success;
            TempData["message"] = $"New service {newService.Name} saved successfully";

            //return to a vew that will let the user now add to the SDP of the service
            return RedirectToAction("Show", new { section = "General", id = newId});
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
			model.ServiceId = 10;

			return PartialView("PartialViews/ShowSwotTable", model);
		}

		[ChildActionOnly]
		public ActionResult ShowSwotActivities(ICollection<ServiceSwotDto> activities)
		{
			TableDataModel model = new TableDataModel();
			model.Action = "ShowSwotActivityItem";
			model.Titles = new List<string> { "Item" };
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
				return RedirectToAction("Index");
			}
			ServiceSectionModel model = new ServiceSectionModel();
			model.Service = new ServiceDto { Id = 10, Name = "Support Services", Description = "this is where we operate" };
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
				TempData["message"] = $"{service.Name} has been saved";
				TempData["messageType"] = WebMessageType.Success;
			}
			return RedirectToAction("Show", new { section = "General", id = service.Id });
		}

		[HttpPost]
		public ActionResult SaveGoalsItem(ServiceGoalDto goal)
		{
			TempData["messageType"] = WebMessageType.Success;
			TempData["message"] = "sucessfully saved goal";

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
			model.Section = section;
			model.Service = new ServiceDto();
			model.Service.Name = "Support Services";
			model.Service.Id = id;
			model.SectionItemId = id;
			model.Service.ServiceGoals = new List<ServiceGoalDto> { new ServiceGoalDto() { Id = 1, ServiceId = 10, Description = "some new goal goes here", Name = "new goal to acheive", StartDate = DateTime.Now, EndDate = DateTime.Now} }.ToArray();
			model.Service.ServiceWorkUnits = new List<IServiceWorkUnitDto>(new List<IServiceWorkUnitDto> { new ServiceWorkUnitDto { Id = 1, WorkUnit = "some team", Contact = "a manager", Responsibilities = "keep out of trouble" } });
			model.Service.ServiceContracts = new List<IServiceContractDto>(new List<IServiceContractDto>());
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
			var model = new ServiceSectionModel();
			model.Section = section;
			model.Service = new ServiceDto();
			model.Service.Name = "Support Services";
			model.Service.Id = 10;
			model.Service.ServiceOwner = "Donald Trump";
			model.Service.Description = "This service will build a great, great wall. Mark my words, it will be a great wall. <ul><li>tall</li><li>long<li><ul>";

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
			if (id == 0)//something has gone very wrong
				return RedirectToAction("Show");

			ConfirmDeleteSectionItemModel cdModel = new ConfirmDeleteSectionItemModel(0, "some Item", "DeleteServiceGoalsItem", "Support Services", "Goals");

			return View("ConfirmDeleteSection", cdModel);
		}

		[HttpPost]
		public ActionResult DeleteServiceGoalsItem(DeleteSectionItemModel model)
		{
			TempData["messageType"] = WebMessageType.Success;
			TempData["message"] = "successfully deleted " + model.FriendlyName;

			return RedirectToAction("Show", new { id = model.Serviceid, section = model.Section });
		}

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
					Guid newFileName = Guid.NewGuid();

					var path = Path.Combine(ConfigurationManager.AppSettings["FilePath"], newFileName.ToString());

					file.SaveAs(Server.MapPath(path));
				}
			}
			return RedirectToAction("Show", new { id, section = "Documents" });
		}

		/// <summary>
		/// Rename the document
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult RenameDocument(Guid id)
		{
			return RedirectToAction("Show", new { section = "Documents", id = 10 });
		}

		public FileResult DownloadServiceDocument(Guid id)
		{
			Response.ContentType = "application/text";
			Response.AddHeader("Content-Disposition", @"filename=""thisIsTheReportio""");

			var path = Path.Combine(ConfigurationManager.AppSettings["FilePath"], id.ToString());

			return new FilePathResult(path + "testDoc.txt", "text/plain");
		}

	    [ChildActionOnly]
	    public ActionResult ShowLifecycleStatuses()
	    {
            IPortfolioService sps = new PortfolioService(dummId, new ServiceBundleController(), new ServicePortfolioService.Controllers.ServiceController(), new LifecycleStatusController());
	        
            return View("PartialViews/ShowLifeCycleStatuses", sps.GetLifecycleStatusNames());
	    }

    }
}