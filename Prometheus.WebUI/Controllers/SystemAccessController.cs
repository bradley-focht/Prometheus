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
			return View("PermissionsAndRoles", new RoleModel { Role = new RoleDto() });
		}

		/// <summary>
		/// Default page to show and edit roles
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public ActionResult ShowRolePermissions(int id)
        {
			// need to get role from user manager 
            return View("PermissionsAndRoles", new RoleModel{Role = new RoleDto {Id = id} });
        }

        /// <summary>
        /// Show the link list of roles
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult ShowRoles(int id = 0)
        {
            var model = new LinkListModel
            {
                Title = "Roles",
                SelectAction = "ShowRolePermissions",
                AddAction = "AddRole",
                Controller = "SystemAccess"
            };

            model.ListItems = new List<Tuple<int, string>> {new Tuple<int, string>(5, "Approver"), new Tuple<int, string>(1, "Service Owner"), new Tuple<int, string>(2, "Service Manager")};
            model.SelectedItemId = id;
            return View("PartialViews/_LinkList", model);
        }

		/// <summary>
		/// Returns screen to add a new role 
		/// </summary>
		/// <returns></returns>
        public ActionResult AddRole()
        {
            return View("UpdateRole", new RoleModel {Role = new RoleDto(), Action = "Add"});
        }


        public ActionResult UpdateRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveRole(RoleDto role)
        {
	        if (!ModelState.IsValid)
	        {
		        TempData["MessageType"] = WebMessageType.Failure;
		        TempData["Message"] = "Failed to save Role due to invalid data";

		        return RedirectToAction("UpdateRole");
	        }

			TempData["MessageType"] = WebMessageType.Success;
			TempData["Message"] = "Successfully saved Role";
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