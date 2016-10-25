using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DataService.Models;
using Prometheus.WebUI.Models.Service;
using Prometheus.WebUI.Models.Shared;

namespace Prometheus.WebUI.Controllers
{
    public class ServiceController : Controller
    {
        /// <summary>
        /// Default page 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(int id = 0)
        {
            return View();
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
            servicesModel.ListItems = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(10, "Operations")
            };
            servicesModel.AddAction = "AddService";
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
            var sections = new List<string>                     //list of sections for each service
            {
                "General",
                "Goals",
                "SWOT",
                "Work Units",
                "Contracts",
                "Measures",
                "Processes",
                "Pricing",
                "Documents"
            };

            string returnLinkSection = null;                    //validate that the section sent in is in the list

            if (sections.Any(linkSection => section == linkSection.Replace(" ", "")))
            {
                returnLinkSection = section.Replace(" ", "");
            }

            if (returnLinkSection == null)                      //default is the first section in the list
                returnLinkSection = sections[0];

            ServiceNavModel model = new ServiceNavModel(sections, returnLinkSection, id, "Show" + returnLinkSection);

            return PartialView("PartialViews/_ServiceNav", model);
        }


        public ActionResult Show(string section = "", int id = 0)
        {
            ServiceModel sm = new ServiceModel();

            if (id == 0)
            {
                sm.Service = new Service() {Id = 0};
            }
            else
            {
                sm = new ServiceModel(new Service() {Id = 10, Name = "Operations"}, section.Replace(" ", ""));
            }

            return View(sm);
        }

        [ChildActionOnly]
        public ActionResult ShowServiceGoals(Service service)
        {
            TableDataModel tblModel = new TableDataModel();
            tblModel.Titles = new List<string> {"Goal", "Duration", "Start Date", "End Date"};
            tblModel.Data = new List<KeyValuePair<int, IEnumerable<string>>>
            {
                new KeyValuePair<int, IEnumerable<string>>(1,
                    new List<string> {"test the system", "short term", "september", "october"}),
                new KeyValuePair<int, IEnumerable<string>>(1,
                    new List<string> {"add actual data", "short term", "october", "march"})
            };
            tblModel.Action = "ShowServiceSectionItem";
            tblModel.ServiceSection = "Goals";
            tblModel.Controller = "Service";

            return PartialView("/Views/Shared/PartialViews/_TableViewer.cshtml", tblModel);
        }

        [ChildActionOnly]
        public ActionResult ShowServiceContracts(Service service)
        {
            TableDataModel tblModel = new TableDataModel();
            tblModel.Titles = new List<string> {"Vendor", "Contract Number", "Start Date", "End Date"};
            tblModel.Data = new List<KeyValuePair<int, IEnumerable<string>>>
            {
                new KeyValuePair<int, IEnumerable<string>>(1,
                    new List<string> {"Prometheus", "44-4507-A", "next month", "last month"})
            };

            return PartialView("/Views/Shared/PartialViews/_TableViewer.cshtml", tblModel);
        }

        [ChildActionOnly]
        public ActionResult ShowServiceWorkUnits(Service service)
        {
            TableDataModel tblModel = new TableDataModel();
            tblModel.Titles = new List<string> {"Work Unit", "Manager", "Roles"};
            tblModel.Data = new List<KeyValuePair<int, IEnumerable<string>>>
            {
                new KeyValuePair<int, IEnumerable<string>>(1,
                    new List<string> {"OCIO", "Vinay chandramohan", "Making the Service Portfolio"}),
                new KeyValuePair<int, IEnumerable<string>>(1,
                    new List<string> {"Executive", "Sean Boczulak", "be da boss"})
            };

            return PartialView("/Views/Shared/PartialViews/_TableViewer.cshtml", tblModel);
        }


        [ChildActionOnly]
        public ActionResult ShowServiceMeasures(Service service)
        {
            TableDataModel tblModel = new TableDataModel();
            tblModel.Titles = new List<string> {"Method", "Outcome"};
            tblModel.Data = new List<KeyValuePair<int, IEnumerable<string>>>
            {
                new KeyValuePair<int, IEnumerable<string>>(1, new List<string> {"divide by 0", "exception"})
            };

            return PartialView("/Views/Shared/PartialViews/_TableViewer.cshtml", tblModel);
        }


        [ChildActionOnly]
        public ActionResult ShowServiceSwot(Service service)
        {
            TableDataModel tblModel = new TableDataModel();
            tblModel.Titles = new List<string> {"Method", "Outcome"};
            tblModel.Data = new List<KeyValuePair<int, IEnumerable<string>>>
            {
                new KeyValuePair<int, IEnumerable<string>>(1, new List<string> {"divide by 0", "exception"})
            };

            return PartialView("/Views/Shared/PartialViews/_TableViewer.cshtml", tblModel);
        }


        [ChildActionOnly]
        public ActionResult ShowServiceProcesses(Service service)
        {
            TableDataModel tblModel = new TableDataModel();
            tblModel.Titles = new List<string> {"Method", "Outcome"};
            tblModel.Data = new List<KeyValuePair<int, IEnumerable<string>>>
            {
                new KeyValuePair<int, IEnumerable<string>>(1, new List<string> {"divide by 0", "exception"})
            };

            return PartialView("/Views/Shared/PartialViews/_TableViewer.cshtml", tblModel);
        }


        [ChildActionOnly]
        public ActionResult ShowServicePricing(Service service)
        {
            TableDataModel tblModel = new TableDataModel();
            tblModel.Titles = new List<string> {"Method", "Outcome"};
            tblModel.Data = new List<KeyValuePair<int, IEnumerable<string>>>
            {
                new KeyValuePair<int, IEnumerable<string>>(1, new List<string> {"divide by 0", "exception"})
            };

            return PartialView("/Views/Shared/PartialViews/_TableViewer.cshtml", tblModel);
        }

        /// <summary>
        /// Sends service to UpdateSection view for form input
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult UpdateGeneral(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            ServiceModel model = new ServiceModel();
            model.Service = new Service() {Id = 10, Name = "Operations", Description = "this is where we operate"};
            model.SelectedSection = "General";

            return View("UpdateSectionItem", model);
        }


        [HttpPost]
        public ActionResult SaveGeneral(Service service)
        {
            return RedirectToAction("Show", new { section="General", id=service.Id});
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
            model.Section = "Goals";
            model.Service = new Service();
            model.Service.Name = "Operations";
            model.Service.Id = 10;
            model.Service.ServiceGoals = new List<IServiceGoal> { new ServiceGoal { Description = "some new goal goes here", Name = "hi" } };
            return View("ShowSectionItem", model);
        }


        public ActionResult UpdateServiceSectionItem(string section, int id = 0)
        {
            ServiceSectionModel model = new ServiceSectionModel();
            model.Section = "Goals";
            model.Service = new Service();
            model.Service.Name = "Operations";
            model.Service.Id = 10;
            model.Service.ServiceGoals = new List<IServiceGoal> { new ServiceGoal { Description = "some new goal goes here", Name = "hi" } };
            return View("UpdateSectionItem", model);
        }
    }
}