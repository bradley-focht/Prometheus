using System.Collections.Generic;
using System.Linq;
using Common.Dto;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Models.Shared;
using System.Web.Mvc;
using Common.Enums.Entities;

namespace Prometheus.WebUI.Controllers
{
    [Authorize]
    public class ServiceMaintenanceController : Controller
    {
        /// <summary>
        /// Returns main scree, this is the menu options
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Basic list of services
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ShowServices(int id = 0)
        {
            ServiceDto model = null;
            if (id != 0)
            {
                var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));

                model = (ServiceDto)ps.GetService(id);
            }
            if (model == null)
                model = new ServiceDto { Id = 0 };

            return View(model);
        }

        /// <summary>
        /// Create the list of services to show
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ShowServiceList(int id = 0)
        {
            var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));

            //create the model 
            LinkListModel servicesModel = new LinkListModel
            {
                SelectAction = "ShowServices",
                Controller = "ServiceMaintenance",
                Title = "Services",
                SelectedItemId = id,
                ListItems = ps.GetServiceNames()
            };

            return PartialView("PartialViews/_LinkList", servicesModel);
        }


        /// <summary>
        /// Show details of selected lifecycle or none if no id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ShowLifecycle(int id = 0)
        {
            var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
            var model = (LifecycleStatusDto)ps.GetLifecycleStatus(id);
            if (model == null)
                model = new LifecycleStatusDto { Id = 0 };
            return View(model);
        }

        /// <summary>
        /// Show lifecycle status link list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ShowLifeCycleList(int id = 0)
        {
            var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));

            LinkListModel servicesModel = new LinkListModel();
            servicesModel.SelectedItemId = id;

            servicesModel.ListItems = ps.GetLifecycleStatusNames();

            servicesModel.AddAction = "AddLifecycle";
            servicesModel.SelectAction = "ShowLifecycle";
            servicesModel.Controller = "ServiceMaintenance";
            servicesModel.Title = "Lifecycle Statuses";

            return PartialView("PartialViews/_LinkList", servicesModel);
        }

        /// <summary>
        /// return view to add a lifecycle status
        /// </summary>
        /// <returns></returns>
        public ActionResult AddLifecycle()
        {
            return View();
        }


        /// <summary>
        /// Save or update Lifecycle Statuses
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveLifecycle(LifecycleStatusDto model)
        {
            if (ModelState.IsValid)
            {
                var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));

                ps.SaveLifecycleStatus(model);

                TempData["messageType"] = WebMessageType.Success;
                TempData["message"] = "successfully saved lifecycle status";
                return RedirectToAction("ShowLifecycle");
            }
            TempData["messageType"] = WebMessageType.Failure;
            TempData["message"] = "failed to save lifecycle status";
            return RedirectToAction("AddLifecycle");
        }

        /// <summary>
        /// Return view for updating a lifecycle status
        /// </summary>
        /// <param name="id">lifecycle status id</param>
        /// <returns></returns>
        public ActionResult UpdateLifecycle(int id)
        {
            var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
            return View((LifecycleStatusDto)ps.GetLifecycleStatus(id));
        }

        /// <summary>
        /// Confirm the deletion prior to deleting
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ConfirmDeleteLifecycle(int id = 0)
        {
            var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
            if (id == 0)
                return RedirectToAction("ShowLifecycle");


            return View((LifecycleStatusDto)ps.GetLifecycleStatus(id));
        }


        /// <summary>
        /// Complete the deltion process of a Lifecycle Status. 
        /// </summary>
        /// <param name="item">Name and Id of what is being deleted</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteLifecycle(DeleteModel item)
        {
            var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
            if (ps.DeleteLifecycleStatus(item.Id))
            {
                TempData["messageType"] = WebMessageType.Success;
                TempData["message"] = $"Successfully deleted lifecycle status {item.Name}";

                return RedirectToAction("ShowLifecycle");
            }

            TempData["messageType"] = WebMessageType.Failure;
            TempData["message"] = $"Failed to delete lifecycle status {item.Name}";
            return RedirectToAction("ShowLifecycle");
        }

        /// <summary>
        /// Sends View for delete confirmation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ConfirmDeleteService(int id = 0)
        {
            var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));

            return View(ps.GetService(id));
        }

        /// <summary>
        /// Complete the deletion process and display a success message
        /// </summary>
        /// <param name="item">Name and ID of what is being deleted</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteService(DeleteModel item)
        {
            var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
            ps.ModifyService(new ServiceDto { Id = item.Id }, EntityModification.Delete);

            TempData["messageType"] = WebMessageType.Failure;
            TempData["message"] = $"Failed to delete {item.Name}";
            return RedirectToAction("ShowServices");
        }

        /// <summary>
        /// Create a drop down menu of available statuses
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetStatusCountDropDown(int id)
        {
            var ps = InterfaceFactory.CreatePortfolioService(int.Parse(Session["Id"].ToString()));
            int count = ps.CountLifecycleStatuses();

            var optionsList = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Position..." } };
            for (int i = 1; i <= count + 1; i++)
            {
                optionsList.Add(
                    new SelectListItem
                    {
                        Value = i.ToString(),
                        Text = i.ToString(),
                        Selected = id == i
                    });
            }
            return View("PartialViews/StatusCountDropDown", optionsList);
        }
    }
}