using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Common.Dto;
using Prometheus.WebUI.Helpers;
using Prometheus.WebUI.Models.Shared;
using Prometheus.WebUI.Models.SystemAccess;


namespace Prometheus.WebUI.Controllers
{
    //[Authorize]
    public class SystemAccessController : Controller
    {
        // GET: SystemAccess
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PermissionsAndRoles()
        {
            return View();
        }

        public ActionResult ShowRolePermissions(int id)
        {

            return View("PermissionsAndRoles");
        }



        /// <summary>
        /// Show the link list of roles
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult ShowRoles()
        {
            var model = new LinkListModel
            {
                Title = "Roles",
                SelectAction = "ShowRolePermissions",
                AddAction = "AddRole",
                Controller = "SystemAccess"
            };

            model.ListItems = new List<Tuple<int, string>> {new Tuple<int, string>(5, "Approver"), new Tuple<int, string>(1, "Service Owner"), new Tuple<int, string>(2, "Service Manager")};

            return View("PartialViews/_LinkList", model);
        }

        public ActionResult AddRole()
        {
            return View();
        }


        public ActionResult UpdateRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveRole(RoleDto role)
        {
            return RedirectToAction("ShowRolePermissions", role.Id);
        }


        public ActionResult ConfirmDeleteRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteRole()
        {
            return RedirectToAction("ShowRolePermissions");
        }
        public ActionResult UserAccess()
        {
            return View(new AdSearchResultsModel());
        }

        /// <summary>
        /// Setup the partial View for searching AD accounts   
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchAdAccount(string searchString)
        {
            AdSearchResultsModel model = new AdSearchResultsModel();
            UserManager.UserManager um = new UserManager.UserManager();
            try
            {
                model.SearchResults = um.SearchUsers(searchString);
            }
            catch(Exception exception)
            {
                TempData["MessageType"] = WebMessageType.Failure;
                TempData["Message"] = exception.Message;
                return View("UserAccess", model);
            }
            return View("UserAccess", model);
        }

    }
}