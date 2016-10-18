using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DataService.Models;
using Prometheus.WebUI.Models.Shared;

namespace Prometheus.WebUI.Controllers
{
    public class ServiceEditController : Controller
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
        /// Add a new Service but no Service Package Framework available
        /// </summary>
        /// <returns></returns>
        public ActionResult AddService()
        {
            return View();
        }

        /// <summary>
        /// Initialize a new service, start with the name and the editor details will come after
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveService(string serviceName)
        {
            //save new service and get it's int
            int newint = 0;

            return RedirectToAction("UpdateGeneral", newint);
        }

        public ActionResult UpdateGeneral(int serviceint=0)
        {
            if (serviceint == 0)            //View will not be functional without a service
            {
                RedirectToAction("Index");
            }
            Service serviceInfo = new Service();
            serviceInfo.Name = "";
            serviceInfo.Description = "";

            return View(serviceInfo);
        }


        public ActionResult UpdateSwot(int serviceint=0)
        {
            if (serviceint == 0)            //View will not be functional without a service
            {
                RedirectToAction("Index");
            }


            return View();
        }

        public ActionResult UpdateWorkUnits(int serviceint=0)
        {
            if (serviceint == 0)            //View will not be functional without a service
            {
                RedirectToAction("Index");
            }


            return View();
        }

        public ActionResult UpdateGoals(int id=0)
        {
            if (id == 0)            //View will not be functional without a service
            {
                RedirectToAction("Index");
            }


            return View();
        }

        public ActionResult UpdateContracts(int id = 0)
        {
            if (id == 0)            //View will not be functional without a service
            {
                RedirectToAction("Index");
            }


            return View();
        }

        public ActionResult UpdateMeasures(int id = 0)
        {
            if (id == 0)            //View will not be functional without a service
            {
                RedirectToAction("Index");
            }


            return View();
        }

        public ActionResult UpdateProcesses(int id = 0)
        {
            if (id == 0)            //View will not be functional without a service
            {
                RedirectToAction("Index");
            }


            return View();
        }

        public ActionResult UpdatePricing(int id = 0)
        {
            if (id == 0)            //View will not be functional without a service
            {
                RedirectToAction("Index");
            }


            return View();
        }

        public ActionResult UpdateDocuments(int id = 0)
        {
            if (id == 0)            //View will not be functional without a service
            {
                RedirectToAction("Index");
            }


            return View();
        }

        /// <summary>
        /// Builds the partial view with selected item
        ///    actions are assumed to follow Add - Show - Update - Delete
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult ShowServiceList(int selectedId=0)
        {
            LinkListModel servicesModel = new LinkListModel();
            servicesModel.SelectedItemId = selectedId;
            servicesModel.ListItems = new List<KeyValuePair<int, string>> {new KeyValuePair<int, string>(1, "Operations")};
            servicesModel.AddAction = "AddService";
            servicesModel.SelectAction = "ShowGeneral";
            servicesModel.Title = "Services";

            return PartialView("PartialViews/_LinkList", servicesModel);
        }

        
    }
}