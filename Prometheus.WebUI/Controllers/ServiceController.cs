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
using Common.Enums.Entities;
using Prometheus.WebUI.Helpers.Enums;

namespace Prometheus.WebUI.Controllers
{
	[Authorize]
	public class ServiceController : Controller
	{
		private const int ServicePageSize = 12;

		/// <summary>
		/// Default page 
		/// </summary>
		/// <returns></returns>
		public ActionResult Index(string filterBy, string filterArg, int pageId = 0)
		{
			if (filterBy == null)       //avoid null pointer exceptions below
				filterBy = "All";
			if (filterArg == null)
				filterArg = "All";
			ServiceViewModel model = new ServiceViewModel { ControlsModel = new ServiceViewControlsModel { FilterBy = filterBy, FilterArg = filterArg, PageNumber = pageId } };
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));

			ServiceIndexHelper helper = new ServiceIndexHelper(ps.GetServices());   //apply filters
			model.ControlsModel.FilterMenu = helper.GetControlsModel();

			if (filterBy != "All")		//one of several applied filters
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
		/// <param name="section"></param>
		/// <param name="id"></param>
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
		/// <param name="id"></param>
		/// <param name="pageId"></param>
		/// <returns></returns>
		public ActionResult Show(string section, int id=0, int pageId = 0)
		{
		    int userId;
		    try{ userId = int.Parse(Session["Id"].ToString()); }            //this page is an entry point, possible userId may not exist
            catch { return View();}

			ServiceModel sm = new ServiceModel { CurrentPage = pageId };

			var ps = InterfaceFactory.CreatePortfolioService(userId);
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
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			ServiceSectionModel model = new ServiceSectionModel();
			model.ServiceBundleNames = ps.GetServiceBundleNames().Select(b =>
						new SelectListItem
						{
							Value = b.Item1.ToString(),
							Text = b.Item2
						});

			ps.GetServiceBundleNames();
			model.StatusNames = ps.GetLifecycleStatusNames().Select(l =>
						new SelectListItem
						{
							Value = l.Item1.ToString(),
							Text = l.Item2
						});
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
				return RedirectToAction("AddService");
			}
			//save service
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			int newId;
			try
			{
				newId = ps.ModifyService(newService, EntityModification.Create).Id;
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
				return RedirectToAction("UpdateServiceSectionItem", new { id = swotItem.ServiceId, section = "Swot" });
			}

			IPortfolioService ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			try
			{
				ps.ModifyServiceSwot(swotItem, swotItem.Id <= 0 ? EntityModification.Create : EntityModification.Update);
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

			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			try
			{
				ps.ModifyServiceWorkUnit(workUnit, workUnit.Id < 1 ? EntityModification.Create : EntityModification.Update);
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
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			ps.ModifyServiceGoal(goal, EntityModification.Create);

			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = $"New service {goal.Name} saved successfully";

			return RedirectToAction("Show", new { id = goal.ServiceId, section = "Goals" });
		}

		[HttpPost]
		public ActionResult SaveMeasuresItem(ServiceMeasureDto measure)
		{
			if (!ModelState.IsValid) /* Server side validation */
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = "Failed to save measure due to invalid data";
				return RedirectToAction("AddService");
			}
			//save service
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			ps.ModifyServiceMeasure(measure, measure.Id < 1 ? EntityModification.Create : EntityModification.Update);

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

			IPortfolioService ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			ps.ModifySwotActivity(activity, activity.Id <= 0 ? EntityModification.Create : EntityModification.Update);
			var activityParent = ps.GetServiceSwot(activity.ServiceSwotId);

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
				return RedirectToAction("UpdateServiceSectionItem", new { id = contract.ServiceId, section = "Contracts" });
			}
			//save service
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			ps.ModifyServiceContract(contract, contract.Id > 0 ? EntityModification.Update : EntityModification.Create);

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

			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			var service = ps.GetService(id);

			if (service.ServiceGoals != null && service.ServiceGoals.Any())
			{
				tblModel.Titles = new List<string> { "Goal","Description", "Duration", "Start Date", "End Date" };
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

			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			var service = ps.GetService(id);

			if (service.ServiceContracts != null && service.ServiceContracts.Any())
			{
				tblModel.Titles = new List<string> { "Provider", "Contract", "Start Date", "Expiry Date" };
				List<Tuple<int, ICollection<string>>> data = new List<Tuple<int, ICollection<string>>>();

				foreach (var contract in service.ServiceContracts)
				//check for data before doing anything, if no data a "add new" message will be displayed
				{
					data.Add(new Tuple<int, ICollection<string>>(contract.Id, new List<string>
					{
						contract.ServiceProvider,
						contract.ContractNumber,
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


			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			var service = ps.GetService(id);
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

			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			var service = ps.GetService(id);
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
			TableDataModel model = new TableDataModel();

			model.Action = "ShowSwotActivity";

			model.Titles = new List<string> { "Activity", "Date" };
			model.Data = new List<Tuple<int, ICollection<string>>>();
			model.UpdateAction = "UpdateSwotActivityItem";
			model.ConfirmDeleteAction = "ConfirmDeleteSwotActivityItem";

			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			var swot = ps.GetServiceSwot(id);

			foreach (var activity in swot.SwotActivities)
			{
				model.Data.Add(new Tuple<int, ICollection<string>>(activity.Id,
					new List<string> { activity.Name, activity.Date.ToString("d") }));
			}

			return View("PartialViews/_TableViewer", model);
		}

		public ActionResult ConfirmDeleteSwotActivityItem(int id)
		{
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
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

			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			int swotId = ps.GetSwotActivity(model.Id).ServiceSwotId;
			ps.ModifySwotActivity(new SwotActivityDto { Id = model.Id }, EntityModification.Delete);

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

			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));

			var items = ps.GetService(id).ServiceProcesses;
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
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			OptionsTableModel model = new OptionsTableModel { Options = new List<ICatalogPublishable>(), ServiceId = id, CurrentPage = pageId };
			var service = ps.GetService(id);
		    if (service.ServiceOptionCategories != null)
		    {
		        model.Options.AddRange((from o in service.ServiceOptionCategories select (ICatalogPublishable) o).ToList());
		            //get and sort data
		    }

		    if (service.ServiceOptions != null)
		    {
		        model.Options.AddRange((from o in service.ServiceOptions select (ICatalogPublishable) (ServiceOptionDto) o).ToList());
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
		/// <param name="option"></param>
		/// <param name="image"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult SaveOptionsItem(ServiceOptionDto option, HttpPostedFileBase image = null)
		{
			if (!ModelState.IsValid) /* Server side validation */
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = "Failed to save option due to invalid data";
				return option.Id == 0 ? RedirectToAction("AddServiceOption", new { id = option.ServiceId }) : RedirectToAction("UpdateServiceOption", new { id = option.Id });
			}
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			if (image != null)
			{
				if (option.Id != 0) //need to deal with updating an existing image
				{
					var existingOption = ps.GetServiceOption(option.Id);
					if (existingOption.Picture != null)
					{
						var path = Path.Combine(ConfigurationManager.AppSettings["OptionPicsPath"], option.Picture.ToString());
							//catch error if key is not in web.config
						try
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
				}

				option.PictureMimeType = image.ContentType; //rename file to a guid and store original file type
				option.Picture = Guid.NewGuid();

				try
				{
					var path = Path.Combine(ConfigurationManager.AppSettings["OptionPicsPath"], option.Picture.ToString()); //save file
					image.SaveAs(Server.MapPath(path));
				}
				catch (Exception exception)
				{
					TempData["MessageType"] = WebMessageType.Failure;
					TempData["Message"] = $"Failed to save option, file error: {exception.Message}";
					return option.Id == 0 ? RedirectToAction("AddServiceOption", new { id = option.ServiceId }) : RedirectToAction("UpdateServiceOption", new { id = option.Id });
				}
			}

			//save valid service, file is already uploaded		
			ps.ModifyServiceOption(option, option.Id < 1 ? EntityModification.Create : EntityModification.Update);
			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = $"New option {option.Name} saved successfully";

			return RedirectToAction("Show", new { id = option.ServiceId, section = "Options" });
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
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			ps.ModifyServiceProcess(process, process.Id < 1 ? EntityModification.Create : EntityModification.Update);

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
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));

			try
			{
				category.Id = ps.ModifyServiceOptionCategory(category, category.Id < 1 ? EntityModification.Create : EntityModification.Update).Id;    //make sure it has the new id if is new
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
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			ServiceSectionModel model = new ServiceSectionModel();
			model.Service = ps.GetService(id);
			model.ServiceBundleNames = ps.GetServiceBundleNames().Select(b =>
						new SelectListItem
						{
							Value = b.Item1.ToString(),
							Text = b.Item2,
							Selected = b.Item1 == model.Service.ServiceBundleId
						});
			ps.GetServiceBundleNames();
			model.StatusNames = ps.GetLifecycleStatusNames().Select(l =>
						new SelectListItem
						{
							Value = l.Item1.ToString(),
							Text = l.Item2,
							Selected = l.Item1 == model.Service.LifecycleStatusId
						});
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
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			ps.ModifyService(service, EntityModification.Update);

			return RedirectToAction("Show", new { section = "General", id = service.Id });
		}

		[HttpPost]
		public ActionResult SaveGoalsItem(ServiceGoalDto goal)
		{
			if (!ModelState.IsValid)
			{
				TempData["MessageType"] = WebMessageType.Failure;
				TempData["Message"] = $"Unable to save {goal.Name}";
				RedirectToAction("Show", new { section = "Goals", id = goal.ServiceId });
			}

			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = "Sucessfully saved goal";
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			ps.ModifyServiceGoal(goal, goal.Id < 1 ? EntityModification.Create : EntityModification.Update);

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
			ServiceSectionModel model = new ServiceSectionModel();
			IPortfolioService ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));

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
			IPortfolioService ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
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
			IPortfolioService ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));

			return View("PartialViews/UpdateSwotItem", ps.GetServiceSwot(id));
		}

		public ActionResult UpdateOptionCategoryItem(int id)
		{
			IPortfolioService ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			var cat = ps.GetServiceOptionCategory(id);                                 //temp category for referencing
			var model = new UpdateOptionCategoryModel
			{
				ServiceName = ps.GetService(cat.ServiceId).Name,
				Action = "Update",
				ServiceId = cat.ServiceId,
				OptionCategory = (ServiceOptionCategoryDto)cat
			};

			//add all items to the list

			return View("UpdateOptionCategory", model);
		}

		public ActionResult AddServiceOption(int id)
		{
			IPortfolioService ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			var model = new ServiceOptionModel { Option = new ServiceOptionDto { ServiceId = id, Id = 0 } };
			model.ServiceName = ps.GetService(model.Option.ServiceId).Name;
			model.Action = "Add";

			return View("UpdateServiceOption", model);
		}

		public ActionResult UpdateServiceOption(int id)
		{
			IPortfolioService ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			var model = new ServiceOptionModel { Option = (ServiceOptionDto)ps.GetServiceOption(id) };
			model.ServiceName = ps.GetService(model.Option.ServiceId).Name;
			model.Action = "Update";

			return View("UpdateServiceOption", model);
		}

		public ActionResult UpdateSwotActivityItem(int id)
		{
			IPortfolioService ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			var model = new SwotActivityItemModel(new SwotActivityDto { ServiceSwotId = id });
			ISwotActivityDto swotActivity = ps.GetSwotActivity(id);
			var swotItem = ps.GetServiceSwot(swotActivity.ServiceSwotId);
			model.SwotName = swotItem.Item;
			model.ServiceId = swotItem.ServiceId;
			model.ServiceName = ps.GetService(swotItem.ServiceId).Name;
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

			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			var swot = ps.GetServiceSwot(id);

			var model = new SwotActivityItemModel(new SwotActivityDto { ServiceSwotId = id });
			model.ServiceName = ps.GetService(swot.ServiceId).Name;
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
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));

			UpdateOptionCategoryModel model = new UpdateOptionCategoryModel { Action = "Add" };
			model.ServiceId = id;
			model.ServiceName = ps.GetService(id).Name;
			model.OptionCategory = new ServiceOptionCategoryDto { ServiceId = id };

			return View("UpdateOptionCategory", model);
		}

		public ActionResult ShowOptionCategory(int id)
		{
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));

			ShowOptionCategoryModel model = new ShowOptionCategoryModel { OptionCategory = ps.GetServiceOptionCategory(id) };
			model.ServiceId = model.OptionCategory.ServiceId;
			model.ServiceName = ps.GetService(model.OptionCategory.ServiceId).Name;

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
			var model = new ServiceSectionModel();
			model.Section = section;

			IPortfolioService ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
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
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
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

		public ActionResult ConfirmDeleteServiceMeasuresItem(int id)
		{
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			var measure = ps.GetServiceMeasure(id);
			var model = new ConfirmDeleteSectionItemModel
			{
				Id = id,
				ServiceId = measure.ServiceId,
				DeleteAction = "DeleteServiceMeasure",
				Name = measure.Method,
				Section = "Measures"

			};
			model.Service = ps.GetService(measure.ServiceId).Name;
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
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
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
		/// Delete a contract
		/// </summary>
		/// <param name="id">Contract id</param>
		/// <returns></returns>
		public ActionResult ConfirmDeleteServiceContractsItem(int id)
		{
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			var item = ps.GetServiceContract(id);
			var model = new ConfirmDeleteSectionItemModel
			{
				Id = id,
				ServiceId = item.ServiceId,
				DeleteAction = "DeleteServiceContract",
				Name = $"{item.ServiceProvider}: {item.ContractNumber}",
				Section = "Contracts"
			};
			model.Service = ps.GetService(item.ServiceId).Name;
			return View("ConfirmDeleteSection", model);
		}

		/// <summary>
		/// Confirm deletion of work unit
		/// </summary>
		/// <param name="id">Work Unit Id</param>
		/// <returns></returns>
		public ActionResult ConfirmDeleteServiceWorkUnitsItem(int id)
		{
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
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

		/// <summary>
		/// Confirm deleting a service option
		/// </summary>
		/// <param name="id">Service Option Id</param>
		/// <returns></returns>
		public ActionResult ConfirmDeleteServiceOption(int id)
		{
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			var item = ps.GetServiceOption(id);
			var model = new ConfirmDeleteSectionItemModel
			{
				Id = id,
				DeleteAction = "DeleteServiceOption",
				Name = item.Name,
				Section = "Options"
			};
			model.Service = ps.GetService(ps.GetServiceOptionCategory(item.ServiceOptionCategoryId).ServiceId).Name;
			return View("ConfirmDeleteSection", model);
		}

		/// <summary>
		/// Confirm delete of an option category
		/// </summary>
		/// <param name="id">category id</param>
		/// <returns></returns>
		public ActionResult ConfirmDeleteOptionCategory(int id)
		{
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			var item = ps.GetServiceOptionCategory(id);
			var model = new ConfirmDeleteSectionItemModel
			{
				Id = id,
				ServiceId = item.ServiceId,
				DeleteAction = "DeleteOptionCategory",
				Name = item.Name,
				Section = "Options"
			};
			model.Service = ps.GetService(item.ServiceId).Name;
			return View("ConfirmDeleteSection", model);
		}
		/// <summary>
		/// confirm deletion of a service process
		/// </summary>
		/// <param name="id">process id</param>
		/// <returns></returns>
		public ActionResult ConfirmDeleteServiceProcessesItem(int id)
		{
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			var item = ps.GetServiceProcess(id);
			var model = new ConfirmDeleteSectionItemModel
			{
				Service = ps.GetService(item.ServiceId).Name,
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
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			try
			{
				ps.ModifyServiceProcess(new ServiceProcessDto { Id = model.Id }, EntityModification.Delete);
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
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			var options = ps.GetServiceOptionCategory(model.Id).ServiceOptions;

			try
			{
				if (options != null)                        //delete service options in the category prior to deleting category
					foreach (var option in options)
					{
						ps.ModifyServiceOption(option, EntityModification.Delete);
					}                                       //delete the category
				ps.ModifyServiceOptionCategory(new ServiceOptionCategoryDto { Id = model.Id }, EntityModification.Delete);
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
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			try
			{
				ps.ModifyServiceOption(new ServiceOptionDto { Id = model.Id }, EntityModification.Delete);
			}
			catch (Exception exception)
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
		/// Completes the deletion from ConfirmDeleteServiceSwotItem
		/// </summary>
		/// <param name="model">item to delete plus information for redirection</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult DeleteServiceSwotItem(DeleteSectionItemModel model)
		{
			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = "Successfully deleted " + model.FriendlyName;

			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			ps.ModifyServiceSwot(new ServiceSwotDto { Id = model.Id }, EntityModification.Delete);

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
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			ps.ModifyServiceGoal(new ServiceGoalDto { Id = model.Id }, EntityModification.Delete);

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
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			try
			{
				ps.ModifyServiceWorkUnit(new ServiceWorkUnitDto() { Id = model.Id }, EntityModification.Delete);
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
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			try
			{
				ps.ModifyServiceContract(new ServiceContractDto { Id = model.Id }, EntityModification.Delete);
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
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			try
			{
				ps.ModifyServiceMeasure(new ServiceMeasureDto() { Id = model.Id }, EntityModification.Delete);
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
				var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));

				if (fileName != null)
				{
					Guid newFileName = Guid.NewGuid(); //to rename document			
													   //file path location comes from the Web.config file
					try
					{
						var path = Path.Combine(ConfigurationManager.AppSettings["ServiceDocsPath"], newFileName.ToString());
						file.SaveAs(Server.MapPath(path));      /*create new doc and upload it */
						ps.ModifyServiceDocument(new ServiceDocumentDto
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

		public ActionResult ShowServiceDocuments(int id, int pageId = 0)
		{
			DocumentsTableModel model = new DocumentsTableModel { ServiceId = id, CurrentPage = pageId };

			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));

			var documents = ps.GetServiceDocuments(id).ToList();
            
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
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			var doc = ps.GetServiceDocument(document.Id);
			doc.Filename = document.Filename;
			try
			{
				ps.ModifyServiceDocument(doc, EntityModification.Update);
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
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
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
		public FileResult DownloadServiceDocument(int id)
		{
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			var doc = ps.GetServiceDocument(id);

			Response.AddHeader("Content-Disposition", @"filename=" + doc.Filename + doc.FileExtension);     //suggest file name to browser

			var path = Path.Combine(ConfigurationManager.AppSettings["ServiceDocsPath"], doc.StorageNameGuid.ToString());

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
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			var file = ps.GetServiceDocument(id);

			ps.ModifyServiceDocument(ps.GetServiceDocument(id), EntityModification.Delete);

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
				return RedirectToAction("Show", new {section="Documents", id = file.ServiceId });
			}

			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = $"Sucessfully deleted file {file.Filename}{file.FileExtension}";

			return RedirectToAction("Show", new {section="Documents", id = file.ServiceId });
		}

		/// <summary>
		/// Document deletion confirmation
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult ConfirmDeleteServiceDocument(int id)
		{
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
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
		/// Builds the partial view with selected item
		///    actions are assumed to follow Add - Show - Update - Delete
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[ChildActionOnly]
		public ActionResult ShowServiceList(int id = 0)
		{
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));

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
			var model = new SwotActivityItemModel((SwotActivityDto)ps.GetSwotActivity(id));
			var swot = ps.GetServiceSwot(model.SwotId);
			model.SwotName = swot.Item;
			model.SwotId = swot.Id;
			model.ServiceId = swot.ServiceId;
			model.ServiceName = ps.GetService(swot.ServiceId).Name;

			return PartialView("ShowSwotActivityItem", model);
		}

		/// <summary>
		/// Shows one Service Option
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult ShowServiceOption(int id)
		{
			var ps = InterfaceFactory.CreatePortfolioService(id);
			var model = new ServiceOptionModel();

			model.Option = (ServiceOptionDto)ps.GetServiceOption(id);
			var service = ps.GetService(model.Option.ServiceId);

			model.ServiceName = service.Name;
			model.CategoryName = (from c in service.ServiceOptionCategories
                                  where model.Option.ServiceOptionCategoryId == c.Id
								  select c.Name).FirstOrDefault();

			return View(model);
		}

		[ChildActionOnly]
		public ActionResult GetCategoryDropdown(int id = 0, int serviceId = 0)
		{
			IEnumerable<SelectListItem> model = new List<SelectListItem>();
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));

			if (serviceId > 0)
			{
				int? selectedCategory = null;

				if (id > 0)
				{
					selectedCategory = ps.GetServiceOption(id).ServiceOptionCategoryId;
				}

				var optionsList = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Category..." } };
				optionsList.AddRange(ps.GetService(serviceId).ServiceOptionCategories.Select(l =>
					new SelectListItem
					{
						Value = l.Id.ToString(),
						Text = l.Name.ToString(),
						Selected = selectedCategory != null && l.Id == selectedCategory
					}).ToList());
				model = optionsList.OrderBy(c => c.Text);
			}
			return View("PartialViews/CategoryDropDown", model);
		}

		public ActionResult GetServicesDropDown(int id)
		{
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));

			//ICollection<int> selectedOptions = (from o in ps.GetOptionCategory(id).ServiceOptions select o.Id).ToList();

			var optionsList = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Services..." } };
			optionsList.AddRange(ps.GetServices().Select(l =>
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
			var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
			ICollection<int> selectedOptions = id != 0 ? (from o in ps.GetServiceOptionCategory(id).ServiceOptions select o.Id).ToList() : new List<int>();
			var optionsList = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Options..." } };

		    var options = ps.GetService(serviceId).ServiceOptions;
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
		    return View("PartialViews/OptionsDropDown", model);
		}

		/// <summary>
		/// Returns picture for use in Html document, use with URL.Action()
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public FileContentResult GetOptionPicture(int id)
		{
			var ps = InterfaceFactory.CreatePortfolioService(id);
			IServiceOptionDto option = ps.GetServiceOption(id);

			if (option.Picture == null)
				return null;

			var path = Path.Combine(ConfigurationManager.AppSettings["OptionPicsPath"], option.Picture.ToString());
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