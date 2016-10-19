using System;
using System.Collections.Generic;
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
        public ActionResult ShowServiceList(int id=0)
        {
            /* I am here for the looks, remove me */
            LinkListModel servicesModel = new LinkListModel();
            servicesModel.SelectedItemId = id;
            servicesModel.ListItems = new List<KeyValuePair<int, string>> {new KeyValuePair<int, string>(10, "Operations")};
            servicesModel.AddAction = "AddService";
            servicesModel.SelectAction = "Show/General";
            servicesModel.Title = "Services";

            return PartialView("PartialViews/_LinkList", servicesModel);
        }

        [ChildActionOnly]
        public ActionResult ShowNav(string section, int id = 0)
        {
            var sections = new List<string>
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

            ServiceNavModel model = new ServiceNavModel(sections, section, id, "Show"+section);
            
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

  }
}