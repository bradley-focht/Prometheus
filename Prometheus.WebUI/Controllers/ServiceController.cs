using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Common.Dto;
using Common.Enums.Entities;
using Common.Enums.Permissions;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Helpers.Enums;
using Prometheus.WebUI.Infrastructure;
using Prometheus.WebUI.Models.Service;
using Prometheus.WebUI.Models.Shared;
using ServicePortfolioService;
using UserManager;

namespace Prometheus.WebUI.Controllers
{
	/// <summary>
	/// Service Details
	/// </summary>
	[Authorize]
	public class ServiceController : PrometheusController
	{
		private const int ServicePageSize = 12;
		private readonly IPortfolioService _portfolioService;

		public ServiceController(IPortfolioService portfolioService, IUserManager userManager)
		{
			_portfolioService = portfolioService;
			_userManager = userManager;
		}

		/// <summary>
		/// Default page 
		/// </summary>
		/// <returns></returns>
		public ActionResult Index(string filterBy, string filterArg, int pageId = 0)
		{
			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanViewServiceDetails)) { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }

			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanViewServiceDetails)) { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }
			if (filterBy == null)       //avoid null pointer exceptions below
				filterBy = "All";
			if (filterArg == null)
				filterArg = "All";
			ServiceViewModel model = new ServiceViewModel { ControlsModel = new ServiceViewControlsModel { FilterBy = filterBy, FilterArg = filterArg, PageNumber = pageId } };


			ServiceIndexHelper helper = new ServiceIndexHelper(_portfolioService.GetServices());   //apply filters
			model.ControlsModel.FilterMenu = helper.GetControlsModel();

			if (filterBy != "All")      //one of several applied filters
			{
				if (filterBy == "Search")
				{
					if (filterArg == "All")
					{
						return RedirectToAction("Index");
					}
					filterArg = filterArg.ToLower();
					model.Services = helper.GetServices().Where(s => s.Name.ToLower().Contains(filterArg));
				}
				else
				{
					int arg = -1; //an impossible arg
					try { arg = int.Parse(filterArg); }
					catch { model.Services = helper.GetServices(); } //no search string returns all services

					switch (filterBy)           //limited number of filter options
					{
						case "Catalog":
							helper.AddFilter(FilterByType.Catalog, arg);
							break;
						case "Status":
							helper.AddFilter(FilterByType.Status, arg);
							break;
						case "ServiceOwner":
							helper.AddFilter(FilterByType.ServiceOwner, 0);
							break;
						default:
							model.Services = helper.GetServices();
							break;
					}
					model.Services = helper.GetServices();
					model.ControlsModel.AppliedFilter = helper.AppliedFilter;
				}
			}
			else
			{
				model.Services = helper.GetServices();
			}

			//now onto pagination
			if (model.Services != null && model.Services.Count() > ServicePageSize)
			{
				model.ControlsModel.TotalPages = ((model.Services.Count() + ServicePageSize - 1) / ServicePageSize);
				model.Services = model.Services.Skip(ServicePageSize * pageId).Take(ServicePageSize);
			}

			return View(model);
		}

		[HttpPost]
		public ActionResult ServiceSearch(string searchString)
		{
			return searchString == "" ? RedirectToAction("Index") : RedirectToAction("Index", "Service", new { filterBy = "Search", filterArg = searchString });
		}

		/// <summary>
		/// Navigation Control, entity names are coded here
		///   names here are used in action names
		///   incoming section names are validated against the list, if no match then first in list is used
		///   names should be put just as they will show up, spaces are removed, special characters will cause problems
		/// </summary>
		/// <param name="section">selected section</param>
		/// <param name="id">selected service id</param>
		/// <returns></returns>
		[ChildActionOnly]
		public ActionResult ShowNav(string section, int id = 0)
		{
			ServiceNavModel model = new ServiceNavModel(ServiceSectionHelper.GenerateNavLinks(), section, id, $"Show{section}");

			return PartialView("PartialViews/_ServiceNav", model);
		}

		/// <summary>
		/// Show service list
		/// </summary>
		/// <param name="section"></param>
		/// <param name="id">selected service id</param>
		/// <param name="pageId">selected page, default is page 9</param>
		/// <returns></returns>
		public ActionResult Show(string section, int id = 0, int pageId = 0)
		{
			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanViewServiceDetails)) { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }

			ServiceModel model = new ServiceModel { CurrentPage = pageId };
			model.Service = _portfolioService.GetService(id);
			model.SelectedSection = section;
			return View(model);
		}

		/// <summary>
		/// Add a new service
		/// </summary>
		/// <returns></returns>
		public ActionResult AddService()
		{
			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanViewServiceDetails)) { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }
			ServiceSectionModel model = new ServiceSectionModel();

			//create list items for service bundle selection
			List<SelectListItem> serviceBundleNames = new List<SelectListItem>();
			serviceBundleNames.Add(new SelectListItem { Text = "Service Bundle..." });
			serviceBundleNames.AddRange(_portfolioService.GetServiceBundleNames().Select(b =>
						new SelectListItem
						{
							Value = b.Item1.ToString(),
							Text = b.Item2
						}));
			model.ServiceBundleNames = serviceBundleNames;

			//create list of service lifecycle statuses
			List<SelectListItem> statuses = new List<SelectListItem>();
			statuses.Add(new SelectListItem {Text = "Lifecycle Status..." });
			statuses.AddRange(_portfolioService.GetLifecycleStatusNames().Select(l =>
						new SelectListItem
						{
							Value = l.Item1.ToString(),
							Text = l.Item2
						}));
			model.StatusNames = statuses;

			//empty new service
			model.Service = new ServiceDto();
			return View("AddService", model);
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
				if (newService.Id < 0)
					return RedirectToAction("AddService");
				return RedirectToAction("UpdateGeneral", new {id = newService.Id});
			}
			//save service

			int newId;
			try
			{
				newId = _portfolioService.ModifyService(UserId, newService, EntityModification.Create).Id;
			}
			catch (Exception e)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to save service {newService.Name}, error: {e}";
				return RedirectToAction("AddService");
			}
			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = $"New service {newService.Name} saved successfully";

			//return to a vew that will let the user now add to the SDP of the service
			return RedirectToAction("Show", new { section = "General", id = newId });
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
				if (swotItem.Id > 0)
					return RedirectToAction("UpdateServiceSectionItem", new { serviceId = swotItem.ServiceId, section = "Swot", id=swotItem.Id });
				return RedirectToAction("AddServiceSectionItem", new {id = swotItem.ServiceId, section = "Swot"});
			}

			try
			{
				_portfolioService.ModifyServiceSwot(UserId, swotItem, swotItem.Id <= 0 ? EntityModification.Create : EntityModification.Update);
			}
			catch (Exception e)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to save SWOT, error: {e.Message}";
				return RedirectToAction("AddServiceSectionItem", new { section = "Swot", id = swotItem.ServiceId });
			}

			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = $"Successfully saved {swotItem.Item}";
			return RedirectToAction("Show", new { section = "Swot", id = swotItem.ServiceId });
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
				return RedirectToAction("UpdateServiceSectionItem", new { section = "WorkUnits", id = workUnit.ServiceId });
			}

			try
			{
				_portfolioService.ModifyServiceWorkUnit(UserId, workUnit, workUnit.Id < 1 ? EntityModification.Create : EntityModification.Update);
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to save Work Unit, error: {exception}";
				return RedirectToAction("UpdateServiceSectionItem", new { section = "WorkUnits", id = workUnit.ServiceId });
			}

			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = $"Successfully saved {workUnit.Name}";
			return RedirectToAction("Show", new { section = "WorkUnits", id = workUnit.ServiceId });
		}

		/// <summary>
		/// Save changes to, or save a new goal
		/// </summary>
		/// <param name="goal"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SaveServiceGoalItem(ServiceGoalDto goal)
		{
			if (!ModelState.IsValid) /* Server side validation */
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = "Failed to save goal due to invalid data";
				if (goal.Id < 1)
					return RedirectToAction("UpdateServiceSectionItem",
						new {section = "Goals", id = goal.Id, serviceId = goal.ServiceId});
				return RedirectToAction("AddServiceSectionItem", new {section="Goals", serviceId=goal.ServiceId});
			}
			//save service
			try
			{
				_portfolioService.ModifyServiceGoal(UserId, goal, EntityModification.Create);
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to save goal due to error: {exception.Message}";
			}
			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = $"New service {goal.Name} saved successfully";

			return RedirectToAction("Show", new { id = goal.ServiceId, section = "Goals" });
		}

		/// <summary>
		/// Save service measure or changes to
		/// </summary>
		/// <param name="measure"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SaveMeasuresItem(ServiceMeasureDto measure)
		{
			if (!ModelState.IsValid) /* Server side validation */
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = "Failed to save measure due to invalid data";
				if (measure.Id > 0)
					return RedirectToAction("UpdateServiceSectionItem", new { section = "Measures", id = measure.Id, serviceId = measure.ServiceId });
				return RedirectToAction("AddServiceSectionItem", new { section = "Measures", serviceId = measure.ServiceId });
			}
			//save service
			_portfolioService.ModifyServiceMeasure(UserId, measure, measure.Id < 1 ? EntityModification.Create : EntityModification.Update);

			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = $"{measure.Method} saved successfully";

			return RedirectToAction("Show", new { id = measure.ServiceId, section = "Measures" });
		}

		/// <summary>
		/// Save a new Swot Activity
		/// </summary>
		/// <param name="activity"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SaveSwotActivityItem(SwotActivityDto activity)
		{
			if (!ModelState.IsValid)    //server side validation
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = "Failed to save SWOT activity due to invalid data";
				return View("UpdateSwotActivityItem", new SwotActivityItemModel(activity));
			}

			_portfolioService.ModifySwotActivity(UserId, activity, activity.Id <= 0 ? EntityModification.Create : EntityModification.Update);
			var activityParent = _portfolioService.GetServiceSwot(UserId, activity.ServiceSwotId);

			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = $"Successfully saved {activity.Name}";

			return RedirectToAction("ShowServiceSectionItem",
				new { section = "Swot", serviceId = activityParent.ServiceId, id = activity.ServiceSwotId });
		}


		[HttpPost]
		public ActionResult SaveContractsItem(ServiceContractDto contract)
		{
			if (!ModelState.IsValid) /* Server side validation */
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = "Failed to save contract due to invalid data";
				if (contract.Id > 0)
					return RedirectToAction("UpdateServiceSectionItem", new { id = contract.Id, section = "Contracts", serviceId = contract.ServiceId });
				return RedirectToAction("AddServiceSectionItem", new { section = "Contracts", serviceId = contract.ServiceId });
			}
			//save service
			_portfolioService.ModifyServiceContract(UserId, contract, contract.Id > 0 ? EntityModification.Update : EntityModification.Create);

			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = $"Contract {contract.ContractNumber} saved successfully";

			return RedirectToAction("Show", new { id = contract.ServiceId, section = "Contracts" });
		}

		/// <summary>
		/// Returns list of table of goals
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
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

			var service = _portfolioService.GetService(id);

			if (service.ServiceGoals != null && service.ServiceGoals.Any())
			{
				tblModel.Titles = new List<string> { "Goal", "Description", "Duration", "Start Date", "End Date" };
				List<Tuple<int, ICollection<string>>> data = new List<Tuple<int, ICollection<string>>>();

				foreach (var goal in service.ServiceGoals)
				//check for data before doing anything, if no data a "add new" message will be displayed
				{
					data.Add(new Tuple<int, ICollection<string>>(goal.Id, new List<string>
					{
						goal.Name,
						StringHelper.CamelToString(goal.Type.ToString()),
						goal.Description,
						goal.StartDate?.ToString("d") ?? "n/a",
						goal.EndDate?.ToString("d") ?? "n/a"
					}));
				}
				tblModel.Data = data;
				tblModel.Action = "ShowServiceSectionItem"; //add rest of functionality if needed
				tblModel.ConfirmDeleteAction = "ConfirmDeleteServiceGoalsItem";
				tblModel.UpdateAction = "UpdateServiceSectionItem";
			}
			return PartialView("PartialViews/_TableViewer", tblModel);
		}


		/// <summary>
		/// Show service contracts table
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[ChildActionOnly]
		public ActionResult ShowServiceContracts(int id)
		{
			TableDataModel tblModel = new TableDataModel
			{
				ServiceSection = "Contracts",
				Controller = "Service",
				AddAction = "AddServiceSectionItem",
				ServiceId = id
			};

			var service = _portfolioService.GetService(id);

			if (service.ServiceContracts != null && service.ServiceContracts.Any())
			{
				tblModel.Titles = new List<string> { "Provider", "Contract", "Description", "Start Date", "Expiry Date" };
				var data = new List<Tuple<int, ICollection<string>>>();

				foreach (var contract in service.ServiceContracts)
				//check for data before doing anything, if no data a "add new" message will be displayed
				{
					data.Add(new Tuple<int, ICollection<string>>(contract.Id, new List<string>
					{
						contract.ServiceProvider,
						contract.ContractNumber,
						contract.Description,
						contract.StartDate.ToString("d"),	//proper date format also used in razor view
						contract.ExpiryDate.ToString("d")
					}));
				}
				tblModel.Data = data;
				tblModel.Action = "ShowServiceSectionItem"; //add rest of functionality if needed
				tblModel.ConfirmDeleteAction = "ConfirmDeleteServiceContractsItem";
				tblModel.UpdateAction = "UpdateServiceSectionItem";
			}
			return PartialView("PartialViews/_TableViewer", tblModel);
		}

		/// <summary>
		/// Work Units
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
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
			tblModel.ConfirmDeleteAction = "ConfirmDeleteServiceWorkUnitsItem";
			tblModel.UpdateAction = "UpdateServiceSectionItem";

			var service = _portfolioService.GetService(id);
			var workUnits = service.ServiceWorkUnits;
			tblModel.ServiceId = service.Id;

			if (workUnits != null && workUnits.Any())
			{
				tblModel.Titles = new List<string> { "Name", "Department", "Contact", "Responsibilities" };
				tblModel.Data = new List<Tuple<int, ICollection<string>>>();
				foreach (var unit in workUnits)
				{
					tblModel.Data.Add(new Tuple<int, ICollection<string>>(unit.Id,
						new List<string> { unit.Name, unit.Department, unit.Contact, unit.Responsibilities }));
				}
			}

			return PartialView("PartialViews/_TableViewer", tblModel);
		}

		/// <summary>
		/// returns Service Measures table
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[ChildActionOnly]
		public ActionResult ShowServiceMeasures(int id)
		{
			TableDataModel tblModel = new TableDataModel
			{
				Action = "ShowServiceSectionItem",
				ServiceSection = "Measures",
				Controller = "Service"
			};

			tblModel.AddAction = "AddServiceSectionItem";
			tblModel.ConfirmDeleteAction = "ConfirmDeleteServiceMeasuresItem";
			tblModel.UpdateAction = "UpdateServiceSectionItem";

			var service = _portfolioService.GetService(id);
			var measures = service.ServiceMeasures;
			tblModel.ServiceId = service.Id;

			if (measures != null && measures.Any())
			{
				tblModel.Titles = new List<string> { "Method", "Outcome" };
				tblModel.Data = new List<Tuple<int, ICollection<string>>>();
				foreach (var measure in measures)
				{
					tblModel.Data.Add(new Tuple<int, ICollection<string>>(measure.Id,
						new List<string> { measure.Method, measure.Outcome }));
				}
			}

			return PartialView("PartialViews/_TableViewer", tblModel);
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
			TableDataModel model = new TableDataModel
			{ /* new table data model */
				Action = "ShowSwotActivity",
				Titles = new List<string> { "Activity", "Date" },
				Data = new List<Tuple<int, ICollection<string>>>(),
				UpdateAction = "UpdateSwotActivityItem",
				ConfirmDeleteAction = "ConfirmDeleteSwotActivityItem"
			};

			var swot = _portfolioService.GetServiceSwot(UserId, id);

			foreach (var activity in swot.SwotActivities)
			{
				model.Data.Add(new Tuple<int, ICollection<string>>(activity.Id,
					new List<string> { activity.Name, activity.Date.ToString("d") }));
			}

			return View("PartialViews/_TableViewer", model);
		}

		/// <summary>
		/// confirm deletion of a swot activity
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult ConfirmDeleteSwotActivityItem(int id)
		{
			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanEditServiceDetails)) { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }
			var activity = _portfolioService.GetSwotActivity(UserId, id);
			var swot = _portfolioService.GetServiceSwot(UserId, activity.ServiceSwotId);
			var model = new ConfirmDeleteSectionItemModel
			{
				Id = id,
				ServiceId = swot.ServiceId,
				DeleteAction = "DeleteSwotActivity",
				Name = activity.Name,
				AltId = swot.Id,
				AltName = swot.Item
			};
			model.Service = _portfolioService.GetService(swot.ServiceId).Name;
			model.ServiceId = swot.ServiceId;

			return View("ConfirmDeleteSwotActivityItem", model);
		}
		/// <summary>
		/// complete deltion of a swot activity
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult DeleteSwotActivity(DeleteSectionItemModel model)
		{
			TempData["messageType"] = WebMessageType.Success;
			TempData["message"] = "Successfully deleted " + model.FriendlyName;

			int swotId = _portfolioService.GetSwotActivity(UserId, model.Id).ServiceSwotId;
			_portfolioService.ModifySwotActivity(UserId, new SwotActivityDto { Id = model.Id }, EntityModification.Delete);

			return RedirectToAction("ShowServiceSectionItem",
				new { id = swotId, serviceId = model.ServiceId, section = "Swot" });
		}

		/// <summary>
		/// Show table of processes
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[ChildActionOnly]
		public ActionResult ShowServiceProcesses(int id)
		{
			TableDataModel tblModel = new TableDataModel
			{
				Action = "ShowServiceSectionItem",
				AddAction = "AddServiceSectionItem",
				ConfirmDeleteAction = "ConfirmDeleteServiceProcessesItem",
				UpdateAction = "UpdateServiceSectionItem",
				ServiceId = id,
				ServiceSection = "Processes",
				Controller = "Service"
			};

			var items = _portfolioService.GetService(id).ServiceProcesses;
			if (items != null && items.Any())
			{
				tblModel.Titles = new List<string> { "Name", "Owner", "Benefits", "Improvements" };                  //titles
				tblModel.Data = new List<Tuple<int, ICollection<string>>>();    //list for data

				foreach (var item in items)
				{
					tblModel.Data.Add(new Tuple<int, ICollection<string>>(item.Id, new List<string> { item.Name, item.Owner, item.Benefits, item.Improvements }));
				}
			}
			return PartialView("PartialViews/_TableViewer", tblModel);
		}

		/// <summary>
		/// Show all ICatalogable items
		/// </summary>
		/// <param name="id"></param>
		/// <param name="pageId"></param>
		/// <returns></returns>
		[ChildActionOnly]
		public ActionResult ShowServiceOptions(int id, int pageId = 0)
		{
			OptionsTableModel model = new OptionsTableModel { Options = new List<ICatalogPublishable>(), ServiceId = id, CurrentPage = pageId };
			var service = _portfolioService.GetService(id);
			if (service.ServiceOptionCategories != null)
			{
				model.Options.AddRange((from o in service.ServiceOptionCategories select (ICatalogPublishable)o).ToList());
				//get and sort data
			}

			if (service.ServiceOptions != null)
			{
				model.Options.AddRange((from o in service.ServiceOptions select (ICatalogPublishable)(ServiceOptionDto)o).ToList());
			}

			model.Options = model.Options.OrderBy(o => o.Name).ToList();                                    //sorting
			if (model.Options != null && model.Options.Count() > ServicePageSize)                           //pagination
			{
				model.TotalPages = (model.Options.Count + ServicePageSize - 1) / ServicePageSize;
				model.Options = model.Options.Skip(ServicePageSize * pageId).Take(ServicePageSize).ToList();
			}
			try
			{
				model.i = ConfigHelper.GetMarr();      // for net present value calculations
				model.n = ConfigHelper.GetPeriod();
			}
			catch (Exception exception)                                                         //leave it to default values (0) if fails
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to read from configuration file, error: {exception.Message}";
			}

			return PartialView("PartialViews/ShowOptionsTable", model);
		}

		/// <summary>
		/// Save a new or updates to an existing service option
		/// </summary>
		/// <param name="option">service option dto</param>
		/// <param name="serviceId">service Id</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SaveOptionsItem(ServiceOptionDto option, int serviceId)
		{
			if (!ModelState.IsValid) /* Server side validation */
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = "Failed to save option due to invalid data";
				return option.Id == 0 ? RedirectToAction("AddServiceOption", new { id = serviceId }) : RedirectToAction("UpdateServiceOption", new { id = option.Id });
			}

			//save valid service, file is already uploaded		
			if (option.Id > 0)      //existing record, need to not save over service catalog info
			{
				var existing = _portfolioService.GetServiceOption(UserId, option.Id);
				option = (ServiceOptionDto)AbbreviatedEntityUpdate.UpdateServiceOption(existing, option);
			}
			_portfolioService.ModifyServiceOption(UserId, option, option.Id < 1 ? EntityModification.Create : EntityModification.Update);
			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = $"New option {option.Name} saved successfully";

			return RedirectToAction("Show", new { id = _portfolioService.GetServiceOptionCategory(UserId, option.ServiceOptionCategoryId).ServiceId, section = "Options" });
		}

		/// <summary>
		/// Complete save of process
		/// </summary>
		/// <param name="process"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SaveProcessesItem(ServiceProcessDto process)
		{
			if (!ModelState.IsValid) /* Server side validation */
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = "Failed to save option due to invalid data";
				return RedirectToAction("UpdateServiceSectionItem", new { serviceId = process.ServiceId, section = "Processes" });
			}

			_portfolioService.ModifyServiceProcess(UserId, process, process.Id < 1 ? EntityModification.Create : EntityModification.Update);

			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = $"Process {process.Name} saved successfully";

			return RedirectToAction("Show", new { id = process.ServiceId, section = "Processes" });
		}


		/// <summary>
		/// Save new or existing option category
		/// </summary>
		/// <param name="category">posted cattegory attributes</param>
		/// <param name="options">Ids of options</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SaveOptionCategory(ServiceOptionCategoryDto category, ICollection<int> options)
		{
			if (!ModelState.IsValid)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = "Failed to save category due to invalid data";
				return RedirectToAction("UpdateOptionCategoryItem", new { id = category.ServiceId });
			}
			//save new category		
			try
			{
				if (category.Id > 0) //need to preserve catalog data on the entity
				{
					category = (ServiceOptionCategoryDto)AbbreviatedEntityUpdate.UpdateServiceCategory(_portfolioService.GetServiceOptionCategory(UserId, category.Id), category);
				}
				category.Id = _portfolioService.ModifyServiceOptionCategory(UserId, category, category.Id < 1 ? EntityModification.Create : EntityModification.Update).Id;    //make sure it has the new id if is new
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to save category, error: {exception.Message}";
				return RedirectToAction("UpdateOptionCategoryItem", new { id = category.ServiceId });
			}

			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = $"Successfully saved {category.Name}";

			return RedirectToAction("Show", new { id = category.ServiceId, section = "Options" });
		}


		/// <summary>
		/// Sends service to UpdateSection view for form input
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult UpdateGeneral(int id)
		{
			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanViewServiceDetails)) { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }
			ServiceSectionModel model = new ServiceSectionModel();
			try
			{
				model.Service = _portfolioService.GetService(id);
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed retrieve service: {exception.Message}";
				return RedirectToAction("Show", new {section = "General", id });
			}
			List<SelectListItem> serviceBundleNames = new List<SelectListItem>();
			serviceBundleNames.Add(new SelectListItem { Text = "Service Bundle..." });
			serviceBundleNames.AddRange(_portfolioService.GetServiceBundleNames().Select(b =>
						new SelectListItem
						{
							Value = b.Item1.ToString(),
							Text = b.Item2,
							Selected = b.Item1 == model.Service.ServiceBundleId
						}));
			model.ServiceBundleNames = serviceBundleNames;

			//create list of service lifecycle statuses
			List<SelectListItem> statuses = new List<SelectListItem>();
			statuses.Add(new SelectListItem { Text = "Lifecycle Status..." });
			statuses.AddRange(_portfolioService.GetLifecycleStatusNames().Select(l =>
						new SelectListItem
						{
							Value = l.Item1.ToString(),
							Text = l.Item2,
							Selected = l.Item1 == model.Service.LifecycleStatusId
						}));
			model.StatusNames = statuses;

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
			if (!ModelState.IsValid)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"{service.Name} has not been failed due to invalid data";
				if (service.Id > 0)
				{
					return RedirectToAction("UpdateGeneral", new {id = service.Id});
				}
				return RedirectToAction("AddService");
			}

			//perform the save
			service = (ServiceDto)AbbreviatedEntityUpdate.UpdateService(_portfolioService.GetService(service.Id), service);         //preserve data updated from ICatalogPublishable interface

			_portfolioService.ModifyService(UserId, service, EntityModification.Update);   //perform the update
			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = $"{service.Name} has been saved";
			return RedirectToAction("Show", new { section = "General", id = service.Id, pageId=0 });
		}

		/// <summary>
		/// Save a Goal
		/// </summary>
		/// <param name="goal"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SaveGoalsItem(ServiceGoalDto goal)
		{
			if (!ModelState.IsValid)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Unable to save {goal.Name}";
				if (goal.Id > 0)
					RedirectToAction("UpdateServiceSectionItem", new { section = "Goals", serviceId = goal.ServiceId, id=goal.Id });
				RedirectToAction("AddServiceSectionItem", new {section = "Goals", id = goal.ServiceId});
			}

			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = "Sucessfully saved goal";

			_portfolioService.ModifyServiceGoal(UserId, goal, goal.Id < 1 ? EntityModification.Create : EntityModification.Update);

			return RedirectToAction("show", new { section = "Goals", id = goal.ServiceId });
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
			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanViewServiceDetails)) { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }
			ServiceSectionModel model = new ServiceSectionModel();

			model.Service = _portfolioService.GetService(serviceId);

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

			model.Section = section;
			model.SectionItemId = id;
			model.Service = _portfolioService.GetService(serviceId);
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
			return View("PartialViews/UpdateSwotItem", _portfolioService.GetServiceSwot(UserId, id));
		}

		public ActionResult UpdateOptionCategoryItem(int id)
		{
			var cat = _portfolioService.GetServiceOptionCategory(UserId, id);                                 //temp category for referencing
			var model = new UpdateOptionCategoryModel
			{
				ServiceName = _portfolioService.GetService(cat.ServiceId).Name,
				Action = "Update",
				ServiceId = cat.ServiceId,
				OptionCategory = cat
			};

			//add all items to the list

			return View("UpdateOptionCategory", model);
		}
		/// <summary>
		/// Add a new service option
		/// </summary>
		/// <param name="id"></param>
		/// <param name="categoryId"></param>
		/// <returns></returns>
		public ActionResult AddServiceOption(int id, int categoryId = 0)
		{
			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanEditServiceDetails)) { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }
			var model = new ServiceOptionModel { Option = new ServiceOptionDto { ServiceOptionCategoryId = categoryId, Id = 0 }, ServiceId = id }; //setup new model
			model.Action = "Add";
			try
			{
				model.ServiceName = _portfolioService.GetService(model.ServiceId).Name;
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Error encountered, {exception.Message}";
			}

			return View("UpdateServiceOption", model);
		}

		/// <summary>
		/// Update Service Option or Add new
		/// </summary>
		/// <param name="id">option id</param>
		/// <returns></returns>
		public ActionResult UpdateServiceOption(int id)
		{
			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanEditServiceDetails)) { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }
			var model = new ServiceOptionModel { Option = _portfolioService.GetServiceOption(UserId, id) };
			model.ServiceId = _portfolioService.GetServiceOptionCategory(UserId, model.Option.ServiceOptionCategoryId).ServiceId;

			model.ServiceName = _portfolioService.GetService(model.ServiceId).Name;
			model.Action = "Update";

			return View("UpdateServiceOption", model);
		}

		/// <summary>
		/// Update an existing swot activity
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult UpdateSwotActivityItem(int id)
		{
			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanEditServiceDetails)) { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }
			var model = new SwotActivityItemModel(new SwotActivityDto { ServiceSwotId = id });
			ISwotActivityDto swotActivity = _portfolioService.GetSwotActivity(UserId, id);
			var swotItem = _portfolioService.GetServiceSwot(UserId, swotActivity.ServiceSwotId);
			model.SwotName = swotItem.Item;
			model.ServiceId = swotItem.ServiceId;
			model.ServiceName = _portfolioService.GetService(swotItem.ServiceId).Name;
			model.SwotActivity = swotActivity;

			return View("UpdateSwotActivityItem", model);
		}

		/// <summary>
		/// Add swot activity
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult AddSwotActivityItem(int id)
		{
			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanEditServiceDetails)) { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }
			var swot = _portfolioService.GetServiceSwot(UserId, id);

			var model = new SwotActivityItemModel(new SwotActivityDto { ServiceSwotId = id });
			model.ServiceName = _portfolioService.GetService(swot.ServiceId).Name;
			model.ServiceId = swot.ServiceId;
			model.Action = "Add";
			model.SwotName = swot.Item;

			return View("UpdateSwotActivityItem", model);
		}

		/// <summary>
		/// Add an option category
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult AddOptionCategory(int id)
		{
			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanEditServiceDetails)) { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }
			UpdateOptionCategoryModel model = new UpdateOptionCategoryModel { Action = "Add" };
			model.ServiceId = id;
			model.ServiceName = _portfolioService.GetService(id).Name;
			model.OptionCategory = new ServiceOptionCategoryDto { ServiceId = id };

			return View("UpdateOptionCategory", model);
		}

		/// <summary>
		/// Show an option category's details
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult ShowOptionCategory(int id)
		{
			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanEditServiceDetails)) { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }
			ShowOptionCategoryModel model = new ShowOptionCategoryModel { OptionCategory = _portfolioService.GetServiceOptionCategory(UserId, id) };
			model.ServiceId = model.OptionCategory.ServiceId;
			model.ServiceName = _portfolioService.GetService(model.OptionCategory.ServiceId).Name;

			return View("ShowOptionCategory", model);
		}


		/// <summary>
		/// General Add new ServiceSectionItem
		/// </summary>
		/// <param name="section"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult AddServiceSectionItem(string section, int id)
		{
			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanEditServiceDetails)) { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }
			var model = new ServiceSectionModel();
			model.Section = section;

			model.Service = _portfolioService.GetService(id);


			return View("AddSectionItem", model);
		}

		/// <summary>
		/// Generates the confirm deletion warning page
		/// </summary>
		/// <param name="id">Id of ServiceGoal</param>
		/// <returns></returns>
		public ActionResult ConfirmDeleteServiceGoalsItem(int id)
		{
			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanEditServiceDetails)) { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }
			var goal = _portfolioService.GetServiceGoal(UserId, id);
			var model = new ConfirmDeleteSectionItemModel
			{
				Id = id,
				ServiceId = goal.ServiceId,
				DeleteAction = "DeleteServiceGoal",
				Name = goal.Name,
				Section = "Goals"

			};
			model.Service = _portfolioService.GetService(goal.ServiceId).Name;
			model.ServiceId = goal.ServiceId;

			return View("ConfirmDeleteSection", model);
		}
		/// <summary>
		/// Delete a Service Measure
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult ConfirmDeleteServiceMeasuresItem(int id)
		{
			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanEditServiceDetails)) { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }
			var measure = _portfolioService.GetServiceMeasure(UserId, id);
			var model = new ConfirmDeleteSectionItemModel
			{
				Id = id,
				ServiceId = measure.ServiceId,
				DeleteAction = "DeleteServiceMeasure",
				Name = measure.Method,
				Section = "Measures"

			};
			model.Service = _portfolioService.GetService(measure.ServiceId).Name;
			model.ServiceId = measure.ServiceId;

			return View("ConfirmDeleteSection", model);
		}

		/// <summary>
		/// Confirmation before deleting a SWOT item
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult ConfirmDeleteServiceSwotItem(int id)
		{
			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanEditServiceDetails)) { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }
			var item = _portfolioService.GetServiceSwot(UserId, id);
			var model = new ConfirmDeleteSectionItemModel
			{
				Id = id,
				ServiceId = item.ServiceId,
				DeleteAction = "DeleteServiceSwotItem",
				Name = item.Item,
				Section = "Swot"
			};
			model.Service = _portfolioService.GetService(item.ServiceId).Name;
			return View("ConfirmDeleteSection", model);
		}

		/// <summary>
		/// Delete a contract
		/// </summary>
		/// <param name="id">Contract id</param>
		/// <returns></returns>
		public ActionResult ConfirmDeleteServiceContractsItem(int id)
		{
			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanEditServiceDetails)) { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }
			var item = _portfolioService.GetServiceContract(UserId, id);
			var model = new ConfirmDeleteSectionItemModel
			{
				Id = id,
				ServiceId = item.ServiceId,
				DeleteAction = "DeleteServiceContract",
				Name = $"{item.ServiceProvider}: {item.ContractNumber}",
				Section = "Contracts"
			};
			model.Service = _portfolioService.GetService(item.ServiceId).Name;
			return View("ConfirmDeleteSection", model);
		}

		/// <summary>
		/// Confirm deletion of work unit
		/// </summary>
		/// <param name="id">Work Unit Id</param>
		/// <returns></returns>
		public ActionResult ConfirmDeleteServiceWorkUnitsItem(int id)
		{
			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanEditServiceDetails)) { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }
			var item = _portfolioService.GetServiceWorkUnit(UserId, id);
			var model = new ConfirmDeleteSectionItemModel
			{
				Id = id,
				ServiceId = item.ServiceId,
				DeleteAction = "DeleteServiceWorkUnit",
				Name = item.Name,
				Section = "WorkUnits"
			};
			model.Service = _portfolioService.GetService(item.ServiceId).Name;
			return View("ConfirmDeleteSection", model);
		}

		/// <summary>
		/// Confirm deleting a service option
		/// </summary>
		/// <param name="id">Service Option Id</param>
		/// <returns></returns>
		public ActionResult ConfirmDeleteServiceOption(int id)
		{
			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanEditServiceDetails)) { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }
			var item = _portfolioService.GetServiceOption(UserId, id);
			var model = new ConfirmDeleteSectionItemModel
			{
				Id = id,
				DeleteAction = "DeleteServiceOption",
				Name = item.Name,
				Section = "Options"
			};
			model.Service = _portfolioService.GetService(_portfolioService.GetServiceOptionCategory(UserId, item.ServiceOptionCategoryId).ServiceId).Name;
			return View("ConfirmDeleteSection", model);
		}

		/// <summary>
		/// Confirm delete of an option category
		/// </summary>
		/// <param name="id">category id</param>
		/// <returns></returns>
		public ActionResult ConfirmDeleteOptionCategory(int id)
		{
			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanEditServiceDetails)) { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }
			var item = _portfolioService.GetServiceOptionCategory(UserId, id);
			var model = new ConfirmDeleteSectionItemModel
			{
				Id = id,
				ServiceId = item.ServiceId,
				DeleteAction = "DeleteOptionCategory",
				Name = item.Name,
				Section = "Options"
			};
			model.Service = _portfolioService.GetService(item.ServiceId).Name;
			return View("ConfirmDeleteSection", model);
		}
		/// <summary>
		/// confirm deletion of a service process
		/// </summary>
		/// <param name="id">process id</param>
		/// <returns></returns>
		public ActionResult ConfirmDeleteServiceProcessesItem(int id)
		{
			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanEditServiceDetails)) { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }
			var item = _portfolioService.GetServiceProcess(UserId, id);
			var model = new ConfirmDeleteSectionItemModel
			{
				Service = _portfolioService.GetService(item.ServiceId).Name,
				Id = id,
				ServiceId = item.ServiceId,
				DeleteAction = "DeleteServiceProcessesItem",
				Name = item.Name,
				Section = "Processes"
			};
			return View("ConfirmDeleteSection", model);
		}

		/// <summary>
		/// completion of deleting a process
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public ActionResult DeleteServiceProcessesItem(DeleteSectionItemModel model)
		{
			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanEditServiceDetails)) { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }
			try
			{
				_portfolioService.ModifyServiceProcess(UserId, new ServiceProcessDto { Id = model.Id }, EntityModification.Delete);
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to delete {model.FriendlyName}, error: {exception.Message}";

				return RedirectToAction("Show", new { id = model.ServiceId, section = "Options" });
			}
			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = $"Successfully deleted {model.FriendlyName}";

			return RedirectToAction("Show", new { id = model.ServiceId, section = "Processes" });
		}

		/// <summary>
		/// delete an option category
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult DeleteOptionCategory(DeleteSectionItemModel model)
		{
			var options = _portfolioService.GetServiceOptionCategory(UserId, model.Id).ServiceOptions;

			try
			{
				if (options != null)                        //delete service options in the category prior to deleting category
					foreach (var option in options)
					{
						_portfolioService.ModifyServiceOption(UserId, option, EntityModification.Delete);
					}                                       //delete the category
				_portfolioService.ModifyServiceOptionCategory(UserId, new ServiceOptionCategoryDto { Id = model.Id }, EntityModification.Delete);
			}
			catch (Exception exception)                     //may fail on fk
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to delete {model.FriendlyName}, error: {exception.Message}";

				return RedirectToAction("Show", new { id = model.ServiceId, section = "Options" });
			}
			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = $"Successfully deleted {model.FriendlyName}";

			return RedirectToAction("Show", new { id = model.ServiceId, section = "Options" });
		}


		/// <summary>
		/// Complete delete of service option
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult DeleteServiceOption(DeleteSectionItemModel model)
		{
			int serviceId = 0;
			try
			{
				_portfolioService.ModifyServiceOption(UserId, new ServiceOptionDto { Id = model.Id }, EntityModification.Delete);
				serviceId = _portfolioService.GetServiceOptionCategory(UserId, model.Id).ServiceId;
			}
			catch (Exception exception)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to delete {model.FriendlyName}, error: {exception.Message}";

				return RedirectToAction("Show", new { id = serviceId, section = "Options" });
			}
			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = $"Successfully deleted {model.FriendlyName}";

			return RedirectToAction("Show", new { id = serviceId, section = "Options" });
		}

		/// <summary>
		/// Completes the deletion from ConfirmDeleteServiceSwotItem
		/// </summary>
		/// <param name="model">item to delete plus information for redirection</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult DeleteServiceSwotItem(DeleteSectionItemModel model)
		{
			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = "Successfully deleted " + model.FriendlyName;


			_portfolioService.ModifyServiceSwot(UserId, new ServiceSwotDto { Id = model.Id }, EntityModification.Delete);

			return RedirectToAction("Show", new { id = model.ServiceId, section = "Swot" });
		}

		/// <summary>
		/// Complete deletion of a ServiceGoal
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult DeleteServiceGoal(DeleteSectionItemModel model)
		{
			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = "Successfully deleted " + model.FriendlyName;

			_portfolioService.ModifyServiceGoal(UserId, new ServiceGoalDto { Id = model.Id }, EntityModification.Delete);

			return RedirectToAction("Show", new { id = model.ServiceId, section = "Goals" });
		}

		/// <summary>
		/// Delete a Work Unit
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult DeleteServiceWorkUnit(DeleteSectionItemModel model)
		{

			try
			{
				_portfolioService.ModifyServiceWorkUnit(UserId, new ServiceWorkUnitDto() { Id = model.Id }, EntityModification.Delete);
			}
			catch (Exception e)
			{
				TempData["messageType"] = WebMessageType.Failure;
				TempData["message"] = $"Failed to delete {model.FriendlyName}, error: {e.Message}";

				return RedirectToAction("Show", new { id = model.ServiceId, section = "WorkUnits" });
			}

			TempData["messageType"] = WebMessageType.Success;
			TempData["message"] = "Successfully deleted " + model.FriendlyName;

			return RedirectToAction("Show", new { id = model.ServiceId, section = "WorkUnits" });
		}

		/// <summary>
		/// Delete a  contract
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult DeleteServiceContract(DeleteSectionItemModel model)
		{
			try
			{
				_portfolioService.ModifyServiceContract(UserId, new ServiceContractDto { Id = model.Id }, EntityModification.Delete);
			}
			catch (Exception e)
			{
				TempData["messageType"] = WebMessageType.Failure;
				TempData["message"] = $"Failed to delete {model.FriendlyName}, error: {e.Message}";
				return RedirectToAction("Show", new { id = model.ServiceId, section = "Contracts" });
			}

			TempData["messageType"] = WebMessageType.Success;
			TempData["message"] = "Successfully deleted " + model.FriendlyName;
			return RedirectToAction("Show", new { id = model.ServiceId, section = "Contracts" });
		}

		/// <summary>
		/// Complete the deltion of a Service Measure
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult DeleteServiceMeasure(DeleteSectionItemModel model)
		{
			try
			{
				_portfolioService.ModifyServiceMeasure(UserId, new ServiceMeasureDto() { Id = model.Id }, EntityModification.Delete);
			}
			catch (Exception e)
			{
				TempData["messageType"] = WebMessageType.Failure;
				TempData["message"] = $"Failed to delete {model.FriendlyName}, error: {e.Message}";

				return RedirectToAction("Show", new { id = model.ServiceId, section = "Measures" });
			}

			TempData["messageType"] = WebMessageType.Success;
			TempData["message"] = "Successfully deleted " + model.FriendlyName;

			return RedirectToAction("Show", new { id = model.ServiceId, section = "Measures" });
		}

		#region Service Documents
		/// <summary>
		/// Upload and save files if they are present. Always redirects to the Show action.
		/// </summary>
		/// <param name="file"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult UploadServiceDocument(HttpPostedFileBase file, int id)
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
						var path = Path.Combine(ConfigHelper.GetServiceDocsPath(), newFileName.ToString());
						file.SaveAs(Server.MapPath(path));      /*create new doc and upload it */
						_portfolioService.ModifyServiceDocument(UserId, new ServiceDocumentDto
						{
							MimeType = file.ContentType,
							ServiceId = id,
							Filename = Path.GetFileNameWithoutExtension(fileName),
							StorageNameGuid = newFileName,
							UploadDate = DateTime.Now,
							FileExtension = Path.GetExtension(fileName)
						}, EntityModification.Create);
					}
					catch (Exception exception)
					{
						TempData["MessageType"] = WebMessageType.Failure;
						TempData["Message"] = $"Failed to upload document, error: {exception.Message}";
					}
				}
			}
			return RedirectToAction("Show", new { id, section = "Documents" });
		}

		/// <summary>
		/// Show table of documents
		/// </summary>
		/// <param name="id"></param>
		/// <param name="pageId"></param>
		/// <returns></returns>
		public ActionResult ShowServiceDocuments(int id, int pageId = 0)
		{
			DocumentsTableModel model = new DocumentsTableModel { ServiceId = id, CurrentPage = pageId };

			var documents = _portfolioService.GetServiceDocuments(id).ToList();

			if (documents.Any())                           //pagination
			{
				model.TotalPages = (documents.Count + ServicePageSize - 1) / ServicePageSize;
				documents = documents.Skip(ServicePageSize * pageId).Take(ServicePageSize).ToList();
				model.Documents = documents;
			}

			return View("PartialViews/ShowDocuments", model);
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
				return RedirectToAction("UpdateServiceDocument", new { id = document.StorageNameGuid });
			}
			//perform the save

			var doc = _portfolioService.GetServiceDocument(UserId, document.Id);
			doc.Filename = document.Filename;
			try
			{
				_portfolioService.ModifyServiceDocument(UserId, doc, EntityModification.Update);
			}
			catch (Exception e)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to save document {document.Filename}, error: {e.Message}";

				return RedirectToAction("Show", new { section = "Documents", id = document.ServiceId });
			}

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
		public ActionResult UpdateServiceDocument(int id)
		{
			var doc = _portfolioService.GetServiceDocument(UserId, id);
			var service = _portfolioService.GetService(doc.ServiceId);

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
		public FileResult DownloadServiceDocument(int id)
		{
			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanViewServiceDetails)) { return null; }
			var doc = _portfolioService.GetServiceDocument(UserId, id);

			Response.AddHeader("Content-Disposition", @"filename=" + doc.Filename + doc.FileExtension);     //suggest file name to browser
			var path = Path.Combine(ConfigHelper.GetServiceDocsPath(), doc.StorageNameGuid.ToString());

			return new FilePathResult(path, doc.MimeType);
		}

		/// <summary>
		/// Delete a document from the file system and from the database
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult DeleteServiceDocument(int id)
		{
			var file = _portfolioService.GetServiceDocument(UserId, id);

			_portfolioService.ModifyServiceDocument(UserId, _portfolioService.GetServiceDocument(UserId, id), EntityModification.Delete);

			//don't forget to delete the document in the file system		
			try
			{
				var path = Path.Combine(ConfigurationManager.AppSettings["ServiceDocsPath"], file.StorageNameGuid.ToString());    //catch error if key is not in web.config
				System.IO.File.Delete(Server.MapPath(path));
			}
			catch (Exception e)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Failed to delete {file.Filename}, error: {e.Message}";
				return RedirectToAction("Show", new { section = "Documents", id = file.ServiceId });
			}

			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = $"Sucessfully deleted file {file.Filename}{file.FileExtension}";

			return RedirectToAction("Show", new { section = "Documents", id = file.ServiceId });
		}

		/// <summary>
		/// Document deletion confirmation
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult ConfirmDeleteServiceDocument(int id)
		{
			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanEditServiceDetails)) { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }
			var document = _portfolioService.GetServiceDocument(UserId, id);
			var service = _portfolioService.GetService(document.ServiceId);

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
		/// Builds the partial view with selected item
		///    actions are assumed to follow Add - Show - Update - Delete
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[ChildActionOnly]
		public ActionResult ShowServiceList(int id = 0)
		{


			//create the model 
			LinkListModel servicesModel = new LinkListModel
			{
				AddAction = "AddService",
				SelectAction = "Show/General",
				Controller = "Service",
				Title = "Services",
				SelectedItemId = id,
				ListItems = _portfolioService.GetServiceNames()
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
			if (!_userManager.UserHasPermission(UserId, ServiceDetails.CanViewServiceDetails)) { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }
			var model = new SwotActivityItemModel(_portfolioService.GetSwotActivity(UserId, id));
			var swot = _portfolioService.GetServiceSwot(UserId, model.SwotId);
			model.SwotName = swot.Item;
			model.SwotId = swot.Id;
			model.ServiceId = swot.ServiceId;
			model.ServiceName = _portfolioService.GetService(swot.ServiceId).Name;

			return PartialView("ShowSwotActivityItem", model);
		}

		/// <summary>
		/// Shows one Service Option
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult ShowServiceOption(int id)
		{

			var model = new ServiceOptionModel();

			model.Option = _portfolioService.GetServiceOption(UserId, id);
			model.ServiceId = _portfolioService.GetServiceOptionCategory(UserId, model.Option.ServiceOptionCategoryId).ServiceId;
			var service = _portfolioService.GetService(model.ServiceId);

			model.ServiceName = service.Name;
			model.CategoryName = (from c in service.ServiceOptionCategories
								  where model.Option.ServiceOptionCategoryId == c.Id
								  select c.Name).FirstOrDefault();
			return View(model);
		}

		/// <summary>
		/// create a properly setup drop down list of categories
		/// </summary>
		/// <param name="id">option Id</param>
		/// <param name="categoryId"></param>
		/// <param name="serviceId"></param>
		/// <returns></returns>
		[ChildActionOnly]
		public ActionResult GetCategoryDropdown(int id = 0, int categoryId = 0, int serviceId = 0)
		{
			IEnumerable<SelectListItem> model = new List<SelectListItem>();


			if (serviceId > 0)
			{
				int selectedCategory = categoryId;

				if (id > 0)     /* if a categoryId is already associated with option */
				{
					selectedCategory = _portfolioService.GetServiceOption(UserId, id).ServiceOptionCategoryId;
				}

				var optionsList = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Category..." } };
				optionsList.AddRange(_portfolioService.GetService(serviceId).ServiceOptionCategories.Select(l =>
					new SelectListItem
					{
						Value = l.Id.ToString(),
						Text = l.Name.ToString(),
						Selected = l.Id == selectedCategory
					}).ToList());
				model = optionsList.OrderBy(c => c.Text);
			}
			return View("PartialViews/CategoryDropDown", model);
		}

		/// <summary>
		/// Retrieve properly formatted service selection drop down for dependencies
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult GetServicesDropDown(int id)
		{
			var optionsList = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Services..." } };
			optionsList.AddRange(_portfolioService.GetServices().Select(l =>
				new SelectListItem
				{
					Value = l.Id.ToString(),
					Text = l.Name.ToString(),
					//Selected = selectedOptions.Contains(l.Id)
				}).ToList());
			IEnumerable<SelectListItem> model = optionsList.OrderBy(c => c.Text);

			return View("PartialViews/ServicesDropDown", model);
		}

		/// <summary>
		/// Returns a styalized dropdown list of options for the corresponding service id and appropriate options selected
		/// </summary>
		/// <param name="serviceId"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult GetOptionsDropDown(int serviceId, int id)
		{

			IEnumerable<int> selectedOptions = id != 0 ? (from o in _portfolioService.GetServiceOptionCategory(UserId, id).ServiceOptions select o.Id).ToList() : new List<int>();
			var optionsList = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Options..." } };

			var options = _portfolioService.GetService(serviceId).ServiceOptions;
			IEnumerable<SelectListItem> model = new List<SelectListItem>();

			if (options != null)
			{
				optionsList.AddRange(options.Select(l =>
					new SelectListItem
					{
						Value = l.Id.ToString(),
						Text = l.Name.ToString(),
						Selected = selectedOptions.Contains(l.Id)
					}).ToList());
				model = optionsList.OrderBy(c => c.Text);
			}
			return PartialView("PartialViews/OptionsDropDown", model);
		}

		/// <summary>
		/// Returns picture for use in Html document, use with URL.Action()
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public FileContentResult GetOptionPicture(int id)
		{
			IServiceOptionDto option = _portfolioService.GetServiceOption(UserId, id);

			if (option.Picture == null)
				return null;

			var path = Path.Combine(ConfigHelper.GetOptionPictureLocation(), option.Picture.ToString());
			byte[] fileData = null;     //file data to return
			try
			{
				fileData = System.IO.File.ReadAllBytes(Server.MapPath(path));
			}
			catch { /* ignored */}
			return File(fileData, option.PictureMimeType);
		}
	}
}